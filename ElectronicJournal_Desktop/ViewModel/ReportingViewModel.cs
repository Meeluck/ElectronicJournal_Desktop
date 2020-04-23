using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class ReportingViewModel : ViewModelBase
	{
		#region Fields

		private readonly INavigationManager _navigationManager;
		private readonly DialogManager _dialogManager;

		#endregion

		#region Constructor

		public ReportingViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion
	}
}
