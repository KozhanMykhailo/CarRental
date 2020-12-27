using CarRental.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Abstract
{
	public interface IRoleRepository
	{
		/// <summary>
		/// Роли 
		/// </summary>
		DbSet<Role> Roles { get; }
	}
}
