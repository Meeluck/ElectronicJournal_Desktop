using System;
using System.Collections.ObjectModel;
using System.Text;
using ElectronicJournal_Desktop.Model.Data;

namespace ElectronicJournal_Desktop.Model.Interfaces
{
	public interface IUserListRequestBehavior
	{
		ObservableCollection<GeneralUserInformation> DataReques();
	}
}
