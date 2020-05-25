using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Model.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class AddNewGroupViewModel : ViewModelBase
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		string _groupName;
		DateTime _date;
		#endregion

		#region Ctor

		public AddNewGroupViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}
		#endregion

		#region Bindings

		public string GroupName
		{
			get { return _groupName; }
			set
			{
				_groupName = value;
				OnPropertyChanged(GroupName);
			}
		}

		public DateTime DateFormation
		{
			get { return _date; }
			set
			{
				_date = value;
				OnPropertyChanged("DateFormation");
			}
		}

		#endregion

		#region Добавление группы

		RelayCommand _addGroup;

		public ICommand AddGroupCommand
		{
			get
			{
				if(_addGroup == null)
				{
					_addGroup = new RelayCommand(ExecuteAddGroupCommand, CanExecuteAddGroupCommand);
				}
				return _addGroup;
			}
		}
		void ExecuteAddGroupCommand(object p)
		{
			
			Groups newGroup = new Groups();
			
			newGroup.GroupLessons = null;
			newGroup.StudentGroups = null;

			newGroup.GroupName = GroupName;
			newGroup.YearFormationGroup = DateFormation;

			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				db.Groups.Add(newGroup);
				db.SaveChanges();
			}

			_navigationManager.Navigate(NavigationKeys.ManagingGroupsView);
		}
		bool CanExecuteAddGroupCommand(object p)
		{
			if (string.IsNullOrEmpty(GroupName) || _date == null)
				return false;
			return true;
		}

		#endregion

		#region Назад

		RelayCommand _goBack;

		public ICommand GoBackCommand
		{
			get
			{
				if (_goBack == null)
					_goBack = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.ManagingGroupsView));
				return _goBack;
			}
		}

		#endregion
	}
}
