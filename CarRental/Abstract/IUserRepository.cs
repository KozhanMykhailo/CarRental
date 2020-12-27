using CarRental.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Abstract
{
	public interface IUserRepository
	{
		/// <summary>
		/// Клиенты
		/// </summary>
		IEnumerable<User> Users { get; }
		/// <summary>
		/// Менеджеры
		/// </summary>
		IEnumerable<User> Managers { get; }
		/// <summary>
		/// Администрторы
		/// </summary>
		IEnumerable<User> Admins { get; }
		/// <summary>
		/// Все зарегистрированные пользователи
		/// </summary>
		DbSet<User> AllUsers { get; }
		bool SaveUser(User user);
		bool DeleteUser(int userId);
		/// <summary>
		/// Сменить состояние блокировки (разболокирвать/заблокировать)
		/// </summary>
		void ChangeLock (int userId);
	}
}
