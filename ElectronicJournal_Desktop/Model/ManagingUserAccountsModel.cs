using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ElectronicJournal_Desktop.Model.Interfaces;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Desktop.Model.Data;
using System.Collections.ObjectModel;

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
							  join tch in db.Teachers on us.UserId equals tch.UserId
							  join pos in db.Positions on tch.PositionId equals pos.PositionId
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
								  AcessLevelName = al.AccessLevelName,
								  Email = us.Email,
								  Phone = us.Phone,
								  PositionId = pos.PositionId,
								  PositionName = pos.PositionName
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
		public Users FullInfoDekanat(int id)
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

		public Users FullInfoAdmin(int id)
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
	}
}
