using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Entities
{
	public class Bill
	{
		/// <summary>
		/// ID
		/// </summary>
		[Key]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		/// <summary>
		/// Id заказчика
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public int UserId { get; set; }
		/// <summary>
		/// Id заказа
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public int OrderId { get; set; }		
		/// <summary>
		/// Тип счета
		/// </summary>
		[Display(Name = "Тип счета")]
		public TypeBill TypeBill { get; set; }
		/// <summary>
		/// К оплате
		/// </summary>
		[Display(Name = "К оплате")]
		[Required(ErrorMessage = "Укажите стоимость ремонта")]
		[Range(typeof(decimal), "0,5", "100000,100", ErrorMessage = "Наименьшие значение 0,5 , в качестве разделителя дробной и целой части используется запятая")]
		public decimal ToPay { get; set; }		
		/// <summary>
		/// Банковские реквизиты
		/// </summary>
		[StringLength(30, ErrorMessage = "Длина описания должна быть до 4000 символов")]
		public string AccountDetails { get; set; }		
	}
}
