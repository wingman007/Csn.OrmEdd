namespace Csn.OrmEdd.Console
{
    using Csn.OrmEdd.Models;
    using Dal;
    using Services;
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Retrieve/View/List all items");
                Console.WriteLine("2. Insert");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Phones");
                Console.WriteLine("6. Exit");
                Console.Write("Please, select a number from 1 to 6: ");
                int.TryParse(Console.ReadLine(), out choice);

                // dispatcher
                switch (choice)
                {
                    case 1:
                        GetAll();
                        break;
                    case 2:
                        Insert();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        Delete();
                        break;
                    case 5:
                        Phones();
                        break;
                }
            } while (choice != 6);

            //Person person = new Person();
            //Console.Clear();
            //Console.WriteLine("Please, enter your details:");
            //Console.Write("Name: ");
            //person.Name = Console.ReadLine();
            //Console.Write("Family Name: ");
            //person.FamilyName = Console.ReadLine();
            //Console.Write("Birth Date (yyyy-MM-dd): ");
            //person.BirthDate = Convert.ToDateTime(Console.ReadLine());
            //Console.Write("Address: ");
            //person.Address = Console.ReadLine();

            //Person person = new Person() {
            //    Name = "Stoyan",
            //    FamilyName = "Cheresharov",
            //    BirthDate = DateTime.Parse("1964-03-29"),
            //    Address = "7 Avliga str. "
            //};

            //Console.WriteLine("Let me introduce: Name: {0}, Family: {1} Birthdate: {2}, Address: {3}", 
            //   person.Name,
            //   person.FamilyName,
            //   person.BirthDate.ToString("yyyy-MM-dd"),
            //   person.Address);

            //PersonDataMapper personDm = new PersonDataMapper(); // "/../../Data"
            //personDm.Insert(person);

            //List<Person> persons = personDm.GetAll();
            //foreach(Person personTm in persons)
            //{
            //    Console.WriteLine(personTm);
            //}
        }

        static void GetAll() // ToDo separate this logic too
        {
            // ToDo move it into a Input Controller
            // PersonDataMapper personDm = new PersonDataMapper();
            PersonServices personServices = new PersonServices();
            List<Person> persons = personServices.GetAll();

            // View
            Console.Clear();
            Console.WriteLine("Please press Enter to go back");
            foreach (Person person in persons)
            {
                Console.WriteLine(person);
            }
            Console.ReadLine();
        }

        static void Insert()
        {
            Person person = new Person();
            // View
            Console.Clear();
            Console.WriteLine("Please, enter your details:");
            Console.Write("Name: ");
            person.Name = Console.ReadLine();
            Console.Write("Family Name: ");
            person.FamilyName = Console.ReadLine();
            Console.Write("Birth Date (yyyy-MM-dd): ");
            person.BirthDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Address: ");
            person.Address = Console.ReadLine();

            // Controller ToDo Separate the logic 
            // PersonDataMapper personDm = new PersonDataMapper(); // "/../../Data"
            // personDm.Insert(person);

            PersonServices personServices = new PersonServices();
            personServices.Insert(person);
        }

        static void Update()
        {
            // View
            int id = 0;
            do
            {
                Console.Clear();
                Console.Write("Please, enter the id of a person to edit:");
            } while (!int.TryParse(Console.ReadLine(), out id));

            // Controller ToDo Separate the logic 
            // Person person = new Person();
            // PersonDataMapper personDm = new PersonDataMapper(); // "/../../Data"
            // Person person = personDm.Get(id);
            PersonServices personServices = new PersonServices();
            Person person = personServices.Get(id);

            // View
            Console.WriteLine("Please, enter the details:");
            Console.Write("Name ({0}): ", person.Name);
            person.Name = Console.ReadLine();
            Console.Write("Family Name ({0}): ", person.FamilyName);
            person.FamilyName = Console.ReadLine();
            // Console.Write("Birth Date ({0:d}): ", person.BirthDate.ToString()); // "yyyy-MM-dd"
            Console.Write("Birth Date ({0}): ", person.BirthDate.ToString("yyyy-MM-dd")); // "yyyy-MM-dd"
            person.BirthDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Address ({0}): ", person.Address);
            person.Address = Console.ReadLine();

            // Controller
            // personDm.Update(person);
            personServices.Update(person);
        }

        static void Delete()
        {
            // View
            int id = 0;
            do
            {
                Console.Clear();
                Console.Write("Please, enter the id of a person to delete:");
            } while (!int.TryParse(Console.ReadLine(), out id));

            // Controller
            // PersonDataMapper personDm = new PersonDataMapper();
            // personDm.Delete(id);
            PersonServices personServices = new PersonServices();
            personServices.Delete(id);
        }

        static void Phones()
        {
            // View
            int id = 0;
            do
            {
                Console.Clear();
                Console.Write("Please, enter the id of a person to manage the pones: ");
            } while (!int.TryParse(Console.ReadLine(), out id));
            PersonServices personServices = new PersonServices();
            // PhoneServices phoneServices = new PhoneServices();
            Person person = personServices.Get(id);
            int choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Phones for {0} {1}", person.Name, person.FamilyName);
                Console.WriteLine("1. Retrieve/View/List all phones");
                Console.WriteLine("2. Insert");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Go back to main menu");
                Console.Write("Please, select a number from 1 to 5: ");
                int.TryParse(Console.ReadLine(), out choice);
                switch(choice)
                {
                    case 1:
                        GetAllPhonesByPerson(person);
                        break;
                    case 2:
                        InsertPhone(person);
                        break;
                    case 3:
                        UpdatePhone(person);
                        break;
                    case 4:
                        DeletePhone(person);
                        break;
                }
            } while (choice != 5);
        }

        static void GetAllPhonesByPerson(Person person)
        {
            // Controller
            PhoneServices phoneServices = new PhoneServices();
            List<Phone> phones = phoneServices.GetPhonesByPersonId(person.Id);

            // view
            Console.Clear();
            Console.WriteLine("Press Enter to return.");
            foreach(Phone phone in phones)
            {
                Console.WriteLine(phone);
            }
            Console.ReadLine();
        }

        static void InsertPhone(Person person)
        {
            Phone phone = new Phone();
            
            // View
            Console.Clear();
            Console.WriteLine("Please, enter phone for {0} {1}:", 
                person.Name, person.FamilyName);
            Console.Write("Number: ");
            phone.Number = Console.ReadLine();
            Console.Write("Family Name: ");
            phone.PersonId = person.Id;

            // Controller
            PhoneServices phoneServices = new PhoneServices();
            phoneServices.Insert(phone);
        }

        static void UpdatePhone(Person person)
        {
            int id = 0;
            do
            {
                Console.Clear();
                Console.Write("Please, enter the id of a phone to edit:");
            } while (!int.TryParse(Console.ReadLine(), out id));

            // Controller
            PhoneServices phoneServices = new PhoneServices();
            Phone phone = phoneServices.Get(id);
            if (phone.PersonId != person.Id) return;
            
            // View
            Console.Write("Number ({0}): ", phone.Number);
            phone.Number = Console.ReadLine();
          
            // Controller
            phoneServices.Update(phone);

        }

        static void DeletePhone(Person person)
        {
            int id = 0;
            do
            {
                Console.Clear();
                Console.Write("Please, enter the id of a phone to delete:");
            } while (!int.TryParse(Console.ReadLine(), out id));

            // Controller
            PhoneServices phoneServices = new PhoneServices();
            Phone phone = phoneServices.Get(id);
            if (phone.PersonId != person.Id) return;
            phoneServices.Delete(id);
        }
    }
}
