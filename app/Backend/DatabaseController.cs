using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Backend
{
    public class DatabaseController : IDatabaseController
    {
        SQLiteAsyncConnection Database;
        private DatabaseController instance;
        public bool DateTimeStoredAsInteger => true;

        public SQLiteOpenFlags CreationFlags { get; } =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache |
            SQLiteOpenFlags.FullMutex;

        private SQLiteAsyncConnection ConnectionFactory(string dbPath) =>
            new SQLiteAsyncConnection(dbPath, CreationFlags, DateTimeStoredAsInteger);

        public async Task<List<Person>> GetPeopleAsync()
        {
            return await Database.Table<Person>().ToListAsync();
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            return await Database.GetAsync<Person>(id);
        }

        public async Task<int> SavePersonAsync(Person person)
        {
            await Database.InsertAsync(person);
            return person.ID;
        }

        public async Task DeletePersonAsync(int id)
        {
            await Database.DeleteAsync(id);
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            // find user
            Person? personOriginal = await Database.Table<Person>().Where(i => i.ID == person.ID).FirstOrDefaultAsync();

            if(personOriginal == null)
            {
                return null;
            }

            // update user
            person = personOriginal;
            await Database.UpdateAsync(person);

            return person;
        }

        public DatabaseController()
        {
            if (instance != null) return;
            string dbPath = Constants.DatabasePath;
            Database = ConnectionFactory(dbPath);
            Database.CreateTableAsync(typeof(Person)).Wait();
        }

    }

    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string BirthNumber { get; set; } = string.Empty;
    }
}
