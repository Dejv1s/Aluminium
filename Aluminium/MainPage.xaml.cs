using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Net.Sockets;
using System.Text;

namespace Aluminium
{
    public partial class MainPage : ContentPage
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public MainPage()
        {
            InitializeComponent(); // Ensure this is called
            LoadSavedIpAddress(); // Load saved IP address
        }

        private void LoadSavedIpAddress()
        {
            // Load the saved IP address from preferences
            string savedIpAddress = Preferences.Get("SavedIpAddress", string.Empty);
            if (!string.IsNullOrEmpty(savedIpAddress))
            {
                IpAddressEntry.Text = savedIpAddress;
            }
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            string ipAddress = IpAddressEntry.Text;

            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                await DisplayAlert("Error", "Please enter a valid IP address.", "OK");
                return;
            }

            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(ipAddress, 13000); // Connect to the server
                _stream = _client.GetStream();

                // Update UI
                StatusLabel.Text = "Connected";
                StatusLabel.TextColor = Color.FromArgb("#32CD32"); // Green color
                FullscreenButton.IsEnabled = true;
                PlayPauseButton.IsEnabled = true;
                ForwardButton.IsEnabled = true;
                BackwardButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Connection Failed";
                StatusLabel.TextColor = Color.FromRgba("#FF0000"); // Red color
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            // Navigate to the Settings page
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void SendCommand(string command)
        {
            if (_client == null || !_client.Connected)
            {
                await DisplayAlert("Error", "Not connected to the server", "OK");
                return;
            }

            try
            {
                byte[] data = Encoding.ASCII.GetBytes(command);
                await _stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnFullscreenClicked(object sender, EventArgs e)
        {
            SendCommand("fullscreen");
        }

        private void OnPlayPauseClicked(object sender, EventArgs e)
        {
            SendCommand("playpause");
        }

        private void OnForwardClicked(object sender, EventArgs e)
        {
            SendCommand("forward");
        }

        private void OnBackwardClicked(object sender, EventArgs e)
        {
            SendCommand("backward");
        }
    }
}