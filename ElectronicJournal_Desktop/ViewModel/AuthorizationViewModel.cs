using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
namespace ElectronicJournal_Desktop.ViewModel
{

	public class LoginAndPassword
	{
		public string Login { get; set; }
		public string Password { get; set; }
	}
	public class AuthorizationViewModel :ViewModelBase
	{
		#region Fields

		private readonly INavigationManager _navigationManager;
		private readonly DialogManager _dialogManager;
		private LoginAndPassword _loginAndPassword;
		private Users _user;
		private UserSession _userSession;

		#endregion

		#region Constructor

		public AuthorizationViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Property
		public LoginAndPassword LoginAndPassword
		{
			get
			{
				if (_loginAndPassword == null)
					_loginAndPassword = new LoginAndPassword();
				return _loginAndPassword;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException();
				_loginAndPassword = value;
				OnPropertyChanged("LoginAndPassword");
			}
		}
		#endregion

		#region Авторизация пользователя

		RelayCommand _authotizationCommand;

		public ICommand AuthotizationCommand
		{
			get
			{
				if (_authotizationCommand == null)
				{
					_authotizationCommand = new RelayCommand(ExecuteAuthotizationCommand,
						CanExecuteAuthotizationCommand);
				}
				return _authotizationCommand;
			}
		}
		public bool CanExecuteAuthotizationCommand(object parameter)
		{
			//если в представлении textbox пустые, то выполения команды не возможно
			if (string.IsNullOrEmpty(LoginAndPassword.Login) ||
				string.IsNullOrEmpty(LoginAndPassword.Password))
				return false;
			return true;
		}

		
		public void ExecuteAuthotizationCommand(object parameter)
		{
			AuthorizationModel authorization = new AuthorizationModel(LoginAndPassword.Login, LoginAndPassword.Password);
			if (!authorization.CheckLogin())
			{
				_dialogManager.ShowMessage($"Пользователя с логином {LoginAndPassword.Login}не существует");
				LoginAndPassword.Login = null;
				LoginAndPassword.Password = null;
				return;
			}

			if (!authorization.CheckAccessLevel())
			{
				_dialogManager.ShowMessage("Недостаточно прав для входа в приложение");
				LoginAndPassword.Login = null;
				LoginAndPassword.Password = null;
				return;
			}


			if (!authorization.CheckPassword())
			{
				_dialogManager.ShowMessage("Неправильный пароль");
				LoginAndPassword.Password = null;
				return;
			}
			_user = authorization.GetUser;
			GoNext();
		}

		private void GoNext()
		{
			UserSession.Instance(_user.UserId);
			_navigationManager.Navigate(NavigationKeys.MainWindow, _user);
		}
		#endregion
	}
}
