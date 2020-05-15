using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ElectronicJournal_Desktop.Model.Interfaces;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Desktop.Model.Data;
using System.Collections.ObjectModel;
using ElectronicJournal_Desktop.View;

namespace ElectronicJournal_Desktop.Model
{
	public class FullInfoStudent:Users
	{
		public string AcessLevelName;
		public int? GroupId { get; set; }
		public string GroupName { get; set; }
	}
	public class FullInfoTeacher : Users
	{
		public string AcessLevelName;
		public int? PositionId { get; set; }
		public string PositionName { get; set; }
	}
	public class FullInfoDekanat :Users
	{
		public string AcessLevelName;
	}
	public class FullInfoAdmin : Users
	{
		public string AcessLevelName;
	}

	public class ManagingUserAccountsModel
	{
		#region Fields

		ObservableCollection<GeneralUserInformation> _usersCollection;
		IUserListRequestBehavior _userListRequest;
		
		FullInfoStudent _fullInfoStudent;
		FullInfoTeacher _fullInfoTeacher;
		FullInfoDekanat _fullInfoDekanat;
		FullInfoAdmin _fullInfoAdmin;
		ObservableCollection<AccessLevels> _accessLevelsList;
		ObservableCollection<Groups> _groups;
		#endregion


		#region DataRequesBehavior

		public void SetUserListRequestBehavior(IUserListRequestBehavior behavior)
		{
			_userListRequest = behavior;
		}

		#endregion

		#region Список пользователей

		public ObservableCollection<GeneralUserInformation> GetUsersCollection
		{
			get
			{
				return _userListRequest.DataReques();
			}
		}

		#endregion

		#region Подробная информация о студенте

		public FullInfoStudent FullInfoStudent(int id)
		{

			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				var student = from us in db.Users
							  join al in db.AccessLevels on us.AccessLevelId equals al.AccessLevelId
							  into alDatails
							  from alDat in alDatails.DefaultIfEmpty()
							  join stGr in db.StudentGroups on us.UserId equals stGr.UserId
							  into stGrDatails
							  from stGrDat in stGrDatails.DefaultIfEmpty()
							  join gr in db.Groups on stGrDat.GroupId equals gr.GroupId
							  into grDatails
							  from grDat in grDatails.DefaultIfEmpty()
							  where us.UserId == id
							  select new FullInfoStudent
							  {
								  UserId = us.UserId,
								  FirstName = us.FirstName,
								  MiddleName = !string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty,
								  LastName = us.LastName,
								  Login = us.Login,
								  PasswordHash = us.PasswordHash,
								  PasswordSalt = us.PasswordSalt,
								  AccessLevelId = us.AccessLevelId,
								  AcessLevelName = !string.IsNullOrEmpty(alDat.AccessLevelName) ? alDat.AccessLevelName : string.Empty,
								  Email = us.Email,
								  Phone = us.Phone,
								  GroupId = stGrDat.GroupId,
								  GroupName = !string.IsNullOrEmpty(grDat.GroupName) ? grDat.GroupName : string.Empty
							  };
				foreach (var item in student)
				{
					_fullInfoStudent = item;
				}
			}
			return _fullInfoStudent;
		}

		#endregion

		#region Подробная информация о преподавателе
		public FullInfoTeacher FullInfoTeacher(int id)
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				var teacher = from us in db.Users
							  join al in db.AccessLevels on us.AccessLevelId equals al.AccessLevelId
							  into alDatails
							  from alDat in alDatails.DefaultIfEmpty()
							  join tch in db.Teachers on us.UserId equals tch.UserId
							  into tchDatail
							  from tchDat in tchDatail.DefaultIfEmpty()
							  join pos in db.Positions on tchDat.PositionId equals pos.PositionId
							  into posDatail
							  from posDat in posDatail.DefaultIfEmpty()
							  where us.UserId == id
							  select new FullInfoTeacher
							  {
								  UserId = us.UserId,
								  FirstName = us.FirstName,
								  MiddleName = !string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty,
								  LastName = us.LastName,
								  Login = us.Login,
								  PasswordHash = us.PasswordHash,
								  PasswordSalt = us.PasswordSalt,
								  AccessLevelId = us.AccessLevelId,
								  AcessLevelName = alDat.AccessLevelName,
								  Email = us.Email,
								  Phone = us.Phone,
								  PositionId = posDat.PositionId,
								  PositionName = posDat.PositionName
							  };
				foreach(var item in teacher)
				{
					_fullInfoTeacher = item;
				}
			}
			return _fullInfoTeacher;
		}
		#endregion

		#region Подробная информация по админу/деканату
		public FullInfoDekanat FullInfoDekanat(int id)
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				var dekanat = from us in db.Users
							  join al in db.AccessLevels on us.AccessLevelId equals al.AccessLevelId
							  where us.UserId == id
							  select new FullInfoDekanat
							  {
								  UserId = us.UserId,
								  FirstName = us.FirstName,
								  MiddleName = !string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty,
								  LastName = us.LastName,
								  Login = us.Login,
								  PasswordHash = us.PasswordHash,
								  PasswordSalt = us.PasswordSalt,
								  AccessLevelId = us.AccessLevelId,
								  AcessLevelName = al.AccessLevelName,
							  };
				foreach (var item in dekanat)
				{
					_fullInfoDekanat = item;
				}
			}
			return _fullInfoDekanat;
		}

		public FullInfoAdmin FullInfoAdmin(int id)
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				var admin = from us in db.Users
							  join al in db.AccessLevels on us.AccessLevelId equals al.AccessLevelId
							  where us.UserId == id
							  select new FullInfoAdmin
							  {
								  UserId = us.UserId,
								  FirstName = us.FirstName,
								  MiddleName = !string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty,
								  LastName = us.LastName,
								  Login = us.Login,
								  PasswordHash = us.PasswordHash,
								  PasswordSalt = us.PasswordSalt,
								  AccessLevelId = us.AccessLevelId,
								  AcessLevelName = al.AccessLevelName,
							  };
				foreach (var item in admin)
				{
					_fullInfoAdmin = item;
				}
			}
			return _fullInfoAdmin;
		}
		#endregion

		#region Список модификаторов доступа

		public ObservableCollection<AccessLevels> AccessLevelsList
		{
			get
			{
				_accessLevelsList = new ObservableCollection<AccessLevels>();

				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
					{
						var accessLevels = from al in db.AccessLevels
										   select new AccessLevels
										   { 	
										   AccessLevelId = al.AccessLevelId,
										   AccessLevelName = al.AccessLevelName
										   };
					foreach (var item in accessLevels)
					{
						_accessLevelsList.Add(item);
					}
				}
				
				return _accessLevelsList;
			}
		}

		public ObservableCollection<AccessLevels> AccessLevelsStudentList
		{
			get
			{
				_accessLevelsList = new ObservableCollection<AccessLevels>();
				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
				{
					var accessLevels = from al in db.AccessLevels
									   where al.AccessLevelId <= 2
									   select new AccessLevels
									   {
										   AccessLevelId = al.AccessLevelId,
										   AccessLevelName = al.AccessLevelName
									   };
					foreach (var item in accessLevels)
					{
						_accessLevelsList.Add(item);
					}
				}
				return _accessLevelsList;
			}
		}
		#endregion

		#region Добавление пользователей

		public void AddNewUsers(Users user)
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				user.AccessLevel = null;
				db.Users.Add(user);
				db.SaveChanges();
			}
		}

		#endregion

		#region Список групп

		public ObservableCollection<Groups> GetGroups
		{
			get
			{
				_groups = new ObservableCollection<Groups>();
				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
				{
					var groupList = from gr in db.Groups
									select new Groups
									{
										GroupId = gr.GroupId,
										GroupName = gr.GroupName,
										YearFormationGroup = gr.YearFormationGroup,
										StarostaId = gr.StarostaId
									};
					foreach (Groups item in groupList)
					{
						_groups.Add(item);
					}
				}
				return _groups;
			}
		}

		#endregion

		#region Связывание студент с группой

		public void ConnectStudentWithGroup(string login,int groupId)
		{
			Users student = new Users();
			StudentGroups sg = new StudentGroups();
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				var st = from us in db.Users
						 where us.Login == login
						 select new Users
						 {
							 UserId = us.UserId,
							 FirstName = us.FirstName,
							 MiddleName = !string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty,
							 LastName = us.LastName,
							 Login = us.Login,
							 PasswordHash = us.PasswordHash,
							 PasswordSalt = us.PasswordSalt,
							 AccessLevelId = us.AccessLevelId,
							 Email = us.Email,
							 Phone = us.Phone
						 };
				foreach(Users item in st)
				{
					student = item;
				}

				sg.GroupId = groupId;
				sg.UserId = student.UserId;

				db.StudentGroups.Add(sg);
				db.SaveChanges();
			}	

		}

		#endregion

		#region Удаление пользователей

		public void DeleteUser(Users user)
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				db.Users.Remove(user);
				db.SaveChanges();
			}
		}

		#endregion

		#region Редактирование пользователей

		public void EditStudent(FullInfoStudent student)
		{
			Users editUser = student;
			StudentGroups editSGUser = new StudentGroups();

			editUser.AccessLevel = null;
			
			using(ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				var studGroup = from sg in db.StudentGroups
								where sg.UserId == editUser.UserId
								select new StudentGroups
								{
									StudentGroupId = sg.StudentGroupId,
									UserId = sg.UserId,
									GroupId = sg.GroupId
								};
				foreach (StudentGroups item in studGroup)
					editSGUser = item;

				if(editSGUser.GroupId != student.GroupId)
				{
					editSGUser.GroupId = student.GroupId;
					db.StudentGroups.Update(editSGUser);
				}
				db.Users.Update(editUser);
				db.SaveChanges();
			}
		}

		#endregion
	}
}
