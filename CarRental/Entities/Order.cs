using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Entities
{
	public class Order
	{
		/// <summary>
		/// ID
		/// </summary>
		[Key]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		/// <summary>
		/// Данные паспорта
		/// </summary>
		[Display(Name = "Данные паспорта")]
		[Required(ErrorMessage = "Заполните данные(Серия + Номер)")]
		[StringLength(30, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
		public string PassportData { get; set; }
		/// <summary>
		/// Имя
		/// </summary>
		[Display(Name = "Имя")]
		[Required(ErrorMessage = "Заполните Имя")]
		[StringLength(30, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
		public string FirstName { get; set; }
		/// <summary>
		/// Данные паспорта
		/// </summary>
		[Display(Name = "Фамилия")]
		[Required(ErrorMessage = "Заполните Фамилию")]
		[StringLength(30, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
		public string LastName { get; set; }		
		/// <summary>
		/// ИНН(ЕДРПО)
		/// </summary>
		[Required(ErrorMessage = "Заполните ИНН(ЕДРПО если вы организация)")]
		[Display(Name = "ИНН(ЕДРПО)")]
		public int INN { get; set; }
		/// <summary>
		/// Услуги водителя
		/// </summary>
		[Required(ErrorMessage = " Укажите опцию")]
		[Display(Name = "Услуги водителя")]
		public OptionsDriver OptionDriver { get; set; }
		/// <summary>
		/// Срок аренды (ЕИ = час)
		/// </summary>
		[Required(ErrorMessage = "Укажите длительность в часах")]
		[Display(Name = "Срок аренды")]
		[Range(typeof(decimal), "0,5", "100000,100", ErrorMessage = "Наименьшие значение 0,5 , в качестве разделителя дробной и целой части используется запятая")]
		public decimal RentalTime { get; set; }
		/// <summary>
		/// Статус заказа
		/// </summary>
		[Display(Name = "Статус")]
		public StatusOrder Status { get; set; }
		/// <summary>
		/// Описание . Менеджеру для комментариев
		/// </summary>
		[StringLength(4000, ErrorMessage = "Длина описания должна быть до 4000 символов")]
		[Display(Name = "Комментарий")]
		public string Comment { get; set; }
		/// <summary>
		/// Id заказчика
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public int UserId { get; set; }
		/// <summary>
		/// Id машины
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public int CarId { get; set; }
	}
}
