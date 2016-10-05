namespace Csn.OrmEdd.Services
{
    using Dal;
    using Models;
    using System.Collections.Generic;

    public class PhoneServices
    {
        private readonly PhoneDataMapper _phoneDm;
        public PhoneServices()
        {
            _phoneDm = new PhoneDataMapper();
        }
        public PhoneServices(PhoneDataMapper phoneDataMapper)
        {
            _phoneDm = phoneDataMapper;
        }
        public List<Phone> GetPhonesByPersonId(int personId)
        {
            return _phoneDm.GetByPersonId(personId);
        }
        public Phone Get(int id)
        {
            return _phoneDm.Get(id);
        }
        public void Insert(Phone phone)
        {
            _phoneDm.Insert(phone);
        }
        public void Update(Phone phone)
        {
            _phoneDm.Update(phone);
        }
        public void Delete(int id)
        {
            _phoneDm.Delete(id);
        }
    }
}
