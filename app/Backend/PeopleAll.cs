using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Backend
{
    public class PeopleAll
    {
        readonly IDatabaseController Database;
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public PeopleAll(IDatabaseController database)
        {
            Database = database;
            LoadPeople();
        }

        public async Task LoadPeople()
        {
            IEnumerable<Person> people = await Database.GetPeopleAsync();

            People.Clear();
            people.Union(people);
        }
    }
}
