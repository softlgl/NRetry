using System;
using System.Collections.Generic;
using NRetry.Test.Model;
using NUnit.Framework;
using Polly;

namespace NRetry.Test
{
    public class PollyTest
    {
        [Test]
        public void Test()
        {
            List<Policy> list = new List<Policy>();
            //var retryPolicy = Policy
            //.Handle<ArgumentException>()
            //.Retry(3, (exception, retryCount, context) =>
            //{
            //    Console.WriteLine($"开始第 {retryCount} 次重试：");
            //});
            var builder = typeof(ArgumentException) == null ? Policy.Handle<Exception>() : Policy.Handle<Exception>(e => e.GetType().IsAssignableFrom(typeof(ArgumentException)));
            list.Add(builder
            .Retry(3, (exception, retryCount, context) =>
            {
                Console.WriteLine($"开始第 {retryCount} 次重试：");
            }));

            //var timeOutPolicy = Policy.Timeout(3, onTimeout: (pollyContext, timespan, task) =>
            //{

            //});
            list.Add(Policy.Timeout(3, onTimeout: (pollyContext, timespan, task) =>
            {

            }));

            list.Add(builder.Fallback(()=>
            {

            }));

            Policy.Wrap(list.ToArray()).Execute(()=> { this.AddPerson(new Person()); });
        }

        public void AddPerson(Person person)
        {
            if (person.Id == 0)
            {
                throw new ArgumentException("参数出错了");
            }
        }
    }
}
