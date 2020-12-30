using CarRental.Abstract;
using CarRental.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class EFCarRepository : ICarRepository
	{
		private ApplicationContext _context;
		public EFCarRepository(ApplicationContext context)
		{
			_context = context;
		}
		public IEnumerable<Car> Cars => _context.Cars;

		public bool DeleteCar(int id)
		{
			var carInDb = _context.Cars.Where(w => w.Id == id).FirstOrDefault();
			if (carInDb != null)
			{
				_context.Cars.Remove(carInDb);
				_context.SaveChanges();
				return true;
			}
			return false;
		}
		public bool SaveCar(CarViewModel carFromView)
		{
			Car car = new Car();
			car.Id = carFromView.Id;
			car.Model = carFromView.Model;
			car.Color = carFromView.Color;
			car.Class = carFromView.Class;
			car.Description = carFromView.Description;
			car.Name = carFromView.Name;
			car.RentalPrice = carFromView.RentalPrice;
			byte[] imageData = null;
			using (var binaryReader = new BinaryReader(carFromView.Avatar.OpenReadStream()))
			{
				imageData = binaryReader.ReadBytes((int)carFromView.Avatar.Length);
			}
			// установка массива байтов
			car.ImageData = imageData;
			if (car.Id == 0)
			{
				if (_context.Cars.Count() == 0)
				{
					car.Id = 1;
				}
				else
				{
					car.Id = _context.Cars.Max(m => m.Id) + 1;
				}
				//byte[] imageData = null;
				//using (var binaryReader = new BinaryReader(carFromView.Avatar.OpenReadStream()))
				//{
				//	imageData = binaryReader.ReadBytes((int)carFromView.Avatar.Length);
				//}
				//// установка массива байтов
				//car.ImageData = imageData;
				_context.Cars.Add(car);
			}
			else
			{
				Car dbCar = _context.Cars.Find(car.Id);
				if (dbCar != null)
				{
					dbCar.Model = car.Model;
					dbCar.Color = car.Color;
					dbCar.Class = car.Class;
					dbCar.Description = car.Description;
					dbCar.Name = car.Name;
					dbCar.RentalPrice = car.RentalPrice;
					dbCar.ImageData = car.ImageData;
				}
			}
			var result = _context.SaveChanges();
			if (result > 0)
				return true;
			else
				return false;
		}
	}
}
