using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class FullInfoLessonViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		ViewGroupsLessons _lessonInfo;
		#endregion

		#region Constructor

		public FullInfoLessonViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}
		#endregion

		#region Инициализация
		public void OnNavigatedTo(object arg)
		{
			if (arg is ViewGroupsLessons)
			{
				_lessonInfo = (ViewGroupsLessons)arg;
			}
			else
				throw new ArgumentException();
		}
		#endregion

		#region Bindings

		public string SubjectName => _lessonInfo.SubjectName;
		public string LessonType => _lessonInfo.LessonType;
		public string LessonDate => _lessonInfo.DateLesson;
		public string LessonTime => _lessonInfo.TimeLesson;
		public string Teachers => _lessonInfo.Teachers;
		public string Classroom => _lessonInfo.Classroom;
		#endregion

		#region GoBack
		public ICommand GoBackCommand
		{
			get 
			{
				return new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.LessonListView, _lessonInfo.GroupId)); 
			}
		}
		#endregion
		#region Delete
		RelayCommand _deleteLesson;
		public ICommand DeleteLessonCommand
		{
			get
			{
				if (_deleteLesson == null)
					_deleteLesson = new RelayCommand(ExecuteDeleteLessonCommand);
				return _deleteLesson;
			}
		}

		void ExecuteDeleteLessonCommand(object p)
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				Lessons delLes = db.Lessons.Find(_lessonInfo.LessonId);
				db.Lessons.Remove(delLes);
				db.SaveChanges();
			}
			_dialogManager.ShowMessage("Занятие удалено");
			_navigationManager.Navigate(NavigationKeys.LessonListView, _lessonInfo.GroupId);
		}
		#endregion



	}
}
