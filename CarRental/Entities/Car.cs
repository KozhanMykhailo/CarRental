using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Entities
{
	public class Car
	{
		/// <summary>
		/// ID
		/// </summary>
		[Key]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		[Display(Name = "Название")]
		[Required(ErrorMessage = "Заполните наименование")]
		[StringLength(30, MinimumLength = 1, ErrorMessage = "Длина строки должна быть до 30 символов")]
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
		public string Description { get; set; }
		/// <summary>
		/// Изображение
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public byte[] ImageData { get; set; }
		/// <summary>
		/// Тип изображения
		/// </summary>
		[HiddenInput(DisplayValue = false)]
		public string ImageMimeType { get; set; }
		
	}
}
