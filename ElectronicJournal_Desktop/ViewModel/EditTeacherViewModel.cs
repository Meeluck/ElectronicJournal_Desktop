using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using System.Collections.ObjectModel;
using ElectronicJournal_Desktop.Model.Data;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class EditTeacherViewModel :ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		FullInfoTeacher _fullInfo;
		ManagingUserAccountsModel _managingUser;
		DialogManager _dialogManager;
		ObservableCollection<Positions> _positionsList;
		Positions _position;

		#endregion

		#region Constructor

		public EditTeacherViewModel(INavigationManager navigationManager, DialogManager dialogManager)
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
		public ObservableCollection<Positions> PositionsList
		{
			get { return _positionsList; }
		}
		public string PositionName
		{
			get { return _fullInfo.PositionName; }
		}
		public Positions Position
		{
			get
			{
				if (_position == null)
				{
					_position = new Positions();
					_position.PositionId = Convert.ToInt32(_fullInfo.PositionId);
					_position.PositionName = _fullInfo.PositionName;
				}
				return _position;
			}
			set
			{
				_position = value;
				_fullInfo.PositionId = _position.PositionId;
				_fullInfo.PositionName = _position.PositionName;
				OnPropertyChanged("Position");
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
			_managingUser.EditTeacher(_fullInfo);
			_navigationManager.Navigate(NavigationKeys.FullInfoTeacherView, _fullInfo);
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

		#region Навигационное свойство 
		public void OnNavigatedTo(object arg)
		{
			if (arg is FullInfoTeacher)
			{
				_fullInfo = (FullInfoTeacher)arg;
				_managingUser = new ManagingUserAccountsModel();
				_positionsList = _managingUser.GetPositions;
			}
			else
				throw new ArgumentException();
		}
		#endregion
	}
}
