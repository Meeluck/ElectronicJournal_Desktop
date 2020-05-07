using ElectronicJournal_Desktop.Model.Interfaces;
using ElectronicJournal_Desktop.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ElectronicJournal_Desktop.Model
{
	public class DataRequestForAdmin : IUserListRequestBehavior
	{
		ObservableCollection<GeneralUserInformation> _users = new ObservableCollection<GeneralUserInformation>();

		public ObservableCollection<GeneralUserInformation> DataReques()
		{
			using (ElectronicalJournalContext db = new ElectronicalJournalContext())
			{
				var usersCollection = from u in db.Users
									  join al in db.AccessLevels on u.AccessLevelId equals al.AccessLevelId
									  select new GeneralUserInformation
									  {
										  UserId = u.UserId,
										  Login = u.Login,
										  FullName = u.LastName + " " + u.FirstName + " " +
												(!string.IsNullOrEmpty(u.MiddleName) ? u.MiddleName : string.Empty),
										  AccessLevelName = al.AccessLevelName
									  };
				foreach (var item in usersCollection)
				{
					_users.Add(item);
				}
			}
			return _users;
		}
	}
}
