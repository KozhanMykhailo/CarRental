using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CarRental.Entities
{
	public enum ClassCar
	{
		/// <summary>
		///  Неизвестно(либо всё)
		/// </summary>
		[Display(Name = "Любой")]
		All = 0,
		/// <summary>
		///  Малогабаритные авто
		/// </summary>
		A = 1,
		/// <summary>
		/// Малолитражки
		/// </summary>
		B = 2,
		/// <summary>
		/// Средние по размеру 
		/// </summary>
		C = 3,
		/// <summary>
		/// Комфорт
		/// </summary>
		D = 4,
		/// <summary>
		/// Премиум
		/// </summary>
		E = 5,
	}
	public enum OptionsDriver
	{
		/// <summary>
		/// Без водителя
		/// </summary>
		[Display(Name = "Без водителя")]
		Off = 0,
		/// <summary>
		/// С водителем
		/// </summary>
		[Display(Name = "С водителем")]
		On = 1,
	}
	public enum SortState
	{
		/// <summary>
		/// по наименованию по возрастанию
		/// </summary>
		NameAsc,
		/// <summary>
		/// по наименованию по убыванию
		/// </summary>
		NameDesc,
		/// <summary>
		/// по цене по возрастанию
		/// </summary>
		PriceAsc,
		/// <summary>
		/// по цене по убыванию
		/// </summary>
		PriсeDesc,
	}
	public enum StatusOrder
	{
		/// <summary>
		/// Подтверждение
		/// </summary>
		[Description("Подтверждение")]
		Approval = 0,
		/// <summary>
		/// Подтвержден
		/// </summary>
		[Description("Подтвержден")]
		Approved = 1,
		/// <summary>
		/// Отменен
		/// </summary>
		[Description("Отменен")]
		Сanceled = 2,
		/// <summary>
		/// Выполнен
		/// </summary>
		[Description("Выполнен")]
		Completed = 3
	}
	public enum TypeBill
	{
		/// <summary>
		/// Услуга
		/// </summary>
		Service = 0,
		/// <summary>
		/// Ремонт
		/// </summary>
		Repair = 1,
	}
	public enum Roles
	{
		/// <summary>
		/// Админ
		/// </summary>
		admin = 1,
		/// <summary>
		/// Пользователь
		/// </summary>
		user = 2,
		/// <summary>
		/// Менеджер
		/// </summary>
		manager = 3,
	}
	
}
