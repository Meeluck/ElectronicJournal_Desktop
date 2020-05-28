using System;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using System.Collections.Generic;
using ElectronicJournal_Desktop.Context;
using System.Linq;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class ManagingScheduleViewModel : ViewModelBase
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		List<Groups> _groupsList;
		Groups _selectedGroup;

		#endregion

		#region Constructor

		public ManagingScheduleViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Bindings

		public List<Groups> GroupsList
		{
			get
			{
				if(_groupsList == null)
				{
					using (ElectronicalJournalContext db = new ElectronicalJournalContext())
					{
						_groupsList = db.Groups.ToList();
					}
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

		#region Просмотр занятий группы

		RelayCommand _viewLesson;

		public ICommand ViewLessonCommand
		{
			get
			{
				if (_viewLesson == null)
					_viewLesson = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.LessonListView, SelectedGroup.GroupId),
					(p)=> SelectedGroup == null ? false : true);
				return _viewLesson;
			}
		}

		#endregion

		#region Добавление занятие
		public ICommand AddNewLessonCommand
		{
			get
			{
				return new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.AddNewLessonView, SelectedGroup.GroupId),
					(p) => SelectedGroup == null ? false : true);
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

	}
}
