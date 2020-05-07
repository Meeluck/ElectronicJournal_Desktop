using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using ElectronicJournal_Desktop.Context;
using System.Linq;
namespace ElectronicJournal_Desktop.ViewModel
{
	public class MainWindowViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields
		private readonly INavigationManager _navigationManager;
		private Users _user;
		private UserSession _userSession;
		#endregion

		#region Constructor

		public MainWindowViewModel(INavigationManager navigationManager)
		{
			_navigationManager = navigationManager;
		}

		#endregion


		#region Property

		public string GetName
		{
			get
			{
				return UserSession.GetName;
			}
		}
		public string AccessLevel
		{
			get
			{
				return UserSession.AccessLevelName;
			}
		}
		public Users User
		{
			get
			{
				if (_user == null)
					_user = new Users();
				return _user;
			}
			set
			{
				_user = value;
				OnPropertyChanged("User");
			}
		}

		#endregion

		#region Commands

		#region Управление аккаунтами (переход)
		RelayCommand gotoManagingUserAccountsView;

		public ICommand GoToManagingUserAccountsViewCommand
		{
			get
			{
				if(gotoManagingUserAccountsView == null)
				{
					gotoManagingUserAccountsView = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.ManagingUserAccountsView));
				}
				return gotoManagingUserAccountsView;
			}
		}
		#endregion


		#region Управление группами (переход)
		RelayCommand gotoManagingGroupsView;
		
		public ICommand GoToManagingGroupsViewCommand
		{
			get
			{
				if (gotoManagingGroupsView == null)
				{
					gotoManagingGroupsView = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.ManagingGroupsView));
				}
				return gotoManagingGroupsView;
			}
		}
		#endregion

		#region Управление расписание (переход)

		RelayCommand gotoManagingScheduleView;

		public ICommand GoToManagingScheduleViewCommand
		{
			get
			{
				if (gotoManagingScheduleView == null)
				{
					gotoManagingScheduleView = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.ManagingScheduleView));
				}
				return gotoManagingScheduleView;
			}
		}

		#endregion

		#region Формирование отчетов (переход)
		RelayCommand gotoReportingView;

		public ICommand GoTogotoReportingViewCommand
		{
			get
			{
				if (gotoReportingView == null)
				{
					gotoReportingView = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.ReportingView));
				}
				return gotoReportingView;
			}
		}
		#endregion

		#endregion

		public void OnNavigatedTo(object arg)
		{
			if (arg is Users)
			{
				User = (Users)arg;
				_userSession = UserSession.Instance(User.UserId);
			}
			else
				throw new ArgumentException();
		}

	}
}
