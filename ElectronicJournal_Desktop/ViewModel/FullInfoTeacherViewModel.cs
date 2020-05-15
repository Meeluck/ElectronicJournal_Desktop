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
	public class FullInfoTeacherViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		private readonly INavigationManager _navigationManager;
		FullInfoTeacher _fullInfo;
		ManagingUserAccountsModel _managingUserAccounts;
		DialogManager _dialogManager;
		#endregion

		#region Constructor

		public FullInfoTeacherViewModel(INavigationManager navigationManager, DialogManager dialogManager)
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
		public string Position
		{
			get { return _fullInfo.PositionName; }
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
				_managingUserAccounts = new ManagingUserAccountsModel();
				_fullInfo = _managingUserAccounts.FullInfoTeacher(Convert.ToInt32(arg));
			}
			else
				throw new ArgumentException();
		}
	}
}
