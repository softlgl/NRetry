using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.Reflection;
using NRetry.Options;
using Polly;

namespace NRetry
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RetryAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 超时时间
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// 回调方法
        /// </summary>
        public string CallBack { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public Type ExceptionType { get; set; }

        /// <summary>
        /// 属性注入
        /// </summary>
        [FromServiceContext]
        private RetryOption retryOption { get; set; }

        //学习地址
        //https://www.cnblogs.com/edisonchou/p/9159644.html
        //https://www.cnblogs.com/willick/p/polly.html

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            List<Policy> policys = new List<Policy>();
            int timeout = Timeout > 0 ? Timeout : (retryOption.Timeout > 0 ? retryOption.Timeout : 0);
            if (timeout > 0)
            {
                policys.Add(Policy.Timeout(timeout, onTimeout: (pollyContext, timespan, task) =>
                {
                }));
            }
            var builder = ExceptionType == null ? Policy.Handle<Exception>() : Policy.Handle<Exception>(e => e.GetType().IsAssignableFrom(ExceptionType));
            if (!string.IsNullOrEmpty(CallBack))
            {
                policys.Add(builder.Fallback(() =>
                {
                    BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    var callBackMethod = context.ServiceMethod.DeclaringType.GetMethod(CallBack, flags);
                    object returnValue = callBackMethod.GetReflector().Invoke(context.Implementation, context.Parameters);
                    context.ReturnValue = returnValue;
                }));
            }
            int retryCount = RetryCount > 0 ? RetryCount : (retryOption.RetryCount > 0 ? retryOption.RetryCount : 0);
            if (retryCount > 0)
            {
                policys.Add(builder.Retry(retryCount, (exception, currentCount, pollyContext) =>
                {
                }));
            }
            await Policy.Wrap(policys.ToArray()).Execute(() => next(context));
        }
    }
}
