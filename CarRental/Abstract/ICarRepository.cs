using CarRental.Entities;
using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Abstract
{
	public interface ICarRepository
	{
		IEnumerable<Car> Cars { get; }
		bool SaveCar(CarViewModel game);
		bool DeleteCar(int gameId);
	}
}
