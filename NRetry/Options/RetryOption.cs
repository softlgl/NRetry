using System;
namespace NRetry.Options
{
    public class RetryOption
    {
        /// <summary>
        /// 超时时间
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }
    }
}
