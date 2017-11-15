using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Reflection;
using TheTagent.Services;
using Xamarin.Forms;

namespace TheTangent
{
    
        public partial class LoginPage : ContentPage
        {
            public LoginPage()
            {
                InitializeComponent();
                txtUsername.Completed += (s, e) => txtPassword.Focus();
            }
            protected void btnLoginClicked(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    DisplayAlert("Error", "Please enter your user name", "OK");
                    return;
                }

             

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    DisplayAlert("Error", "Please enter your password.", "OK");
                    return;
                }

                DoLogin(); // async call    

            }

            private async void DoLogin()
            {
                string errorMessage = "";

                using (var dlg = UserDialogs.Instance.Loading("Authenticating...", null, null, true, MaskType.Black))
                {
                var token = await TheTagent.Services.Client.Login(txtUsername.Text, txtPassword.Text);


                }

                if (errorMessage != "")
                    await DisplayAlert("Error", errorMessage, "OK");

            }

        }

}
