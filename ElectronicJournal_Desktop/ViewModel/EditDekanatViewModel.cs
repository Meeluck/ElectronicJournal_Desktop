using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class EditDekanatViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		FullInfoDekanat _fullDekanatInfo;
		FullInfoAdmin _fullAdminInfo;
		Users _fullInfo;
		ManagingUserAccountsModel _managingUser;
		DialogManager _dialogManager;
		#endregion

		#region Constructor

		public EditDekanatViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bingings

		public string LastName
		{
			get { return _fullInfo.LastName; }
			set
			{
				_fullInfo.LastName = value;
				OnPropertyChanged("LastName");
			}
		}

		public string FirstName
		{
			get { return _fullInfo.FirstName; }
			set
			{
				_fullInfo.FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}

		public string MiddleName
		{
			get { return _fullInfo.MiddleName; }
			set
			{
				_fullInfo.MiddleName = value;
				OnPropertyChanged("MiddleName");
			}
		}

		public string Phone
		{
			get { return _fullInfo.Phone; }
			set
			{
				_fullInfo.Phone = value;
				OnPropertyChanged("Phone");
			}
		}

		public string Email
		{
			get { return _fullInfo.Email; }
			set
			{
				_fullInfo.Email = value;
				OnPropertyChanged("Email");
			}
		}

		#endregion

		#region Редактирование

		RelayCommand _editCommand;

		public ICommand EditCommand
		{
			get
			{
				if (_editCommand == null)
				{
					_editCommand = new RelayCommand(ExecuteEditCommand, CanExecuteEditCommand);
				}
				return _editCommand;
			}
		}
		public void ExecuteEditCommand(object p)
		{
			_managingUser.EditDekanat(_fullInfo);
			if (_fullInfo.AccessLevelId == 4)
			{
				_fullDekanatInfo.LastName = _fullInfo.LastName;
				_fullDekanatInfo.FirstName = _fullInfo.FirstName;
				_fullDekanatInfo.MiddleName= _fullInfo.MiddleName;
				_fullDekanatInfo.Phone = _fullInfo.Phone;
				_fullDekanatInfo.Email = _fullInfo.Email;

				_navigationManager.Navigate(NavigationKeys.FullInfoDekanatView, _fullDekanatInfo);
			}
			else
			{
				_fullAdminInfo.LastName = _fullInfo.LastName;
				_fullAdminInfo.FirstName = _fullInfo.FirstName;
				_fullAdminInfo.MiddleName = _fullInfo.MiddleName;
				_fullAdminInfo.Phone = _fullInfo.Phone;
				_fullAdminInfo.Email = _fullInfo.Email;
				_navigationManager.Navigate(NavigationKeys.FullInfoAdminView, _fullAdminInfo);
			}
		}
		public bool CanExecuteEditCommand(object p)
		{
			if (string.IsNullOrEmpty(LastName) ||
				string.IsNullOrEmpty(FirstName))
				return false;
			return true;
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
			if(arg is FullInfoDekanat)
			{
				_fullInfo = (Users)arg;
				_fullDekanatInfo = (FullInfoDekanat)arg;
				_managingUser = new ManagingUserAccountsModel();
			}	
			else if(arg is FullInfoAdmin)
			{
				_fullInfo = (Users)arg;
				_fullAdminInfo = (FullInfoAdmin)arg;
				_managingUser = new ManagingUserAccountsModel();

			}
			else
				throw new ArgumentException();
		}



	}
}
