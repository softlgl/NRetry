using System;
using System.Threading.Tasks;
using NRetry.Test.Model;

namespace NRetry.Test.Service
{
    public class PersonService : IPersonService
    {
        public PersonService()
        {
        }

        public Person GetPerson(int id)
        {
            Person person = new Person
            {
                Id=id,
                Name="liguoliang",
                Birthday=new DateTime(1992,12,11)
            };
            return person;
        }

        [Retry(ExceptionType =typeof(ArgumentException),CallBack = "AddCallBack",RetryCount =2)]
        public void AddPerson(Person person)
        {
            if (person.Id == 0)
            {
                throw new ArgumentException("参数出错了");
            }
        }

        public void AddCallBack(Person person)
        {
            
        }

        public async Task<Person> UpdatePerson(int id,Person person)
        {
            await Task.Delay(10);
            return person;
        }
    }
}
