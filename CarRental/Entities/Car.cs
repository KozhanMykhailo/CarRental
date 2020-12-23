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
		public int Id { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Цвет
		/// </summary>
		public string Color { get; set; }
		/// <summary>
		/// Модель авто
		/// </summary>
		public string Model { get; set; }
		/// <summary>
		/// Цена за аренду в ЧАС
		/// </summary>
		public decimal RentalPrice { get; set; }
		/// <summary>
		/// Класс авто
		/// </summary>
		public ClassCar Class { get; set; }
		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// Изображение
		/// </summary>
		public byte[] ImageData { get; set; }
		/// <summary>
		/// Тип изображения
		/// </summary>
		public string ImageMimeType { get; set; }
	}
}
