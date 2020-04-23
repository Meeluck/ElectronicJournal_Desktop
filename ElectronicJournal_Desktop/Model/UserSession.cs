using System;
using System.Collections.Generic;
using System.Text;
using ElectronicJournal_Desktop.Model.Data;

namespace ElectronicJournal_Desktop.Model
{
	public class UserSession
	{
		private static UserSession _userSession;
		private static string _fullName;
		private static string _accessLecel;

		protected UserSession() { }

		public static UserSession Instance(string fullname, string accessLevele)
		{
			if (_userSession == null)
			{
				_userSession = new UserSession();
				_fullName = fullname;
				_accessLecel = accessLevele;
			}
			return _userSession;
		}
		public static string GetName
		{
			get { return _fullName; }
		}
		public static string AccessLevelName
		{
			get { return _accessLecel; } 
		}
	}
}
