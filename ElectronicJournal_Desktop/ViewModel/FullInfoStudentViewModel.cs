using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class FullInfoStudentViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		FullInfoStudent _fullInfo;
		ManagingUserAccountsModel _managingUser;
		DialogManager _dialogManager;
		#endregion

		#region Constructor

		public FullInfoStudentViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bindings

		public string FullName
		{
			get{ return _fullInfo.LastName + " " + _fullInfo.FirstName + " " + _fullInfo.MiddleName; }
		}
		
		public string AccessLevelName
		{
			get	{ return _fullInfo.AcessLevelName; }
		}
		public string Email
		{
			get { return _fullInfo.Email; }
		}
		public string Phone
		{
			get { return _fullInfo.Phone; }
		}
		public string GroupName
		{
			get { return _fullInfo.GroupName; }
		}

		#endregion

		#region Удаление

		RelayCommand _deleteStudentCommand;

		public ICommand DeleteStudentCommand
		{
			get
			{
				if(_deleteStudentCommand == null)
				{
					_deleteStudentCommand = new RelayCommand(ExecuteDeleteUserCommand);
				}
				return _deleteStudentCommand;
			}
		}

		void ExecuteDeleteUserCommand(object p)
		{
			_managingUser.DeleteUser(_fullInfo);
			_dialogManager.ShowMessage("Пользователь удален");
			_navigationManager.Navigate(NavigationKeys.ManagingUserAccountsView);
		}
		#endregion

		#region Редакитрование

		RelayCommand _editStudentCommand;

		public ICommand EditStudentCommand
		{
			get
			{
				if (_editStudentCommand == null)
				{
					_editStudentCommand = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.EditStudentView, _fullInfo));
				}
				return _editStudentCommand;
			}
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
				_fullInfo = _managingUser.FullInfoStudent(Convert.ToInt32(arg));
			}
			else if(arg is FullInfoStudent)
			{
				_managingUser = new ManagingUserAccountsModel();
				_fullInfo = (FullInfoStudent)arg;
			}
			else
				throw new ArgumentException();
		}
	}
}
