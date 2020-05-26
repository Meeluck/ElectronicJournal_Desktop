using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicJournal_Desktop.Constants;
using System.Windows.Input;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class FullInfoGroupViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		int _groupId;
		int _course;
		Groups _group;
		List<GeneralUserInformation> _students;

		#endregion

		#region Constructor

		public FullInfoGroupViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bindings

		public string GroupName => _group.GroupName;
		public string YearFromation => _group.YearFormationGroup.ToLongDateString();
		public int Course => _course;
		public List<GeneralUserInformation> StudentsOfGroup => _students;

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

		#region Удаление группы

		RelayCommand _deleteGroup;

		public ICommand DeleteGroupCommand
		{
			get
			{
				if (_deleteGroup == null)
					_deleteGroup = new RelayCommand(ExecuteDeleteGroupCommand);
				return _deleteGroup;
			}
		}

		void ExecuteDeleteGroupCommand(object p)
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				db.Groups.Remove(_group);
				db.SaveChanges();
			}
			_dialogManager.ShowMessage("Группа удалена");
			_navigationManager.Navigate(NavigationKeys.ManagingGroupsView);
		}

		#endregion

		#region Редакитроавние группы

		RelayCommand _editGroup;

		public ICommand EditGroupCommand
		{
			get
			{
				if (_editGroup == null)
					_editGroup = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.EditGroupView,_group));
				return _editGroup;
			}
		}

		#endregion

		#region Инициализация 
		public void OnNavigatedTo(object arg)
		{
			if(arg is int)
			{
				_groupId = Convert.ToInt32(arg);
				_group = new Groups();
				_students = new List<GeneralUserInformation>();

				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
				{
					_group = db.Groups.Find(_groupId);

					_students = (from us in db.Users
								   join sg in db.StudentGroups on us.UserId equals sg.UserId
								   join al in db.AccessLevels on us.AccessLevelId equals al.AccessLevelId
								   where sg.GroupId == _groupId
								   select new GeneralUserInformation
								   {
									   UserId = us.UserId,
									   Login = us.Login,
									   FullName = us.LastName + " " + us.FirstName + " " +
												(!string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty),
									   AccessLevelName = al.AccessLevelName
								   }).ToList();
				}

				int days = Convert.ToInt32(DateTime.Today.Subtract(_group.YearFormationGroup).TotalDays);
				_course = 0;
				while (days > 0)
				{
					days = days - 365;
					_course++;
				}
			}
			else
				throw new ArgumentException();
		}
		#endregion

	}
}
