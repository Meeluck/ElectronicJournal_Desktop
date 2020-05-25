using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using ElectronicJournal_Desktop.Context;
using System.Collections.ObjectModel;

namespace ElectronicJournal_Desktop.ViewModel
{
	
	public class ManagingGroupsViewModel :ViewModelBase
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		ObservableCollection<Groups> _groupsList;
		Groups _selectedGroup;
		#endregion

		#region Constructor

		public ManagingGroupsViewModel( INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Отображение групп

		public ObservableCollection<Groups> GroupsList
		{
			get
			{
				if (_groupsList == null)
				{
					ManagingUserAccountsModel _managingUserAccounts = new ManagingUserAccountsModel();
					_groupsList = _managingUserAccounts.GetGroups;
				}
				return _groupsList;
			}
		}

		public Groups SelectedGroup
		{
			get { return _selectedGroup; }
			set
			{
				_selectedGroup = value;
				OnPropertyChanged("SelectedGroup");
			}
		}

		#endregion


		#region Добавлнеие новой группы

		RelayCommand _addGroup;

		public ICommand AddGroupCommand
		{
			get
			{
				if (_addGroup == null)
				{
					_addGroup = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.AddNewGroupView));
				}
				return _addGroup;
			}
		}

		#endregion

		#region Назад

		RelayCommand _goBack;

		public ICommand GoBackCommand
		{
			get
			{
				if (_goBack == null)
					_goBack = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.MainWindow));
				return _goBack;
			}
		}

		#endregion

		#region Отображение подробной информации о группе

		RelayCommand _fullInfoGroup;

		public ICommand FullInfoGroupCommand
		{
			get
			{
				if (_fullInfoGroup == null)
					_fullInfoGroup = new RelayCommand((p)=>_navigationManager.Navigate(NavigationKeys.FullInfoGroupView,_selectedGroup.GroupId),
														(p) => (_selectedGroup == null ? false : true));
				return _fullInfoGroup;
			}
		}


		#endregion
	}
}
