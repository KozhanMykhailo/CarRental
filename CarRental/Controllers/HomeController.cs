using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Abstract;
using CarRental.Entities;
using CarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRental.Controllers
{

	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private ICarRepository _carRepository;
		private IOrderRepository _orderRepository;
		private IBillRepository _billRepository;

		public HomeController(ILogger<HomeController> logger, ICarRepository carRepository, IOrderRepository orderRepository,IBillRepository billRepository)
		{
			_logger = logger;
			_orderRepository = orderRepository;
			_carRepository = carRepository;
			_billRepository = billRepository;
		}
		//[Authorize(Roles = "admin,user")]
		public IActionResult Index(ClassCar? clas, string model, int page = 1, SortState sortOrder = SortState.NameAsc)
		{
			int pageSize = 5;

			//фильтрация
			IEnumerable<Car> cars = _carRepository.Cars;
			if (clas == ClassCar.All && string.IsNullOrEmpty(model))
			{
				cars = _carRepository.Cars;
			}
			else if (clas == ClassCar.All && !string.IsNullOrEmpty(model))
			{
				cars = cars.Where(p => p.Model.Contains(model) || p.Model.StartsWith(model));
			}
			else
			{
				if (clas != null)
				{
					cars = cars.Where(p => p.Class == clas);
				}
				if (!String.IsNullOrEmpty(model))
				{
					cars = cars.Where(p => p.Model.Contains(model) || p.Model.StartsWith(model));
				}
			}

			// сортировка
			switch (sortOrder)
			{
				case SortState.NameDesc:
					cars = cars.OrderByDescending(s => s.Name);
					break;
				case SortState.PriceAsc:
					cars = cars.OrderBy(s => s.RentalPrice);
					break;
				case SortState.PriсeDesc:
					cars = cars.OrderByDescending(s => s.RentalPrice);
					break;
					cars = cars.OrderBy(s => s.Name);
					break;
			}

			// пагинация
			var count = cars.Count();
			var items = cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();

			// формируем модель представления
			IndexViewModel viewModel = new IndexViewModel
			{
				PageViewModel = new PageViewModel(count, page, pageSize),
				SortViewModel = new SortViewModel(sortOrder),
				FilterViewModel = new FilterViewModel(clas, model),
				Cars = items
			};
			return View(viewModel);
		}
		/// <summary>
		/// Создать заказ. GET
		/// </summary>
		/// <param name="id">Id машины на которую формируется заказ</param>
		/// <returns></returns>
		[Route("Home/CreateOrder")]
		[Authorize(Roles = "user")]
		[HttpGet]
		public IActionResult CreateOrder(int carId)
		{
			HttpContext.Session.SetString("CarId", carId.ToString());
			return View();
		}
		[Route("Home/CreateOrder")]
		[Authorize(Roles = "user")]
		[HttpPost]
		public IActionResult CreateOrder(Order order)
		{
			if (ModelState.IsValid)
			{
				int userId;				
				if (int.TryParse(HttpContext.Session.GetString("CurrentUserId"), out userId))
				{
					order.UserId = userId;
					order.CarId = int.Parse(HttpContext.Session.GetString("CarId"));
					HttpContext.Session.Remove("CarId");
					_orderRepository.SaveOrder(order);
					return RedirectToAction("MyAllOrders");
				}
				else
					return RedirectToRoute(new { controller = "Account", action = "SignOut" });
				
			}
			return View(order);
		}
		[Route("Home/MyAllOrders")]
		[Authorize(Roles = "user")]
		[HttpGet]
		public IActionResult MyAllOrders()
		{
			//если перезапускать IIS то авторизация отстается а параметр в сессии нет...
			int userId;
			if (int.TryParse(HttpContext.Session.GetString("CurrentUserId"), out userId))
				return View(_orderRepository.Orders.Where(w => w.UserId == userId));
			else
				return RedirectToRoute(new {controller = "Account",action = "SignOut" });
		}
		[Route("Home/PayBill")]
		[Authorize(Roles = "user")]
		[HttpGet]
		public IActionResult PayBill(int orderId)
		{
			return View(_billRepository.Bills.Where(w => w.OrderId == orderId).FirstOrDefault());
		}
		[Route("Home/Pay")]
		[Authorize(Roles = "user")]
		[HttpGet]
		public IActionResult Pay(int billId)
		{
			return View();
		}
	}
}
