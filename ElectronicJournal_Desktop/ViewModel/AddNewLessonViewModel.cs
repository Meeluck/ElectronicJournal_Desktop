using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Model;
using ElectronicJournal_Desktop.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows.Input;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class TeachersModelForAddNewLesson
	{
		public int TeacherId { get; set; }
		public string FullName { get; set; }
	}
	public class AddNewLessonViewModel :ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		int _groupId;
		List<Subjects> _subjectsList;
		DateTime _lessondDate;
		List<TimeSchedules> _timeSchedulesList;
		List<LessonTypes> _lessonTypesList;
		List<TeachersModelForAddNewLesson> _teachersList;
		List<Classrooms> _classroomsList;
		Subjects _selectedSubject;
		TimeSchedules _selectedTimeSchedule;
		LessonTypes _selectedLessonType;
		TeachersModelForAddNewLesson _selectedTeacher;
		Classrooms _selectedClassroom;

		#endregion

		#region Constructor

		public AddNewLessonViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Инициализация
		public void OnNavigatedTo(object arg)
		{
			if(arg is int)
			{
				_groupId = Convert.ToInt32(arg);
				_lessondDate = DateTime.Today;
				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
				{
					_subjectsList = db.Subjects.ToList();
					_timeSchedulesList = db.TimeSchedules.ToList();
					_lessonTypesList = db.LessonTypes.ToList();

					_teachersList = (from tch in db.Teachers
									 join us in db.Users on tch.UserId equals us.UserId
									 select new TeachersModelForAddNewLesson
									 {
										 TeacherId = tch.TeacherId,
										 FullName = us.LastName + " " + us.FirstName + " " 
											+ (!string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty)
									 }).ToList();
					_classroomsList = (from cl in db.Classrooms
									   join bl in db.Buildings on cl.BuildingId equals bl.BuildingId
									   into blDatail
									   from blDat in blDatail.DefaultIfEmpty()
									   select new Classrooms
									   {
										   ClassroomId = cl.ClassroomId,
										   ClassroomName = (!string.IsNullOrEmpty(blDat.BuildingName)?blDat.BuildingName:string.Empty) + " " + cl.ClassroomName
									   }).ToList();
				}
			}
			else
				throw new ArgumentException();
		}
		#endregion

		#region Bindings

		public List<Subjects> SubjectsList => _subjectsList;
		public Subjects SelectedSubject
		{
			get { return _selectedSubject; }
			set
			{
				_selectedSubject = value;
				OnPropertyChanged("SelectedSubject");
			}
		}

		public List<TimeSchedules> TimeSchedulesList => _timeSchedulesList;
		public TimeSchedules SelectedTimeSchedule
		{
			get { return _selectedTimeSchedule; }
			set
			{
				_selectedTimeSchedule = value;
				OnPropertyChanged("SelectedTimeSchedule");
			}
		}

		public List<LessonTypes> LessonTypesList => _lessonTypesList;
		public LessonTypes SelectedLessonType
		{
			get { return _selectedLessonType; }
			set
			{
				_selectedLessonType = value;
				OnPropertyChanged("SelectedLessonType");
			}
		}

		public List<TeachersModelForAddNewLesson> TeachersList => _teachersList;
		public TeachersModelForAddNewLesson SelectedTeacher
		{
			get { return _selectedTeacher; }
			set
			{
				_selectedTeacher = value;
				OnPropertyChanged("SelectedTeacher");
			}
		}

		public List<Classrooms> ClassroomsList => _classroomsList;
		public Classrooms SelectedClassroom
		{
			get { return _selectedClassroom; }
			set
			{
				_selectedClassroom = value;
				OnPropertyChanged("SelectedClassroom");
			}
		}

		public DateTime SelectedLessonDate
		{
			get { return _lessondDate; }
			set
			{
				_lessondDate = value;
				OnPropertyChanged("SelectedLessonDate");
			}
		}
		#endregion

		#region Добавление нового занятие
		RelayCommand _addNewLesson;
		public ICommand AddNewLessonCommand
		{
			get
			{
				if (_addNewLesson == null)
					_addNewLesson = new RelayCommand(ExecuteAddNewLessonCommand, CanExecuteAddNewLessonCommand);
				return _addNewLesson;
			}
		}

		void ExecuteAddNewLessonCommand(object p)
		{
			_dialogManager.ShowMessage($"{SelectedSubject.SubjectId} {SelectedSubject.SubjectName}\n" +
				$"{SelectedLessonDate}\n" +
				$"{SelectedTimeSchedule.TimeScheduleId}  {SelectedTimeSchedule.TimeInterval}\n" +
				$"{SelectedLessonType.LessonTypeId}  {SelectedLessonType.LessonTypeName}\n" +
				$"{SelectedTeacher.TeacherId}  {SelectedTeacher.FullName}\n" +
				$"{SelectedClassroom.ClassroomId}  {SelectedClassroom.ClassroomName}");

			Lessons newLesson = new Lessons
			{
				Date = SelectedLessonDate,
				TimeScheduleId = SelectedTimeSchedule.TimeScheduleId,
				SubjectId = SelectedSubject.SubjectId,
				LessonTypeId = SelectedLessonType.LessonTypeId,
				ClassroomId = SelectedClassroom.ClassroomId
			};
			

			GroupLessons groupLessons = new GroupLessons
			{
				GroupId = _groupId,
				Lesson = newLesson
			};
			TeacherLessons teacherLessons = new TeacherLessons
			{
				TeacherId = SelectedTeacher.TeacherId,
				Lesson = newLesson
			};
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				db.Lessons.Add(newLesson);
				db.GroupLessons.Add(groupLessons);
				db.TeacherLessons.Add(teacherLessons);
				db.SaveChanges();
			}
			_navigationManager.Navigate(NavigationKeys.LessonListView, _groupId);

		}
		bool CanExecuteAddNewLessonCommand(object p)
		{
			if (SelectedSubject == null || SelectedLessonDate == null)
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
					_goBack = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.LessonListView,_groupId));
				return _goBack;
			}
		}
		#endregion
	}
}
