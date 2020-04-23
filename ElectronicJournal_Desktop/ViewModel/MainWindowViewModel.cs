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
				return _user.LastName + " " + _user.FirstName + " " + User.MiddleName;
			}
		}
		public string AccessLevel
		{
			get
			{
				string alName = string.Empty;
				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
				{
					var al = from accesslevel in db.AccessLevels
							 where accesslevel.AccessLevelId == User.AccessLevelId
							 select accesslevel.AccessLevelName;
					foreach (var item in al)
					{
						alName = item;
					}
				}
				return alName;
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

		public void OnNavigatedTo(object arg)
		{
			if (arg is Users)
				User = (Users)arg;
			else
				throw new ArgumentException();
		}

	}
}
