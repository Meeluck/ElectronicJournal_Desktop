using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class EditGroupViewModel :ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		Groups _group;
		
		#endregion

		#region Constructor

		public EditGroupViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bingings

		public string GroupName
		{
			get { return _group.GroupName; }
			set
			{
				_group.GroupName = value;
				OnPropertyChanged("GroupName");
			}
		}
		public DateTime YearFormation
		{
			get { return _group.YearFormationGroup; }
			set
			{
				_group.YearFormationGroup = value;
				OnPropertyChanged("YearFormation");
			}
		}

		#endregion

		#region EditCommand

		RelayCommand _editGroup;
		public ICommand EditGroupCommand
		{
			get
			{
				if (_editGroup == null)
					_editGroup = new RelayCommand(ExecuteEditGroupCommand, CanExecuteEditGroupCommand);
				return _editGroup;
			}
		}
		void ExecuteEditGroupCommand (object p)
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				db.Groups.Update(_group);
				db.SaveChanges();
			}
			_navigationManager.Navigate(NavigationKeys.FullInfoGroupView, _group.GroupId);
		}
		bool CanExecuteEditGroupCommand(object p)
		{
			if (string.IsNullOrEmpty(GroupName) || YearFormation == null)
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

		#region Инициализация
		public void OnNavigatedTo(object arg)
		{
			if(arg is Groups)
			{
				_group = (Groups)arg;
			}
			else
				throw new ArgumentException();
		}
		#endregion

	}
}
