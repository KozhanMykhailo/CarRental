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
		IEnumerable<User> Users { get; }
		IEnumerable<User> Managers { get; }
		IEnumerable<User> Admins { get; }
		DbSet<User> AllUsers { get; }
		bool SaveUser(User user);
		bool DeleteUser(int userId);
		void ChangeLock (int userId);
	}
}
