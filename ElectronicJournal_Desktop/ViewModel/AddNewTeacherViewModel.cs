using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using ElectronicJournal_Library;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class AddNewTeacherViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		Users _teacher;
		ManagingUserAccountsModel _managingUser;
		ObservableCollection<Positions> _positionsList;
		Positions _positions;
		#endregion

		#region Constructor

		public AddNewTeacherViewModel(INavigationManager navigationManager, DialogManager dialogManager)
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
				return _teacher.LastName + " " + _teacher.FirstName + " " +
					(!string.IsNullOrEmpty(_teacher.MiddleName) ? _teacher.MiddleName : string.Empty);
			}
		}

		public ObservableCollection<Positions> PositionsList
		{
			get
			{
				_positionsList = new ObservableCollection<Positions>();
				_positionsList = _managingUser.GetPositions;
				return _positionsList;
			}
		}
		public Positions Position
		{
			get
			{
				return _positions;
			}
			set
			{
				_positions = value;
				OnPropertyChanged("Position");
			}
		}

		#endregion

		#region Добавление преподавателя

		RelayCommand _addTeacherCommand;

		public ICommand AddTeacherCommand
		{
			get
			{
				if(_addTeacherCommand == null)
				{
					_addTeacherCommand = new RelayCommand(ExecuteAddTeacherCommand, CanExecuteAddTeacherCommand);
				}
				return _addTeacherCommand;
			}
		}

		void ExecuteAddTeacherCommand(object p)
		{
			_managingUser.AddTeacher(_teacher.Login,Position.PositionId);
			_navigationManager.Navigate(NavigationKeys.ManagingUserAccountsView);
		}

		bool CanExecuteAddTeacherCommand(object p)
		{
			if (Position == null)
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

		#region Прием параметров из представлений 
		public void OnNavigatedTo(object arg)
		{
			if (arg is Users)
			{
				_teacher = (Users)arg;
				_managingUser = new ManagingUserAccountsModel();
			}
			else
				throw new ArgumentException();
		}
		#endregion
	}
}
