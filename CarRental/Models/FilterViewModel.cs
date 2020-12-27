using CarRental.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class FilterViewModel
	{
        public FilterViewModel( ClassCar? classCar, string model)
        {
            SelectedClass = classCar;
            SelectedModel = model;
        }
        public ClassCar? SelectedClass { get; private set; }   // выбранный класс
        public string SelectedModel { get; private set; }    // введенное имя модели
    }
}
