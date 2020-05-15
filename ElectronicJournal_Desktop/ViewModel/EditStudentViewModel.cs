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
	public class EditStudentViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		FullInfoStudent _fullInfoStudent;
		ManagingUserAccountsModel _managingUser;
		DialogManager _dialogManager;
		ObservableCollection<AccessLevels> _accessLevelsList;
		ObservableCollection<Groups> _groupsList;
		Groups _group;
		#endregion

		#region Constructor

		public EditStudentViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bingings

		public string LastName
		{
			get { return _fullInfoStudent.LastName; }
			set
			{
				_fullInfoStudent.LastName = value;
				OnPropertyChanged("LastName");
			}
		}

		public string FirstName
		{
			get { return _fullInfoStudent.FirstName; }
			set
			{
				_fullInfoStudent.FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}

		public string MiddleName
		{
			get { return _fullInfoStudent.MiddleName; }
			set
			{
				_fullInfoStudent.MiddleName = value;
				OnPropertyChanged("MiddleName");
			}
		}
		public ObservableCollection<AccessLevels> AccessLevelsList
		{
			get { return _accessLevelsList; }
		}

		public string AccessLevelName
		{
			get
			{
				return _fullInfoStudent.AcessLevelName;
			}
		}

		public AccessLevels AccessLevel
		{
			get { return _fullInfoStudent.AccessLevel; }
			set
			{
				_fullInfoStudent.AccessLevel = value;
				_fullInfoStudent.AccessLevelId = value.AccessLevelId;
				_fullInfoStudent.AcessLevelName = value.AccessLevelName;
				OnPropertyChanged("AccessLevel");
			}
		}

		public string Phone
		{
			get { return _fullInfoStudent.Phone; }
			set
			{
				_fullInfoStudent.Phone = value;
				OnPropertyChanged("Phone");
			}
		}

		public string Email
		{
			get { return _fullInfoStudent.Email; }
			set
			{
				_fullInfoStudent.Email = value;
				OnPropertyChanged("Email");
			}
		}
		public ObservableCollection<Groups> GroupsList
		{
			get { return _groupsList; }
		} 
		public string GroupName
		{
			get { return _fullInfoStudent.GroupName; }
		}
		public Groups Group
		{
			get
			{
				if(_group == null)
				{
					_group = new Groups();
					_group.GroupId = Convert.ToInt32(_fullInfoStudent.GroupId);
					_group.GroupName = _fullInfoStudent.GroupName;
				}
				return _group;
			}
			set
			{
				_group = value;
				_fullInfoStudent.GroupId = _group.GroupId;
				_fullInfoStudent.GroupName = _group.GroupName;
				OnPropertyChanged("Group");
			}
		}
		#endregion


		#region Редактирование 
		RelayCommand _editStudentCommand;

		public ICommand EditStudentCommand
		{
			get
			{
				if (_editStudentCommand == null)
				{
					_editStudentCommand = new RelayCommand((p)=> _managingUser.EditStudent(_fullInfoStudent), CanExecuteEditStudentCommand);
				}
				return _editStudentCommand;
			}
		}
		public void Test(object p)
		{
			_dialogManager.ShowMessage(_fullInfoStudent.LastName + "\n" +
				_fullInfoStudent.FirstName + "\n"+
				_fullInfoStudent.MiddleName + "\n"+
				_fullInfoStudent.AcessLevelName + "\n" +
				_fullInfoStudent.Phone + "\n" +
				_fullInfoStudent.Email + "\n" +
				_fullInfoStudent.GroupName);
		}
		public bool CanExecuteEditStudentCommand(object p)
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
			if (arg is FullInfoStudent)
			{
				_fullInfoStudent = (FullInfoStudent)arg;
				_managingUser = new ManagingUserAccountsModel();
				_accessLevelsList = _managingUser.AccessLevelsStudentList;
				_groupsList = _managingUser.GetGroups;
			}
			else
				throw new ArgumentException();
			#endregion
		}

	}
}
