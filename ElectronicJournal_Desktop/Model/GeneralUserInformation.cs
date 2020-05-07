using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicJournal_Desktop.Model
{
	public class GeneralUserInformation
	{
		public int UserId { get; set; }
		public string Login { get; set; }
		public string FullName { get; set; }
		public string AccessLevelName { get; set; }
	}
}
