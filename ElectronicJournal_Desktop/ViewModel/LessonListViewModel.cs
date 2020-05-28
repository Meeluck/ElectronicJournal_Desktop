using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Desktop.Infrastructure;
using ElectronicJournal_Desktop.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ElectronicJournal_Desktop.ViewModel
{
	public class ViewGroupsLessons
	{
		public int GroupId { get; set; }
		public int LessonId { get; set; }
		public string SubjectName { get; set; }
		public string LessonType { get; set; }
		public string GroupName { get; set; }
		public string DateLesson { get; set; }
		public string TimeLesson { get; set; }
		public string Teachers { get; set; }
		public string Classroom { get; set; }
	}

	public class LessonListViewModel : ViewModelBase, INavigatedToAware
	{
		#region Fields

		readonly INavigationManager _navigationManager;
		readonly DialogManager _dialogManager;
		Groups _group;
		List<ViewGroupsLessons> _lessonsList;
		ViewGroupsLessons _selectedLessons;
		#endregion

		#region Constructor

		public LessonListViewModel(INavigationManager navigationManager, DialogManager dialogManager)
		{
			_navigationManager = navigationManager;
			_dialogManager = dialogManager;
		}

		#endregion

		#region Инициализация
		public void OnNavigatedTo(object arg)
		{
			if (arg is int)
			{
				_lessonsList = new List<ViewGroupsLessons>();
				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
				{
					_group = db.Groups.Find(Convert.ToInt32(arg));
					#region запрос данных
					List<ViewGroupsLessons> lessons = (from ls in db.Lessons
													   join sub in db.Subjects on ls.SubjectId equals sub.SubjectId
													   join lt in db.LessonTypes on ls.LessonTypeId equals lt.LessonTypeId
													   into ltDatails
													   from ltDat in ltDatails.DefaultIfEmpty()
													   join ts in db.TimeSchedules on ls.TimeScheduleId equals ts.TimeScheduleId
													   into tsDatails
													   from tsDat in tsDatails.DefaultIfEmpty()
													   join cls in db.Classrooms on ls.ClassroomId equals cls.ClassroomId
													   into clsDatail
													   from clsDat in clsDatail.DefaultIfEmpty()
													   join build in db.Buildings on clsDat.BuildingId equals build.BuildingId
													   into buildDatail
													   from buidDat in buildDatail.DefaultIfEmpty()
													   join gr_ls in db.GroupLessons on ls.LessonId equals gr_ls.LessonId
													   join gr in db.Groups on gr_ls.GroupId equals gr.GroupId
													   join tl in db.TeacherLessons on ls.LessonId equals tl.LessonId
													   join tch in db.Teachers on tl.TeacherId equals tch.TeacherId
													   join us in db.Users on tch.UserId equals us.UserId
													   where gr.GroupId == _group.GroupId
													   select new ViewGroupsLessons
													   {
														   LessonId = ls.LessonId,
														   GroupId =gr.GroupId,
														   GroupName = gr.GroupName,
														   DateLesson = ls.Date.ToLongDateString(),
														   TimeLesson = !string.IsNullOrEmpty(tsDat.TimeInterval) ? tsDat.TimeInterval : string.Empty,
														   Teachers = us.LastName + " " + us.LastName + " " +(!string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty),
														   SubjectName = sub.SubjectName,
														   LessonType = ltDat.LessonTypeName,
														   Classroom = buidDat.BuildingName + " " + clsDat.ClassroomName
													   }).ToList();
					#endregion
					//Формируем список преподавателей, которые ведут занятия (на тот случай, если двое ведут одну пару)
					int i = 0;
					int j = 0;
					StringBuilder teachersList = new StringBuilder();
					while (i < lessons.Count && j < lessons.Count) 
					{
						j = i + 1;
						if (j < lessons.Count)
						{
							if (lessons[i].LessonId != lessons[j].LessonId)
							{
								_lessonsList.Add(lessons[i]);
								i++;
								if (j == lessons.Count - 1)
									_lessonsList.Add(lessons[j]);
							}
							else
							{
								teachersList.Append(lessons[i].Teachers + "\n");
								teachersList.Append(lessons[j].Teachers + "\n");
								j++;
								while (j < lessons.Count && lessons[i].LessonId == lessons[j].LessonId) //пока id равны =>пропускаем элементы
								{
									teachersList.Append(lessons[j] + "\n");
								}
								lessons[i].Teachers = teachersList.ToString();
								_lessonsList.Add(lessons[i]);
								i = j-1;
								teachersList.Clear();
							}
						}
					}
				}
			}
			else
				throw new ArgumentException();
		}
		#endregion

		#region Bindings
		public string Header
		{
			get { return "Список занятий гр. " + _group.GroupName; }
		}
		public List<ViewGroupsLessons> LessonsList => _lessonsList;

		public ViewGroupsLessons SelectedLessons
		{
			get { return _selectedLessons; }
			set
			{
				_selectedLessons = value;
				OnPropertyChanged("SelectedLessons");
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
					_goBack = new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.ManagingScheduleView));
				return _goBack;
			}
		}
		#endregion

		#region Подробная информаци по занятию
		public ICommand FullInfoLessonCommand
		{
			get
			{
				return new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.FullInfoLessonView, _selectedLessons),
										(p) => _selectedLessons == null ? false : true);
			}
		}
		#endregion
		#region Добавить новое занятие
		public ICommand AddNewLessonCommand
		{
			get
			{
				return new RelayCommand((p) => _navigationManager.Navigate(NavigationKeys.AddNewLessonView, _group.GroupId));
			}
		}
		#endregion



	}
}
