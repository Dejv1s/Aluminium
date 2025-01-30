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
            InitializeComponent();

            // Load saved IP address when the page loads
            LoadSavedIpAddress();
        }

        private void LoadSavedIpAddress()
        {
            // Retrieve the saved IP address from preferences
            string savedIpAddress = Preferences.Get("SavedIpAddress", string.Empty);

            // If a saved IP address exists, set it in the Entry field
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
                await _client.ConnectAsync(ipAddress, 13000);
                _stream = _client.GetStream();

                // Update UI
                StatusLabel.Text = "Connected";
                StatusLabel.TextColor = Color.FromArgb("#32CD32");
                FullscreenButton.IsEnabled = true;
                PlayPauseButton.IsEnabled = true;
                ForwardButton.IsEnabled = true;
                BackwardButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Connection Failed";
                StatusLabel.TextColor = Color.FromRgba("#FF0000");
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            // Navigate to the Settings page
            await Navigation.PushAsync(new SettingsPage());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadSavedIpAddress(); // Reload the saved IP address every time the page appears
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