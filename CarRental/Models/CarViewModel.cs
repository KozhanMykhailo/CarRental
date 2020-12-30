using CarRental.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class CarViewModel
	{
		/// <summary>
		/// ID
		 /// </summary>
		[Key]
		[HiddenInput]
		public int Id { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		[Required(ErrorMessage = "Заполните наименование")]
		[StringLength(30, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
		[Display(Name = "Название")]
		public string Name { get; set; }
		/// <summary>
		/// Цвет
		/// </summary>
		[StringLength(30, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
		[Required(ErrorMessage = "Укажите цвет")]
		[Display(Name = "Цвет")]
		public string Color { get; set; }
		/// <summary>
		/// Марка авто
		/// </summary>
		[StringLength(30, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
		[Required(ErrorMessage = " Укажите марку авто")]
		[Display(Name = "Марка авто")]
		public string Model { get; set; }
		/// <summary>
		/// Цена за аренду в ЧАС
		/// </summary>
		[Required(ErrorMessage = "Укажите цену")]
		[Display(Name = "Цена за аренду")]
		[Range(typeof(decimal), "0,5", "100000,100", ErrorMessage = "Наименьшие значение 0,5 , в качестве разделителя дробной и целой части используется запятая")]
		public decimal RentalPrice { get; set; }
		/// <summary>
		/// Класс авто
		/// </summary>
		[Required(ErrorMessage = "Укажите класс")]
		[Display(Name = "Класс")]
		public ClassCar Class { get; set; }
		/// <summary>
		/// Описание
		/// </summary>
		[StringLength(4000, ErrorMessage = "Длина описания должна быть до 4000 символов")]
		[Display(Name = "Описание")]
		public string Description { get; set; }
		/// <summary>
		/// Изображение
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Новое изображение")]
		public IFormFile Avatar { get; set; }
		/// <summary>
		/// Изображение для вывода в корректировку
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Текущее изображение")]
		public byte[] Image { get; set; }
	}
}
