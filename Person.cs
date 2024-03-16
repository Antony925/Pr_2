using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp24
{
    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }

        public bool IsAdult
        {
            get
            {
                int age = CalculateAge(BirthDate);
                return age >= 18;
            }
        }

        public string WesternSign
        {
            get
            {
                return CalculateWesternZodiac(BirthDate);
            }
        }

        public string ChineseSign
        {
            get
            {
                return CalculateChinaZodiac(BirthDate);
            }
        }

        public bool IsBirthday
        {
            get
            {
                return IsBirthDay(BirthDate);
            }
        }

        public Person(string firstName, string lastName, string emailAddress, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
        }

        public Person(string firstName, string lastName, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            BirthDate = default; 
        }

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = string.Empty; 
            BirthDate = birthDate;
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
    }
}
