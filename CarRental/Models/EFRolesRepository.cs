using CarRental.Abstract;
using CarRental.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class EFRolesRepository : IRoleRepository
	{
		private ApplicationContext _context;
		public EFRolesRepository(ApplicationContext context)
		{
			_context = context;
		}
		public DbSet<Role> Roles => _context.Roles;
	}
}
