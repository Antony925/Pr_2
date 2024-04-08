using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace KMA.ProgrammingInCSharp24
{
    internal class PersonService
    {
        private List<Person> _person;

        public List<Person> Person
        {
            get { return _person; }
            set { _person = value; }
        }

        public PersonService()
        {
            _person = new List<Person>();
            InitializeUsers();
        }

        public void InitializeUsers()
        {
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                string firstName = NameGenerator.GenerateName() + (i + 1);
                string lastName = NameGenerator.GenerateName() + (i + 1);
                string email = $"{lastName}@gmail.com";

                Random r_date = new Random();
                int year = DateTime.Now.Year - r_date.Next(7, 91);
                int month = r_date.Next(1, 13);
                DateTime birthDate = new DateTime(year, month, 8);

                string Western = CalculateWesternZodiac(birthDate);
                string China = CalculateChinaZodiac(birthDate);
                int Age = CalculateAge(birthDate);
                bool BirthDay = IsBirthDay(birthDate);
                _person.Add(new Person(firstName, lastName, email, birthDate, Western, China, Age, BirthDay));
            }
        }

        public List<Person> GetUsers()
        {
            return _person;
        }

        public void AddUser(Person user)
        {
            _person.Add(user);
        }

        public void RemoveUser(Person user)
        {
            _person.Remove(user);
        }

        public void SaveToFile(string fileName)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var jsonString = JsonSerializer.Serialize(_person, options);
            File.WriteAllText(fileName, jsonString);
        }

        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                var jsonString = File.ReadAllText(fileName);
                _person = JsonSerializer.Deserialize<List<Person>>(jsonString);
            }
        }

        public void SortUsersBy(Func<Person, object> keySelector)
        {
            _person = _person.OrderBy(keySelector).ToList();
        }

        public List<Person> FilterUsersBy(Func<Person, bool> predicate)
        {
            return _person.Where(predicate).ToList();
        }


        public static int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;

            if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }

        public static string CalculateWesternZodiac(DateTime birthDate)
        {
            int month = birthDate.Month;
            int day = birthDate.Day;
            if ((month == 1 && day >= 21) || (month == 2 && day <= 19))
                return "Водолій";
            else if ((month == 2 && day >= 20) || (month == 3 && day <= 20))
                return "Риби";
            else if ((month == 3 && day >= 21) || (month == 4 && day <= 20))
                return "Овен";
            else if ((month == 4 && day >= 21) || (month == 5 && day <= 21))
                return "Телець";
            else if ((month == 5 && day >= 22) || (month == 6 && day <= 21))
                return "Близнюки";
            else if ((month == 6 && day >= 22) || (month == 7 && day <= 22))
                return "Рак";
            else if ((month == 7 && day >= 23) || (month == 8 && day <= 21))
                return "Лев";
            else if ((month == 8 && day >= 22) || (month == 9 && day <= 23))
                return "Діва";
            else if ((month == 9 && day >= 24) || (month == 10 && day <= 23))
                return "Терези";
            else if ((month == 10 && day >= 24) || (month == 11 && day <= 22))
                return "Скорпіон";
            else if ((month == 11 && day >= 23) || (month == 12 && day <= 22))
                return "Стрілець";
            else
                return "Козоріг";
        }

        public static string CalculateChinaZodiac(DateTime birthDate)
        {
            int year = birthDate.Year;
            string[] chineseZodiacs = { "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза", "Мавпа", "Півень", "Собака", "Свиня" };
            int index = (year - 4) % 12;

            if (index < 0)
                index += 12;

            return chineseZodiacs[index];
        }

        public static bool IsBirthDay(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            if (birthDate.Month == currentDate.Month && birthDate.Day == currentDate.Day)
                return true;
            return false;
        }

        internal class NameGenerator
        {
            private static readonly string[] PartNames = { "mon", "ger", "ma", "su", "ar", "kat", "lor", "stu", "dav", "jon", "on", "is", "mer", "ik", "er", "an", "en", "om", "ad", "un", "ry", "ka", "la", "bu", "si", "mo", "na", "le", "ko", "ki", "to", "li", "va", "ro", "lu" };
            private static readonly Random rnd = new Random();

            public static string GenerateName()
            {
                int partsCount = rnd.Next(2, 5); 
                StringBuilder nameBuilder = new StringBuilder();
                for (int i = 0; i < partsCount; i++)
                {
                    string part;
                    if (i == 0)
                        part = PartNames[rnd.Next(PartNames.Length)]; 
                    else
                        part = PartNames[rnd.Next(PartNames.Length)]; // Всі інші частинки прізвища.

                    nameBuilder.Append(part);
                }
                return nameBuilder.ToString();
            }
        }

        public void AddPerson(string firstName, string lastName, string emailAddress, DateTime birthDate)
        {
            string westernSign = CalculateWesternZodiac(birthDate);
            string chineseSign = CalculateChinaZodiac(birthDate);
            int age = CalculateAge(birthDate);
            bool isBirthday = IsBirthDay(birthDate);

            Person person = new Person(firstName, lastName, emailAddress, birthDate, westernSign, chineseSign, age, isBirthday);
            _person.Add(person);
        }

        public void RemovePerson(Person person)
        {
            _person.Remove(person);
        }

        public void SaveChanges()
        {
            SaveToFile("users.json");
        }
    }
}
