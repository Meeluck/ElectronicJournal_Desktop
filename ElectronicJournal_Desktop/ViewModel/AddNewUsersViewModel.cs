using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using ElectronicJournal_Library;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class AddNewUsersViewModel : ViewModelBase
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		ObservableCollection<GeneralUserInformation> _usersInformation;
		ManagingUserAccountsModel _managingUserAccounts;
		readonly DialogManager _dialogManager;
		Users _newUser;
		Password _password;
		string _pswrd;
		ObservableCollection<AccessLevels> _accessLevels;

		#endregion

		#region Constructor

		public AddNewUsersViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bindings

		public string LastName
		{
			get
			{
				if (_newUser == null)
					_newUser = new Users();
				return _newUser.LastName;
			}
			set
			{
				_newUser.LastName = value;
				OnPropertyChanged("LastName");
			}
		}

		public string FirstName
		{
			get
			{
				if (_newUser == null)
					_newUser = new Users();
				return _newUser.FirstName;
			}
			set
			{
				_newUser.FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		public string MiddleName
		{
			get
			{
				if (_newUser == null)
					_newUser = new Users();
				return _newUser.MiddleName;
			}
			set
			{
				_newUser.MiddleName = value;
				OnPropertyChanged("MiddleName");
			}
		}
		public string Login
		{
			get
			{
				if (_newUser == null)
					_newUser = new Users();
				return _newUser.Login;
			}
			set
			{
				_newUser.Login = value;
				OnPropertyChanged("Login");
			}
		}
		public string Password
		{
			get
			{
				if (_pswrd == null)
					_pswrd = string.Empty;
				return _pswrd;
			}
			set
			{
				_password = new Password(value);
				_pswrd = value;
				_newUser.PasswordSalt = _password.PasswordSalt;
				_newUser.PasswordHash = _password.PasswordHash;
				OnPropertyChanged("Password");
			}
		}

		public ObservableCollection<AccessLevels> AccessLevelsList
		{
			get
			{
				if (_accessLevels == null)
				{
					_managingUserAccounts = new ManagingUserAccountsModel();
					_accessLevels = _managingUserAccounts.AccessLevelsList;
				}
				return _accessLevels;
			}
		}

		public AccessLevels AccessLevel
		{
			get
			{
				if (_newUser == null)
					_newUser = new Users();
				return _newUser.AccessLevel;
			}
			set
			{
				_newUser.AccessLevel = value;
				_newUser.AccessLevelId = value.AccessLevelId;
				OnPropertyChanged("AccessLevel");
			}
		}

		public string Phone
		{
			get
			{
				if (_newUser == null)
					_newUser = new Users();
				return _newUser.Phone;
			}
			set
			{
				_newUser.Phone = value;
				OnPropertyChanged("Phone");
			}
		}

		public string Email
		{
			get
			{
				if (_newUser == null)
					_newUser = new Users();
				return _newUser.Email;
			}
			set
			{
				_newUser.Email = value;
				OnPropertyChanged("Email");
			}
		}

		#endregion

		#region Добавление пользователя

		RelayCommand _addNewUserCommand;

		public ICommand AddNewUserCommand
		{
			get
			{
				
				if (_addNewUserCommand == null)
				{
					_addNewUserCommand = new RelayCommand(ExecuteAddNewUserCommand,CanExecuteAddNewUserCommand);
				}
				return _addNewUserCommand;
			}
		}

		bool CanExecuteAddNewUserCommand(object p)
		{
			if (string.IsNullOrEmpty(LastName) ||
				string.IsNullOrEmpty(FirstName) ||
				string.IsNullOrEmpty(Login) ||
				string.IsNullOrEmpty(Password))
				return false;
			return true;
		}

		void ExecuteAddNewUserCommand(object p)
		{
			if(_managingUserAccounts == null)
				_managingUserAccounts = new ManagingUserAccountsModel();

			_managingUserAccounts.AddNewUsers(_newUser);

			switch (_newUser.AccessLevelId)
			{
				case 1:
					_navigationManager.Navigate(NavigationKeys.AddNewStudentView, _newUser);
					break;
				case 2:
					_navigationManager.Navigate(NavigationKeys.AddNewStudentView, _newUser);
					break;
				case 3:
					_navigationManager.Navigate(NavigationKeys.AddNewTeacherView, _newUser);
					break;
				case 4:

					break;
				case 5:

					break;
				default:
					break;
			}

		}
		void GoToAddNewUser()
		{
			_navigationManager.Navigate(NavigationKeys.AddNewStudentView, _newUser);
		}

		#endregion

		#region Назад

		RelayCommand _goBackCommand;

		public ICommand GoBackCommand
		{
			get
			{
				if (_goBackCommand == null)
				{
					_goBackCommand = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.ManagingUserAccountsView));
				}
				return _goBackCommand;
			}
		}
		#endregion

	}
}
