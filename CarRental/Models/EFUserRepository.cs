using CarRental.Abstract;
using CarRental.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class EFUserRepository : IUserRepository
	{
		private readonly int idManagerRole = (int)Roles.manager;
		private readonly int idAdminRole = (int)Roles.admin;
		private readonly int idUserRole = (int)Roles.user;
		private ApplicationContext _context;
		public EFUserRepository(ApplicationContext context)
		{
			_context = context;
		}
		public IEnumerable<User> Users => _context.Users.Where(w=>w.RoleId == idUserRole);
		public IEnumerable<User> Managers => _context.Users.Where(w => w.RoleId == idManagerRole);
		public IEnumerable<User> Admins => _context.Users.Where(w => w.RoleId == idAdminRole);
		public DbSet<User> AllUsers => _context.Users;
		public void ChangeLock(int userId)
		{
			var user = _context.Users.Where(w => w.Id == userId).FirstOrDefault();
			if (user != null)
			{
				//если заблокирован - то разблокировать и наоборот , наименование кнопки для этого действия определяется динамично по этому принципу
				user.Lock = user.Lock == 0 ? 1 : 0;
				_context.Users.Update(user);
				_context.SaveChanges();
			}
		}
		public bool DeleteUser(int userId)
		{
			var userInDb = _context.Users.Where(w => w.Id == userId).FirstOrDefault();
			if (userInDb != null)
			{
				_context.Users.Remove(userInDb);
				_context.SaveChanges();
				return true;
			}
			return false;
		}
		public bool SaveUser(User user)
		{
			if (user.Id == 0)
			{
				if (_context.Users.Count() == 0)
				{
					user.Id = 1;
				}
				else
				{
					user.Id = _context.Users.Max(m => m.Id) + 1;
				}
				user.Lock = 0;
				_context.Users.Add(user);
			}
			else
			{
				User dbUser = _context.Users.Find(user.Id);
				if (dbUser != null)
				{
					dbUser.Email = user.Email;
					dbUser.Password = user.Password;
					dbUser.Lock = user.Lock;
					dbUser.RoleId = user.RoleId;
				}
			}
			if (_context.SaveChanges() > 0)
				return true;
			else
				return false;
			
		}
	}
}
