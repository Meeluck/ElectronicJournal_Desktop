using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class ManagingUserAccountsViewModel : ViewModelBase
	{
		#region Fields

		private readonly INavigationManager _navigationManager;
		ObservableCollection<GeneralUserInformation> _usersInformation;
		ManagingUserAccountsModel _managingUserAccounts;
		#endregion

		#region Constructor

		public ManagingUserAccountsViewModel(INavigationManager navigationManager)
		{
			_navigationManager = navigationManager;
		}

		#endregion

		#region Отображение списка аккаунтов
		
		public ObservableCollection<GeneralUserInformation> UserInformation
		{
			get
			{
				if (_usersInformation == null)
				{
					if (UserSession.AccessLevelId == 4)
					{
						_managingUserAccounts = new ManagingUserAccountsModel();
						_managingUserAccounts.SetUserListRequestBehavior(new DataRequestForDekanat());
						_usersInformation = _managingUserAccounts.GetUsersCollection;
					}
					else
					{
						_managingUserAccounts = new ManagingUserAccountsModel();
						_managingUserAccounts.SetUserListRequestBehavior(new DataRequestForAdmin());
						_usersInformation = _managingUserAccounts.GetUsersCollection;
					}
				}
				return _usersInformation;
			}
		}


		GeneralUserInformation _selectedUser;

		public GeneralUserInformation SelectedUser
		{
			get { return _selectedUser; }
			set
			{
				_selectedUser = value;
				OnPropertyChanged("SelectedUser");
			}
		}

		#endregion


		#region Показать подробную информацию о пользователе
		RelayCommand _fullInfo;

		public ICommand ShowFullInfoCommand
		{
			get
			{
				if (_fullInfo == null)
				{
					_fullInfo = new RelayCommand(ExecuteShowFullInfoCommand, CanExecuteShowFullInfoCommand);
				}
				return _fullInfo;
			}
		}
		
		private bool CanExecuteShowFullInfoCommand(object parametr)
		{
			return SelectedUser != null;
		}

		private void ExecuteShowFullInfoCommand(object parametr)
		{
			switch (SelectedUser.AccessLevelName)
			{
				case "Студент":
					GoFullInfoStudent();
					break;
				case "Староста":
					GoFullInfoStudent();
					break;
				case "Преподаватель":
					GoFullInfoTeacher();
					break;
				case "Деканат":

					break;
				case "Админ":

					break;

				default:
					break;
			}
		}

		private void GoFullInfoStudent()
		{
			_navigationManager.Navigate(NavigationKeys.FullInfoStudentView, SelectedUser.UserId);
		}
		private void GoFullInfoTeacher()
		{
			_navigationManager.Navigate(NavigationKeys.FullInfoTeacherView, SelectedUser.UserId);
		}
		#endregion

	}
}
