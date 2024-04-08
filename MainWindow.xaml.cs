using System;
using System.Windows;

namespace KMA.ProgrammingInCSharp24.MorgunLab01
{
    public partial class MainWindow : Window
    {
        private PersonService _authenticationService;

        public MainWindow()
        {
            InitializeComponent();
            var MyBirthDatePicker = new MyBirthDatePicker();
            MyBirthDatePicker.ShowDialog();

            _authenticationService = new PersonService();

            try
            {
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadUsers()
        {
            _authenticationService.LoadFromFile("users.json");
            userGrid.ItemsSource = _authenticationService.GetUsers();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;
            DateTime birthDate = BirthDatePicker.SelectedDate.GetValueOrDefault();

            _authenticationService.AddPerson(firstName, lastName, email, birthDate);
            _authenticationService.SaveChanges();
            LoadUsers();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Person selectedPerson = userGrid.SelectedItem as Person;
            if (selectedPerson != null)
            {
                _authenticationService.RemovePerson(selectedPerson);
                _authenticationService.SaveChanges();
                LoadUsers();
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _authenticationService.SaveChanges();
            MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
