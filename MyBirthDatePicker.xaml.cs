using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoaderIcon.Visibility = Visibility.Visible;
                DateTime selectedDate = BirthDatePicker.SelectedDate.GetValueOrDefault();

                int age = await Task.Run(() => Person.CalculateAge(selectedDate));

                if (selectedDate > DateTime.Now)
                {
                    throw new FutureBirthDateException("Error! Birth date cannot be in the future.");
                }

                if (selectedDate < DateTime.Now.AddYears(-135))
                {
                    throw new DistantPastBirthDateException("Error! Birth date is too far in the past.");
                }

                if (!IsValidEmail(email))
                {
                    throw new InvalidEmailException("Error! Invalid email address.");
                }

                if (await Task.Run(() => Person.IsBirthDay(selectedDate)))
                {
                    MessageBox.Show("Happy Birthday to you!!!");
                }

                string w_Zodiac = await Task.Run(() => Person.CalculateWesternZodiac(selectedDate));
                string c_Zodiac = await Task.Run(() => Person.CalculateChinaZodiac(selectedDate));

                _Age.Text = $"You are {age} years old";
                _Western.Text = $"Western zodiac: {w_Zodiac}";
                _China.Text = $"China zodiac: {c_Zodiac}";
                _FirstName.Text = $"First Name: {firstName}";
                _LastName.Text = $"Last Name: {lastName}";
                _Email.Text = $"Email: {email}";
                _birthDate.Text = $"Birth Date: {selectedDate.ToString()}";
                if (await Task.Run(() => Person.IsBirthDay(selectedDate)) == true)
                {
                    _IsBirthDay.Text = "Today is your birthday!";
                }
                else
                {
                    _IsBirthDay.Text = "Today is not your birthday";
                }

                LoaderIcon.Visibility = Visibility.Collapsed;
            }
            catch (FutureBirthDateException ex)
            {
                LoaderIcon.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DistantPastBirthDateException ex)
            {
                LoaderIcon.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidEmailException ex)
            {
                LoaderIcon.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                LoaderIcon.Visibility = Visibility.Collapsed;
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Перевірка на "@"
            if (!email.Contains("@"))
                return false;

            // Перевірка на єдиність "@"
            int atIndex = email.IndexOf('@');
            if (email.LastIndexOf('@') != atIndex)
                return false;

            // Перевірка на "@" на початку/кінці адреси
            if (atIndex == 0 || atIndex == email.Length - 1)
                return false;
            foreach (char c in email)
            {
                if (c >= 'а' && c <= 'я' || c >= 'А' && c <= 'Я')
                    return false;
            }

            return true;
        }
    }
}
