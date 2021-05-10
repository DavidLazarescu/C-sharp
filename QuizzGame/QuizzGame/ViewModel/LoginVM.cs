using QuizzGame.Model;
using QuizzGame.ViewModel.Commands.Login;
using QuizzGame.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace QuizzGame.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        /*Commands*/
        public LoginCommand LoginCommand { get; set; }
        public RegisterCommand RegisterCommand { get; set; }
        public ResetPasswordCommand ResetPasswordCommand { get; set; }
        public ToResetPasswordWindowCommand ToResetPasswordWindowCommand { get; set; }
        public ToLoginWindowCommand ToLoginWindowCommand { get; set; }
        public ToRegisterWindowCommand ToRegisterWindowCommand { get; set; }


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



        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set 
            { 
                firstname = value;

                User = new User
                {
                    Email = email,
                    ConfirmPassword = confirmPassword,
                    Password = password,
                    Lastname = lastname,
                    Firstname = firstname
                };

                OnPropertyChanged("Firstname");
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
                    ConfirmPassword = confirmPassword,
                    Password = password,
                    Lastname = lastname,
                    Firstname = firstname
                };

                OnPropertyChanged("Lastname");
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
                    ConfirmPassword = confirmPassword,
                    Password = password,
                    Lastname = lastname,
                    Firstname = firstname
                };

                OnPropertyChanged("Email");
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
                    ConfirmPassword = confirmPassword,
                    Password = password,
                    Lastname = lastname,
                    Firstname = firstname
                };

                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set 
            { 
                confirmPassword = value;

                User = new User
                {
                    Email = email,
                    ConfirmPassword = confirmPassword,
                    Password = password,
                    Lastname = lastname,
                    Firstname = firstname
                };

                OnPropertyChanged("ConfirmPassword");
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

        private Visibility resetPasswordVis;
        public Visibility ResetPasswordVis
        {
            get { return resetPasswordVis; }
            set 
            { 
                resetPasswordVis = value;
                OnPropertyChanged("ResetPasswordVis");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler AuthentificationSucessfull;


        public LoginVM()
        {
            /*Commands*/
            LoginCommand = new LoginCommand(this);
            RegisterCommand = new RegisterCommand(this);
            ResetPasswordCommand = new ResetPasswordCommand(this);
            ToResetPasswordWindowCommand = new ToResetPasswordWindowCommand(this);
            ToLoginWindowCommand = new ToLoginWindowCommand(this);
            ToRegisterWindowCommand = new ToRegisterWindowCommand(this);

            User = new User();

            /*Visibilitys*/
            LoginVis = Visibility.Visible;
            RegisterVis = Visibility.Collapsed;
            ResetPasswordVis = Visibility.Collapsed;
        }



        public async void login()
        {
            /*calls the Login method*/
            if (await LoginHelper.Login(User) == true)
            {
                AuthentificationSucessfull?.Invoke(this, new EventArgs());
            }
        }

        public async void register()
        {
            /*calls the Register method*/
            if (await LoginHelper.Register(User) == true)
            {
                if (ToLoginWindowCommand.CanExecute(new object()))
                {
                    ToLoginWindowCommand.Execute(new object());
                }
            }
        }

        public async void resetPassword()
        {
            /*calls the ResetPassword method*/
            if(await LoginHelper.ResetPassword(User) == true)
            {
                registerVis = Visibility.Collapsed;
                resetPasswordVis = Visibility.Collapsed;
                loginVis = Visibility.Visible;
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
