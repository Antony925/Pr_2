using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KMA.ProgrammingInCSharp24
{
    /// <summary>
    /// Логика взаимодействия для MyBirthDatePicker.xaml
    /// </summary>
    public partial class MyBirthDatePicker : Window
    {
        private string firstName;
        private string lastName;
        private string email;

        public MyBirthDatePicker()
        {
            InitializeComponent();
            LoaderIcon.Visibility = Visibility.Collapsed;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            firstName = FirstNameTextBox.Text;
            lastName = LastNameTextBox.Text;
            email = EmailTextBox.Text;
            DateTime birthDate = BirthDatePicker.SelectedDate.GetValueOrDefault();

            // Перевіряємо, чи всі поля введення не пусті
            if (!string.IsNullOrWhiteSpace(FirstNameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(LastNameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(EmailTextBox.Text) &&
                BirthDatePicker.SelectedDate != null)
            {
                ProceedButton.IsEnabled = true;
            }
            else
            {
                ProceedButton.IsEnabled = false;
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            LoaderIcon.Visibility = Visibility.Visible;
            DateTime selectedDate = BirthDatePicker.SelectedDate.GetValueOrDefault();

            int age = Person.CalculateAge(selectedDate);
            if (age < 0 || age > 135)
            {
                MessageBox.Show("Error! Please, enter your reall birth date!");
                LoaderIcon.Visibility = Visibility.Collapsed;
                return;
            }

            if (Person.IsBirthDay(selectedDate))
            {
                MessageBox.Show("Happy Birthday to you!!!");
            }

            string w_Zodiac = Person.CalculateWesternZodiac(selectedDate);
            string c_Zodiac = Person.CalculateChinaZodiac(selectedDate);


            _Age.Text = $"You are {age} years old";
            _Western.Text = $"Western zodiac: {w_Zodiac}";
            _China.Text = $"China zodiac: {c_Zodiac}";
            _FirstName.Text = $"First Name: {firstName}";
            _LastName.Text = $"Last Name: {lastName}";
            _Email.Text = $"Email: {email}";
            _birthDate.Text = $"Birth Date: {selectedDate.ToString()}";
            if (Person.IsBirthDay(selectedDate) == true)
            {
                _IsBirthDay.Text = "Today is your birthday";
            }
            else
            {
                _IsBirthDay.Text = "Today is not your birthday";
            }
            LoaderIcon.Visibility = Visibility.Collapsed;
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
