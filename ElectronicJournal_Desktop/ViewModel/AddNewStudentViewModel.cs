using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using System.Collections.ObjectModel;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class AddNewStudentViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		Users _student;
		ObservableCollection<Groups> _groupsList;
		ManagingUserAccountsModel _managingUser;
		Groups _group;
		#endregion

		#region Constructor

		public AddNewStudentViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bindings

		public string FullName
		{
			get
			{
				return _student.LastName + " " + _student.FirstName + " " +
					(!string.IsNullOrEmpty(_student.MiddleName) ? _student.MiddleName : string.Empty);			
			}
		}

		public ObservableCollection<Groups> GroupsList
		{
			get
			{
				if (_managingUser == null)
					_managingUser = new ManagingUserAccountsModel();
				_groupsList = _managingUser.GetGroups;
				return _groupsList;
			}
		}
		public Groups Groups
		{
			get
			{
				return _group;
			}
			set
			{
				_group = value;
				OnPropertyChanged("Groups");
			}
		}

		#endregion

		#region Связывание студента с группой
		RelayCommand _connectStudentGroupCommand;

		public ICommand ConnectStudentGroupCommand
		{
			get
			{
				if(_connectStudentGroupCommand==null)
				{
					_connectStudentGroupCommand = new RelayCommand(ExecuteConnectStudentGroup);
				}
				return _connectStudentGroupCommand;
			}
		}

		public void ExecuteConnectStudentGroup(object p)
		{
			if (_group != null)
			{
				_managingUser = new ManagingUserAccountsModel();
				_managingUser.ConnectStudentWithGroup(_student.Login, _group.GroupId);
			}

			_navigationManager.Navigate(NavigationKeys.ManagingUserAccountsView);

		}

		#endregion

		#region Пропустить добавление группы

		RelayCommand _missAdd;
		public ICommand MissAddCommand
		{
			get
			{
				if (_missAdd == null)
				{
					_missAdd = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.ManagingUserAccountsView));
				}
				return _missAdd;
			}
		}

		#endregion

		#region BackCommand
		RelayCommand _goBackCommand;

		public ICommand GoBackCommand
		{
			get
			{
				if (_goBackCommand == null)
				{
					_goBackCommand = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.AddNewUsersView));
				}
				return _goBackCommand;
			}
		}
		#endregion


		public void OnNavigatedTo(object arg)
		{
			if (arg is Users)
			{
				_student = (Users)arg;
			}
			else
				throw new ArgumentException();
		}
	}
}
