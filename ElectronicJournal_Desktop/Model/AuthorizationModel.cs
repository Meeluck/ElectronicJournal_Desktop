using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ElectronicJournal_Desktop.Model.Data;
using ElectronicJournal_Desktop.Context;
using ElectronicJournal_Library;
namespace ElectronicJournal_Desktop.Model
{
	public class AuthorizationModel
	{
		#region Field
		
		private string _login;
		private string _password;
		private Users _user;

		#endregion

		#region Ctor

		public AuthorizationModel()
		{
			_login = null;
			_password = null;
			_user = null;
		}

		public AuthorizationModel(string login, string password)
		{
			_login = login;
			_password = password;
			_user = null;
		}

		#endregion

		#region Propety

		public string SetLogin
		{
			set { _login = value; }
		}

		public string SetPassword
		{
			set { _password = value; }
		}

		public Users GetUser
		{
			get
			{
				if (_user == null)
					_user = new Users();
				return _user;
			}
		}

		#endregion

		#region Поиск логина, проверка уровня доступа и сравнение паролей

		//Метод проверки, существует пользователь с таким логин в бд
		public bool CheckLogin()
		{
			var db = new ElectronicalJournalContext();

			var us = from user in db.Users
					 where user.Login == _login
					 select new Users
					 {
						 UserId = user.UserId,
						 FirstName = user.FirstName,
						 MiddleName = !string.IsNullOrEmpty(user.MiddleName) ? user.MiddleName : string.Empty,
						 LastName = user.LastName,
						 Login = user.Login,
						 PasswordHash = user.PasswordHash,
						 PasswordSalt = user.PasswordSalt,
						 AccessLevelId = user.AccessLevelId,
						 Phone = !string.IsNullOrEmpty(user.Phone) ? user.Phone : string.Empty,
						 Email = !string.IsNullOrEmpty(user.Email) ? user.Email : string.Empty,
					 };
			foreach (var item in us)
			{
				_user = new Users
				{
					UserId = item.UserId,
					FirstName = item.FirstName,
					MiddleName = item.MiddleName,
					LastName = item.LastName,
					Login = item.Login,
					PasswordHash = item.PasswordHash,
					PasswordSalt = item.PasswordSalt,
					AccessLevelId = item.AccessLevelId,
					Phone = item.Phone,
					Email = item.Email
				};
			}

			if (_user == null)
				return false;
			else
				return true;

		}
		//проверяем уровень доступа к приложению. 
		//доступ к приложению доступен у пользователей с 
		//модификаторами доступа 4(Деканат) и 5(Админ) 
		public bool CheckAccessLevel()
		{
			if (_user.AccessLevelId == 4 || _user.AccessLevelId == 5) 
				return true;
			else 
				return false;
		}
		//метод для проверки пароля
		public bool CheckPassword()
		{
			Password ps = new Password(_user.PasswordHash, _user.PasswordSalt);

			return ps.ComparePassword(_password);
		}

		#endregion
	}
}
