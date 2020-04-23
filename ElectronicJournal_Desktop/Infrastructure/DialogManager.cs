using System.Windows;

namespace ElectronicJournal_Desktop.Infrastructure
{
	public class DialogManager
	{
		public void ShowMessage(string message)
		{
			MessageBox.Show(message);
		}
	}
}
