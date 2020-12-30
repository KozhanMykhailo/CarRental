using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Abstract;
using CarRental.Entities;
using CarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils.Messaging;

namespace CarRental.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "admin")]
	public class AdminController : Controller
	{
		private readonly int idManagerRole = (int)Roles.manager;
		private ICarRepository _carRepository;
		private IUserRepository _userRepository;
		public AdminController(ICarRepository carRepository, IUserRepository userRepository)
		{
			_carRepository = carRepository;
			_userRepository = userRepository;
		}
		[Route("Admin/DefaultAction")]
		[HttpGet]
		public IActionResult DefaultAction()
		{
			return View();
		}
		[Route("Admin/Users")]
		[HttpGet]
		public IActionResult Users()
		{
			return View(_userRepository.AllUsers);
		}
		/// <summary>
		/// Метод для изменения состояния 1 поля в объекте 
		/// Что бы не создавать 2 action get,post я ограничился вот таким решением, 
		/// так в ТЗ указано -> разблокировать/блокировать - а не возможность редактирования всех данных 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Route("Admin/ChangeLock")]
		public IActionResult ChangeLock(int id)
		{
			_userRepository.ChangeLock(id);
			return RedirectToAction("Users");
		}
		[Route("Admin/GetAllCars")]
		[HttpGet]
		public IActionResult GetAllCars()
		{
			return View(_carRepository.Cars);
		}
		[Route("Admin/CreateCar")]
		[HttpGet]
		public IActionResult CreateCar()
		{
			return View();
		}
		[Route("Admin/CreateCar")]
		[HttpPost]
		public IActionResult CreateCar(CarViewModel newcar)
		{
			if (ModelState.IsValid)
			{
				if(_carRepository.SaveCar(newcar))
					return RedirectToAction("GetAllCars");
			}			
			return View(newcar);
		}
		[Route("Admin/EditCar")]
		[HttpGet]
		public IActionResult EditCar(int id)
		{
			var car = _carRepository.Cars.Where(w => w.Id == id).FirstOrDefault();
			if (car != null)
			{				
				//var stream = new MemoryStream(car.ImageData);
				return View(new CarViewModel() { 
					Name = car.Name,
					RentalPrice = car.RentalPrice,
					Model = car.Model,
					//Avatar = new FormFile(stream, 0, car.ImageData.Length, "potoc", "image1"),
					Image = car.ImageData,
					Color = car.Color,
					Class = car.Class,
					Description = car.Description,
					Id = car.Id}
					);
			}
			return RedirectToAction("GetAllCars");
		}
		[Route("Admin/EditCar")]
		[HttpPost]
		public IActionResult EditCar(CarViewModel car)
		{
			if (ModelState.IsValid)
			{
				if (_carRepository.SaveCar(car))
					return RedirectToAction("GetAllCars");				
			}
			return View(car);
		}
		[Route("Admin/DeleteCar")]
		[HttpGet]
		public IActionResult DeleteCar(int id)
		{
			var car = _carRepository.Cars.Where(w => w.Id == id).FirstOrDefault();
			if (car != null)
			{
				return View(car);
			}
			return RedirectToAction("GetAllCars");
		}
		[Route("Admin/DeleteCar")]
		[HttpPost]
		public IActionResult DeleteCar(Car car)
		{			
			if (_carRepository.DeleteCar(car.Id))
			{				
				return RedirectToAction("GetAllCars");
			}
			return NotFound();
		}
		[Route("Admin/DetailsCar")]
		[HttpGet]
		public IActionResult DetailsCar(int id)
		{
			var car = _carRepository.Cars.Where(w => w.Id == id).FirstOrDefault();
			if (car != null)
			{
				return View(car);
			}
			return RedirectToAction("GetAllCars");
		}
		[Route("Admin/GetAllManagers")]
		[HttpGet]
		public IActionResult GetAllManagers()
		{
			return View(_userRepository.Managers);
		}
		[Route("Admin/CreateManager")]
		[HttpGet]
		public IActionResult CreateManager()
		{
			return View();
		}
		[Route("Admin/CreateManager")]
		[HttpPost]
		public IActionResult CreateManager(User newmanager)
		{
			if (ModelState.IsValid)	
			{
				newmanager.RoleId = idManagerRole;
				_userRepository.SaveUser(newmanager);
				return RedirectToAction("GetAllManagers");
			}
			return View(newmanager);
		}
		[Route("Admin/DeleteManager")]
		[HttpPost]
		public IActionResult DeleteManager(User user)
		{
			if (_userRepository.DeleteUser(user.Id))
			{
				return RedirectToAction("GetAllManagers");
			}
			return NotFound();
		}
		[Route("Admin/DeleteManager")]
		[HttpGet]
		public IActionResult DeleteManager(int id)
		{
			var user = _userRepository.Managers.Where(w => w.Id == id).FirstOrDefault();
			if (user != null)
			{
				return View(user);
			}
			return RedirectToAction("GetAllManagers");
		}
	}
}