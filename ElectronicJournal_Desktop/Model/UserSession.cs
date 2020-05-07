using System;
using System.Collections.Generic;
using System.Text;
using ElectronicJournal_Desktop.Model.Data;
using ElectronicJournal_Desktop.Context;
using System.Linq;
namespace ElectronicJournal_Desktop.Model
{
	public class UserSession
	{
		static UserSession _userSession;
		static int _userId;
		static string _fullName;
		static int _accessLevelId;
		static string _accessLevelName;

		protected UserSession() { }

		public static UserSession Instance(int userId)
		{
			if (_userSession == null)
			{
				_userSession = new UserSession();
				_userId = userId;
				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
				{
					var user = from u in db.Users
							   join al in db.AccessLevels on u.AccessLevelId equals al.AccessLevelId
							   where u.UserId == userId
							   select new
							   {
								   FullName = u.LastName + " " + u.FirstName + " " +
								   (!string.IsNullOrEmpty(u.MiddleName) ? u.MiddleName : string.Empty),
								   AccessLevel = u.AccessLevelId == null ? 0 : Convert.ToInt32(u.AccessLevelId),
								   AccessLevelName = al.AccessLevelName
							   };
					foreach (var item in user)
					{
						_fullName = item.FullName;
						_accessLevelId = item.AccessLevel;
						_accessLevelName = item.AccessLevelName;
					}
				}
			}

			return _userSession;
		}
		public static string GetName
		{
			get { return _fullName; }
		}
		public static string AccessLevelName
		{
			get { return _accessLevelName; } 
		}
		public static int AccessLevelId
		{
			get { return _accessLevelId; }
		}
	}
}
