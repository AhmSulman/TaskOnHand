using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace TaskOnHand
{
    public partial class MainWindow : Window
    {
        private string jsonFilename;

        public MainWindow()
        {
            InitializeComponent();

            // Display the current date
            DateTextBlock.Text = DateTime.Now.ToString("MMMM dd, yyyy");

            // Set the default filename based on the current date
            jsonFilename = DateTime.Now.ToString("MM_dd_yyyy") + ".json";

            // Load tasks from the JSON file
            json_deserialize(jsonFilename);
        }

        public string Datetime = DateTime.Now.ToLongDateString();

        private void AddToDoButton_Click(object sender, RoutedEventArgs e)
        {
            string newToDo = NewTodoTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(newToDo))
            {
                TaskListBox.Items.Add(newToDo);
                NewTodoTextBox.Clear();
            }
        }

        private void RemoveToDoButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = TaskListBox.SelectedItem;
            if (selectedItem != null)
            {
                TaskListBox.Items.Remove(selectedItem);
            }
        }

        private void json_serialize(string jsonFilename)
        {
            var tasks = new List<string>();

            foreach (var item in TaskListBox.Items)
            {
                tasks.Add(item.ToString());
            }

            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(jsonFilename, json);
        }

        private void json_deserialize(string jsonFilename)
        {
            if (File.Exists(jsonFilename))
            {
                string json = File.ReadAllText(jsonFilename);
                var tasks = JsonConvert.DeserializeObject<List<string>>(json);

                if (tasks != null)
                {
                    TaskListBox.Items.Clear();
                    foreach (var task in tasks)
                    {
                        TaskListBox.Items.Add(task);
                    }
                }
            }
            else
            {
                MessageBox.Show("No tasks found for the specified date.", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GetDate_button(object sender, RoutedEventArgs e)
        {
            // Fetch the date from the TextBox
            string dateInput = GetDateList.Text.Trim();

            if (!string.IsNullOrEmpty(dateInput))
            {
                // Validate the date format
                DateTime parsedDate;
                if (DateTime.TryParseExact(dateInput, "MM_dd_yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    dateInput = dateInput.Replace("/", "_");
                    string filename = dateInput + ".json";
                    json_deserialize(filename);
                }
                else
                {
                    MessageBox.Show("Please enter a valid date in the format MM_dd_yyyy.", "Invalid Date Format", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // Save tasks to JSON when the window is closing
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            json_serialize(jsonFilename);
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            json_serialize(jsonFilename);
            Close();
        }
    }
}
