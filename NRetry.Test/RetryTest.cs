using System;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NRetry.Extensions;
using NRetry.Test.Model;
using NRetry.Test.Service;
using NUnit.Framework;

namespace NRetry.Test
{
    public class RetryTest
    {
        [Test]
        public void TestTimeOut()
        {
            Assert.Pass();
        }

        [Test]
        public void TestRetryCount()
        {
            Assert.Pass();
        }

        [Test]
        public void TestExceptionType()
        {
            IServiceProvider serviceProvider = BuildServiceProvider();
            IPersonService personService = serviceProvider.GetService<IPersonService>();
            personService.AddPerson(new Person());
            Assert.Pass();
        }

        [Test]
        public void TestCallBack()
        {
            Assert.Pass();
        }

        private static IServiceProvider BuildServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IPersonService, PersonService>();
            services.AddNRetry();
            return services.BuildDynamicProxyProvider();
        }
    }
}