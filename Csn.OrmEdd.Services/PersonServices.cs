namespace Csn.OrmEdd.Services
{
    using Dal;
    using Models;
    using System.Collections;
    using System.Collections.Generic;

    public class PersonServices
    {
        private readonly PersonDataMapper _personDm;
        public PersonServices()
        {
            _personDm = new PersonDataMapper();
        }
        public PersonServices(PersonDataMapper personDm)
        {
            _personDm = personDm;
        }
        public List<Person> GetAll()
        {
            return _personDm.GetAll();
        }

        public void Insert(Person person)
        {
            _personDm.Insert(person);
        }

        public Person Get(int id)
        {
            return _personDm.Get(id);
        }

        public void Update(Person person)
        {
            _personDm.Update(person);
        }

        public void Delete(int id)
        {
            _personDm.Delete(id);
        }
    }
}
