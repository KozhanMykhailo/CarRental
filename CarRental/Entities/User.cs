using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Entities
{
	public class User
	{
		/// <summary>
		/// ID
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Електронная почта
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// Пароль
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// ID Роли
		/// </summary>
		public int? RoleId { get; set; }
		/// <summary>
		/// Роль
		/// </summary>
		public Role Role { get; set; }
	}
}
