using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class FullInfoAdminViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		FullInfoAdmin _fullInfo;
		ManagingUserAccountsModel _managingUser;
		DialogManager _dialogManager;
		#endregion

		#region Constructor

		public FullInfoAdminViewModel (INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bindings

		public string FullName
		{
			get { return _fullInfo.LastName + " " + _fullInfo.FirstName + " " + _fullInfo.MiddleName; }
		}

		public string AccessLevelName
		{
			get { return _fullInfo.AcessLevelName; }
		}
		public string Email
		{
			get { return _fullInfo.Email; }
		}
		public string Phone
		{
			get { return _fullInfo.Phone; }
		}


		#endregion

		#region Удаление

		RelayCommand _deleteCommand;

		public ICommand DeleteCommand
		{
			get
			{
				if (_deleteCommand == null)
				{
					_deleteCommand = new RelayCommand(ExecuteDeleteUserCommand);
				}
				return _deleteCommand;
			}
		}

		void ExecuteDeleteUserCommand(object p)
		{
			_managingUser.DeleteUser(_fullInfo);
			_dialogManager.ShowMessage("Пользователь удален");
			_navigationManager.Navigate(NavigationKeys.ManagingUserAccountsView);
		}
		#endregion

		#region Редакитрование

		RelayCommand _editCommand;

		public ICommand EditCommand
		{
			get
			{
				if (_editCommand == null)
				{
					_editCommand = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.EditDekanatView, _fullInfo));
				}
				return _editCommand;
			}
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

		public void OnNavigatedTo(object arg)
		{
			if (arg is int)
			{
				_managingUser = new ManagingUserAccountsModel();
				_fullInfo = _managingUser.FullInfoAdmin(Convert.ToInt32(arg));
			}
			else if(arg is FullInfoAdmin)
			{
				_managingUser = new ManagingUserAccountsModel();
				_fullInfo = (FullInfoAdmin)arg;
			}
			else
				throw new ArgumentException();
		}
	}
}
