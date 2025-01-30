using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluminium
{
    public partial class SettingsPage : ContentPage
    {
        private List<string> _ipAddresses;

        public SettingsPage()
        {
            InitializeComponent();

            // Load saved IP addresses when the page loads
            LoadSavedIpAddresses();
        }

        private void LoadSavedIpAddresses()
        {
            // Retrieve saved IP addresses from preferences
            string savedIpAddresses = Preferences.Get("SavedIpAddresses", string.Empty);

            // Split the saved IP addresses into a list
            _ipAddresses = string.IsNullOrEmpty(savedIpAddresses)
                ? new List<string>()
                : savedIpAddresses.Split(',').ToList();

            // Bind the list to the ListView
            IpAddressList.ItemsSource = _ipAddresses;
        }

        private void OnSaveIpAddressClicked(object sender, EventArgs e)
        {
            string newIpAddress = NewIpAddressEntry.Text;

            if (!string.IsNullOrWhiteSpace(newIpAddress))
            {
                // Add the new IP address to the list
                _ipAddresses.Add(newIpAddress);

                // Save the updated list to preferences
                Preferences.Set("SavedIpAddresses", string.Join(",", _ipAddresses));

                // Refresh the ListView
                IpAddressList.ItemsSource = null;
                IpAddressList.ItemsSource = _ipAddresses;

                // Clear the new IP address entry
                NewIpAddressEntry.Text = string.Empty;

                DisplayAlert("Success", "IP Address Saved", "OK");
            }
            else
            {
                DisplayAlert("Error", "Please enter a valid IP address.", "OK");
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var ipAddress = button?.BindingContext as string;

            if (ipAddress != null)
            {
                // Prompt the user to edit the IP address
                string newIpAddress = await DisplayPromptAsync("Edit IP Address", "Enter the new IP address:", initialValue: ipAddress);

                if (!string.IsNullOrWhiteSpace(newIpAddress))
                {
                    // Update the IP address in the list
                    int index = _ipAddresses.IndexOf(ipAddress);
                    _ipAddresses[index] = newIpAddress;

                    // Save the updated list to preferences
                    Preferences.Set("SavedIpAddresses", string.Join(",", _ipAddresses));

                    // Refresh the ListView
                    IpAddressList.ItemsSource = null;
                    IpAddressList.ItemsSource = _ipAddresses;

                    DisplayAlert("Success", "IP Address Updated", "OK");
                }
            }
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var ipAddress = button?.BindingContext as string;

            if (ipAddress != null)
            {
                // Remove the IP address from the list
                _ipAddresses.Remove(ipAddress);

                // Save the updated list to preferences
                Preferences.Set("SavedIpAddresses", string.Join(",", _ipAddresses));

                // Refresh the ListView
                IpAddressList.ItemsSource = null;
                IpAddressList.ItemsSource = _ipAddresses;

                DisplayAlert("Success", "IP Address Deleted", "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            // Navigate back to the MainPage
            await Navigation.PopAsync();
        }
    }
}