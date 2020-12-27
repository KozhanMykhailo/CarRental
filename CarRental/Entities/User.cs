using Microsoft.AspNetCore.Mvc;
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
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		/// <summary>
		/// Електронная почта
		/// </summary>
		[Display(Name = "Электронная почта")]
		[Required(ErrorMessage = "Электронная почта должна быть заполнена")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
		public string Email { get; set; }
		/// <summary>
		/// Пароль
		/// </summary>
		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Введите пароль")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		/// <summary>
		/// ID Роли
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public int? RoleId { get; set; }
		/// <summary>
		/// Блокировка . 0 - незаблокирован , 1 - заблокирован
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public int? Lock { get; set; }
		/// <summary>
		/// Роль
		/// </summary>
	    [HiddenInput(DisplayValue = false)]
		public Role Role { get; set; }
	}
}
