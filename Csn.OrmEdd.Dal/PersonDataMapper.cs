namespace Csn.OrmEdd.Dal
{
    using Csn.OrmEdd.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public class PersonDataMapper
    {
        private readonly string _filePath;

        public PersonDataMapper()
        {
            _filePath = "Person.csv";
        }

        public PersonDataMapper(string filePath)
        {
            _filePath = filePath;
        }

        public int GetNextId()
        {
            int id = 0;
            int maxId = 0;
            try
            {
                // FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
                // StreamReader sr = new StreamReader(fs);
                // or
                // System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
                // StreamWriter reader = new StreamReader(_file, encoding);
                StreamReader reader = new StreamReader(_filePath); // default UTF-8
                using (reader)
                {
                    string line = reader.ReadLine();
                    string[] elements = new string[5];
                    string[] separators = { "," };
                    while(line != null)
                    {
                        elements = line.Split(separators,StringSplitOptions.None);
                        if (int.TryParse(elements[0], out id))
                        {
                            if (id > maxId) maxId = id;
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
            return ++maxId;
        }
        public int Insert(Person person)
        {
            if (person.Id < 1) person.Id = GetNextId();
            SaveItem(person);
            //try
            //{
            //    // FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            //    // StreamWriter sr = new StreamWriter(fs);

            //    // System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
            //    // StreamWriter writer = new StreamWriter(_file, false, encoding);
            //    StreamWriter writer = new StreamWriter(_filePath,true); // default UTF-8
            //    using (writer)
            //    {
            //        CultureInfo provider = CultureInfo.InvariantCulture;
            //        // provider = new CultureInfo("fr-FR");
            //        // string format = "ddd dd MMM yyyy h:mm tt zzz";
            //        // string format = "dd/mm/yyy";
            //        string format = "yyyy-MM-dd";
            //        // DateTime myDate = new DateTime(long ticks);
            //        // DateTime myDate = new DateTime(int year, int, month, int day);
            //        writer.WriteLine("{0},{1},{2},{3},{4}",
            //            person.Id,
            //            person.Name,
            //            person.FamilyName,
            //            person.BirthDate.ToString(format, provider),
            //            person.Address
            //            );
            //    }
            //}
            //catch (IOException e)
            //{
            //    Console.Error.WriteLine("There was and IOException {0}", e.Message);
            //}
            //finally
            //{

            //}
            return person.Id;
        }
        public List<Person> GetAll()
        {
            List<Person> persons = new List<Person>();
            try
            {
                // FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
                // StreamReader reader = new StreamReader(fileStream);
                // or
                // System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
                // StreamWriter reader = new StreamReader(_file, encoding);
                StreamReader reader = new StreamReader(_filePath); // default UTF-8
                using (reader)
                {
                    string line = reader.ReadLine();
                    string[] elements = new string[5];
                    string[] separators = { "," };
                    int id = 0;
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    string format = "yyyy-MM-dd";
                    while (line != null)
                    {
                        elements = line.Split(separators, StringSplitOptions.None);
                        if (int.TryParse(elements[0],out id))
                        {
                            Person person = new Person();
                            person.Id = id;
                            person.Name = elements[1];
                            person.FamilyName = elements[2];
                            // person.BirthDate = DateTime.Parse(elements[3], provider);
                            // person.BirthDate = DateTime.ParseExact(elements[3],format, provider);
                            person.BirthDate = DateTime.Parse(elements[3]);
                            person.Address = elements[4];
                            persons.Add(person);
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
            return persons; 
        }
        public Person Get(int id)
        {
            Person person = null;
            List<Person> persons = GetAll();
            foreach(Person personTemp in persons)
            {
                if (personTemp.Id == id) return personTemp;
            }
            return person;
        }
        public void Update(Person person)
        {

            //string tempFilePath = "temp." + _filePath;

            //FileStream ifs = new FileStream(_filePath, FileMode.OpenOrCreate);
            //StreamReader sr = new StreamReader(ifs);

            //FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            //StreamWriter sw = new StreamWriter(ofs);


            List<Person> persons = GetAll();
            for (int i =0; i < persons.Count; i++)
            {
                if (persons[i].Id == person.Id)
                {
                    persons[i] = person;
                    break;
                }
            }
            SaveAll(persons);

            //try
            //{
            //    // FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            //    // StreamWriter sr = new StreamWriter(fs);

            //    // System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
            //    // StreamWriter writer = new StreamWriter(_file, false, encoding);
            //    StreamWriter writer = new StreamWriter(_filePath, false); // default UTF-8
            //    using (writer)
            //    {
            //        CultureInfo provider = CultureInfo.InvariantCulture;
            //        // provider = new CultureInfo("fr-FR");
            //        // string format = "ddd dd MMM yyyy h:mm tt zzz";
            //        // string format = "dd/mm/yyy";
            //        string format = "yyyy-MM-dd";
            //        // DateTime myDate = new DateTime(long ticks);
            //        // DateTime myDate = new DateTime(int year, int, month, int day);
            //        foreach(Person personTemp in persons)
            //        {
            //            writer.WriteLine("{0},{1},{2},{3},{4}",
            //                personTemp.Id,
            //                personTemp.Name,
            //                personTemp.FamilyName,
            //                personTemp.BirthDate.ToString(format, provider),
            //                personTemp.Address
            //            );
            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    Console.Error.WriteLine("There was and IOException {0}", e.Message);
            //}

            //File.Delete(filePath);
            //File.Move(tempFilePath, filePath);
        }
        public void Delete(int id)
        {
            List<Person> persons = GetAll();
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].Id == id)
                {
                    persons.RemoveAt(i);
                    break;
                }
            }
            SaveAll(persons);
        }
        private void SaveAll(List<Person> persons)
        {
            try
            {
                // FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
                // StreamWriter sr = new StreamWriter(fs);

                // System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
                // StreamWriter writer = new StreamWriter(_file, false, encoding);
                StreamWriter writer = new StreamWriter(_filePath, false); // default UTF-8
                using (writer)
                {
                    // CultureInfo provider = CultureInfo.InvariantCulture;
                    // provider = new CultureInfo("fr-FR");
                    // string format = "ddd dd MMM yyyy h:mm tt zzz";
                    // string format = "dd/mm/yyy";
                    // string format = "yyyy-MM-dd";
                    // DateTime myDate = new DateTime(long ticks);
                    // DateTime myDate = new DateTime(int year, int, month, int day);
                    foreach (Person personTemp in persons)
                    {
                        writer.WriteLine( Extract(personTemp)
                            //"{0},{1},{2},{3},{4}",
                            //personTemp.Id,
                            //personTemp.Name,
                            //personTemp.FamilyName,
                            //personTemp.BirthDate.ToString(format, provider),
                            //personTemp.Address
                        );
                    }
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("There was and IOException {0}", e.Message);
            }
        }
        private void SaveItem(Person person, bool isAppend = true)
        {
            try
            {
                // FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
                // StreamWriter sr = new StreamWriter(fs);

                // System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
                // StreamWriter writer = new StreamWriter(_file, false, encoding);
                StreamWriter writer = new StreamWriter(_filePath, isAppend); // default UTF-8
                using (writer)
                {
                    // CultureInfo provider = CultureInfo.InvariantCulture;
                    // provider = new CultureInfo("fr-FR");
                    // string format = "ddd dd MMM yyyy h:mm tt zzz";
                    // string format = "dd/mm/yyy";
                    // string format = "yyyy-MM-dd";
                    // DateTime myDate = new DateTime(long ticks);
                    // DateTime myDate = new DateTime(int year, int, month, int day);
                    writer.WriteLine(Extract(person)
                        //"{0},{1},{2},{3},{4}",
                        //person.Id,
                        //person.Name,
                        //person.FamilyName,
                        //person.BirthDate.ToString(format, provider),
                        //person.Address
                        );
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("There was and IOException {0}", e.Message);
            }
        }
        private string Extract(Person person)
        {
            //CultureInfo provider = CultureInfo.InvariantCulture;
            //string format = "yyyy-MM-dd";
            return String.Format("{0},{1},{2},{3},{4}",
                        person.Id,
                        person.Name,
                        person.FamilyName,
                        person.BirthDate.ToString(GetDateFormat(), GetProvider()),
                        person.Address);
        }
        //private void Hydrate(Person person, string line)
        //{
        //    string[] elements = new string[5];
        //    string[] separators = { "," };
        //    int id = 0;
        //    CultureInfo provider = GetProvider();// CultureInfo.InvariantCulture;
        //    string format = GetDateFormat(); // "yyyy-MM-dd";

        //    person.Id = id;
        //    person.Name = elements[1];
        //    person.FamilyName = elements[2];
        //    // person.BirthDate = DateTime.Parse(elements[3], provider);
        //    // person.BirthDate = DateTime.ParseExact(elements[3],format, provider);
        //    person.BirthDate = DateTime.Parse(elements[3]);
        //    person.Address = elements[4];
        //}
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
