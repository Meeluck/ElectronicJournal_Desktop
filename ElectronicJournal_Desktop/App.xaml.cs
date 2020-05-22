using System;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using ElectronicJournal_Desktop.Constants;
using ElectronicJournal_Desktop.View;
using ElectronicJournal_Desktop.ViewModel;
using ElectronicJournal_Desktop.Infrastructure;
using System.Windows;

namespace ElectronicJournal_Desktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var window = new MainWindow();
			//Создайте менеджер навигации
			var navigationManager = new NavigationManager(window);

			var dialogManager = new DialogManager();

			//2. Определите правила навигации: 
			//зарегистрируйте ключ (строку) с соответствующими View и ViewModel для него
			navigationManager.Register<AuthorizationView>(NavigationKeys.AuthorizationView,
				() => new AuthorizationViewModel(navigationManager, dialogManager));

			navigationManager.Register<MainWindowView>(NavigationKeys.MainWindow,
				() => new MainWindowViewModel(navigationManager));
			
			navigationManager.Register<ManagingUserAccountsView>(NavigationKeys.ManagingUserAccountsView,
				() => new ManagingUserAccountsViewModel(navigationManager));

			navigationManager.Register<ManagingGroupsView>(NavigationKeys.ManagingGroupsView,
				() => new ManagingGroupsViewModel(navigationManager, dialogManager));

			navigationManager.Register<ManagingScheduleView>(NavigationKeys.ManagingScheduleView,
				() => new ManagingScheduleViewModel(navigationManager, dialogManager));

			navigationManager.Register<ReportingView>(NavigationKeys.ReportingView,
				() => new ReportingViewModel(navigationManager, dialogManager));

			navigationManager.Register<FullInfoStudentView>(NavigationKeys.FullInfoStudentView,
				() => new FullInfoStudentViewModel(navigationManager,dialogManager));

			navigationManager.Register<FullInfoTeacherView>(NavigationKeys.FullInfoTeacherView,
				() => new FullInfoTeacherViewModel(navigationManager, dialogManager));

			navigationManager.Register<FullInfoDekanatView>(NavigationKeys.FullInfoDekanatView,
				() => new FullInfoDekanatViewModel(navigationManager, dialogManager));

			navigationManager.Register<FullInfoAdminView>(NavigationKeys.FullInfoAdminView,
				() => new FullInfoAdminViewModel(navigationManager, dialogManager));

			navigationManager.Register<AddNewUsersView>(NavigationKeys.AddNewUsersView,
				() => new AddNewUsersViewModel(navigationManager, dialogManager));

			navigationManager.Register<AddNewStudentView>(NavigationKeys.AddNewStudentView,
				() => new AddNewStudentViewModel(navigationManager, dialogManager));

			navigationManager.Register<EditStudentView>(NavigationKeys.EditStudentView,
				() => new EditStudentViewModel(navigationManager, dialogManager));

			navigationManager.Register<AddNewTeacherView>(NavigationKeys.AddNewTeacherView,
				() => new AddNewTeacherViewModel(navigationManager, dialogManager));

			navigationManager.Register<EditTeacherView>(NavigationKeys.EditTeacherView,
				() => new EditTeacherViewModel(navigationManager, dialogManager));

			navigationManager.Register<EditDekanatView>(NavigationKeys.EditDekanatView,
				() => new EditDekanatViewModel(navigationManager, dialogManager));

			//3. Отобразите стартовый UI
			window.Show();
			navigationManager.Navigate(NavigationKeys.AuthorizationView);

		}
	}
}
