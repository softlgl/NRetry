using System;
using System.Threading.Tasks;
using NRetry.Test.Model;

namespace NRetry.Test.Service
{
    public interface IPersonService
    {
        Person GetPerson(int id);

        void AddPerson(Person person);

        Task<Person> UpdatePerson(int id,Person person);
    }
}
