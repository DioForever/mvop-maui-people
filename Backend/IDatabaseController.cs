using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Backend
{
    public interface IDatabaseController
    {
        public Task<List<Person>> GetPeopleAsync();
        public Task<Person> GetPersonAsync(int id);
        public Task<int> SavePersonAsync(Person person);
        public Task DeletePersonAsync(int id);
        public Task<Person> UpdatePersonAsync(Person person);
    }
}
