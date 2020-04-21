using HandyControl.Controls;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CodeHubDesktop.ViewModels
{
    public class AdminViewModel : BindableBase
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand LoginCommand { get; set; }
        public AdminViewModel()
        {
            LoginCommand = new DelegateCommand(OnLogin);
        }

        public class adminModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        private async void OnLogin()
        {
            var model = new adminModel { Username = Username, Password = Password };
            var json = System.Text.Json.JsonSerializer.Serialize(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var result = await client.PostAsync("http://codehub.pythonanywhere.com/api/v1/admin/login", data);

            MessageBox.Show(result.Content.ReadAsStringAsync().Result);
        }
    }
}
