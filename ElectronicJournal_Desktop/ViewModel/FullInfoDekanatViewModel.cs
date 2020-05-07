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

		#endregion

		#region Constructor

		public FullInfoDekanatViewModel(INavigationManager navigationManager)
		{
			_navigationManager = navigationManager;
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

		public void OnNavigatedTo(object arg)
		{
			if (arg is int)
			{
				_managingUser = new ManagingUserAccountsModel();
				_fullInfo = _managingUser.FullInfoDekanat(Convert.ToInt32(arg));
			}
			else
				throw new ArgumentException();
		}
	}
}
