namespace Csn.OrmEdd.Dal
{
    using Csn.OrmEdd.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    public class PhoneDataMapper
    {
        private readonly string _filePath;
        public PhoneDataMapper()
        {
            _filePath = "Phone.csv";
        }
        public PhoneDataMapper(string filePath)
        {
            _filePath = filePath;
        }
        public int GetNextId()
        {
            int maxId = 0;
            List<Phone> phones = GetAll();
            foreach(Phone phone in phones)
            {
                if (maxId < phone.Id) maxId = phone.Id;
            }
            return ++maxId;
        }
        public List<Phone> GetAll()
        {
            List<Phone> phones = new List<Phone>();
            try
            {
                // System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
                StreamReader reader = new StreamReader(_filePath); // default UTF-8
                using (reader)
                {
                    string line = reader.ReadLine();
                    string[] elements = new string[3];
                    string[] separators = { "," };
                    int id = 0;
                    //CultureInfo provider = CultureInfo.InvariantCulture;
                    //string format = "yyyy-MM-dd";
                    while (line != null)
                    {
                        elements = line.Split(separators, StringSplitOptions.None);
                        if (int.TryParse(elements[0], out id))
                        {
                            Phone phone = new Phone();
                            phone.Id = id;
                            phone.PersonId = int.Parse(elements[1]);
                            phone.Number = elements[2];
                            // person.BirthDate = DateTime.Parse(elements[3], provider);
                            // person.BirthDate = DateTime.ParseExact(elements[3],format, provider);
                            phones.Add(phone);
                        }
                        line = reader.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("The file {0} was not found.", _filePath);
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("There was and IOException {0}", e.Message);
            }

            return phones;
        }
        public List<Phone> GetByPersonId(int personId)
        {
            List<Phone> phones = new List<Phone>();
            foreach(Phone phone in GetAll())
            {
                if (phone.PersonId == personId)
                {
                    phones.Add(phone);
                }
            }
            return phones;
        }
        public Phone Get(int id)
        {
            Phone phone = null;
            List<Phone> persons = GetAll();
            foreach (Phone phoneTemp in persons)
            {
                if (phoneTemp.Id == id) return phoneTemp;
            }
            return phone;
        }
        public int Insert(Phone phone)
        {
            if (phone.Id < 1) phone.Id = GetNextId();
            SaveItem(phone);
            return phone.Id;
        }
        public void Update(Phone phone)
        {
            List<Phone> phones = GetAll();
            for (int i = 0; i < phones.Count; i++)
            {
                if (phones[i].Id == phone.Id)
                {
                    phones[i] = phone;
                    break;
                }
            }
            SaveAll(phones);
        }
        public void Delete(int id)
        {
            List<Phone> phones = GetAll();
            for (int i = 0; i < phones.Count; i++)
            {
                if (phones[i].Id == id)
                {
                    phones.RemoveAt(i);
                    break;
                }
            }
            SaveAll(phones);
        }
        private void SaveAll(List<Phone> phones)
        {
            try
            {
                StreamWriter writer = new StreamWriter(_filePath, false); // default UTF-8
                using (writer)
                {
                    foreach(Phone phone in phones)
                    {
                        writer.WriteLine(Extract(phone));
                    }
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("There was and IOException {0}", e.Message);
            }
        }
        private void SaveItem(Phone phone, bool isAppend = true)
        {
            try
            {
                StreamWriter writer = new StreamWriter(_filePath, isAppend);
                using (writer)
                {
                    writer.WriteLine(Extract(phone));
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("There was and IOException {0}", e.Message);
            }
        }
        private string Extract(Phone phone)
        {
            //CultureInfo provider = CultureInfo.InvariantCulture;
            //string format = "yyyy-MM-dd";
            return String.Format("{0},{1},{2}",
                        phone.Id,
                        phone.PersonId,
                        phone.Number);
        }
        private CultureInfo GetProvider()
        {
            return CultureInfo.InvariantCulture;
        }
        private string GetDateFormat()
        {
            return "yyyy-MM-dd";
        }
    }
}
