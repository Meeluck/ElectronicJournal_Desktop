using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class FullInfoDekanatViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		FullInfoDekanat _fullInfo;
		ManagingUserAccountsModel _managingUser;
		DialogManager _dialogManager;
		#endregion

		#region Constructor

		public FullInfoDekanatViewModel(INavigationManager navigationManager, DialogManager dialogManager)
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

		#region Удаление

		RelayCommand _deleteDekanatCommand;

		public ICommand DeleteDekanatCommand
		{
			get
			{
				if (_deleteDekanatCommand == null)
				{
					_deleteDekanatCommand = new RelayCommand(ExecuteDeleteUserCommand);
				}
				return _deleteDekanatCommand;
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

		public void OnNavigatedTo(object arg)
		{
			if (arg is int)
			{
				_managingUser = new ManagingUserAccountsModel();
				_fullInfo = _managingUser.FullInfoDekanat(Convert.ToInt32(arg));
			}
			else if (arg is FullInfoDekanat)
			{
				_managingUser = new ManagingUserAccountsModel();
				_fullInfo = (FullInfoDekanat)arg;
			}
			else
				throw new ArgumentException();
		}
	}
}
