using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
namespace ElectronicJournal_Desktop.ViewModel
{
	public class ManagingUserAccountsViewModel : ViewModelBase
	{
		#region Fields

		private readonly INavigationManager _navigationManager;

		#endregion

		#region Constructor

		public ManagingUserAccountsViewModel(INavigationManager navigationManager)
		{
			_navigationManager = navigationManager;
		}

		#endregion
	}
}
