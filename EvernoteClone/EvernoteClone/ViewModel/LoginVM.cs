using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private bool isShowingRegister = false;



        private User user;
        public User User
        {
            get { return user; }
            set 
            { 
                user = value;
                OnPropertyChanged("User");
            }
        }


        private string email;
        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                User = new User
                {
                    Email = email,
                    Password = this.Password,
                    ConfirmPassword = confirmPassword,
                    Name = name,
                    Lastname = lastname
                };
                OnPropertyChanged("Email");

            }
        }


        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                User = new User
                {
                    Email = email,
                    Password = this.Password,
                    ConfirmPassword = confirmPassword,
                    Name = name,
                    Lastname = lastname
                };
                OnPropertyChanged("Name");
            }
        }


        private string password;
        public string Password
        {
            get { return password; }
            set 
            {
                password = value;
                User = new User
                {
                    Email = email,
                    Password = this.Password,
                    ConfirmPassword = confirmPassword,
                    Name = name,
                    Lastname = lastname
                };
                OnPropertyChanged("Password");
            }
        }


        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set {
                confirmPassword = value;
                User = new User
                {
                    Email = email,
                    Password = this.Password,
                    ConfirmPassword = confirmPassword,
                    Name = name,
                    Lastname = lastname
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }


        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set 
            { 
                lastname = value; 
                User = new User
                {
                    Email = email,
                    Password = this.Password,
                    ConfirmPassword = confirmPassword,
                    Name = name,
                    Lastname = lastname
                };
                OnPropertyChanged("Lastname");
            }
        }



        private Visibility loginVis;
        public Visibility LoginVis
        {
            get { return loginVis; }
            set 
            {
                loginVis = value;
                OnPropertyChanged("LoginVis");
            }
        }

        private Visibility registerVis;

        public Visibility RegisterVis
        {
            get { return registerVis; }
            set
            {
                registerVis = value;
                OnPropertyChanged("RegisterVis");
            }
        }

        private Visibility passwordErrorVis;

        public Visibility PasswordErrorVis
        {
            get { return passwordErrorVis; }
            set 
            { 
                passwordErrorVis = value;
                OnPropertyChanged("PasswordErrorVis");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler AuthenticationSucessfull;


        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }


        public LoginVM()
        {
            LoginVis = Visibility.Visible;
            RegisterVis = Visibility.Collapsed;
            PasswordErrorVis = Visibility.Hidden;

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            User = new User();
        }


        public void switchViews()
        {
            isShowingRegister = !isShowingRegister;

            //So the information doesnt stay in the fileds, even if switching from login to register
            User = new User();

            Email = null;
            Name = null;
            Lastname = null;
            Password = null;
            ConfirmPassword = null;

            if (isShowingRegister)
            {
                RegisterVis = Visibility.Visible;
                LoginVis = Visibility.Collapsed;
            }
            else
            {
                RegisterVis = Visibility.Collapsed;
                LoginVis = Visibility.Visible;
            }
        }


        public async void login()
        {
            bool result = await FirebaseAuthHelper.Login(User);
            if (result)
            {
                AuthenticationSucessfull?.Invoke(this, new EventArgs());
            }
        }

        public async void register()
        {
            bool result = await FirebaseAuthHelper.Register(User);

            if(result == true)
            {
                switchViews();
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
