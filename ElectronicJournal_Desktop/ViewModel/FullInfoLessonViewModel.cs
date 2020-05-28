using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Infrastructure;
using System;
using System.Collections.Generic;
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



	}
}
