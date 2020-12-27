using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Abstract;
using CarRental.Entities;
using CarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "manager")]
    public class ManagerController : Controller
    {
        private string accountDetails = "3546 4747 4747 1111";
        private IOrderRepository _orderRepository;
        private IBillRepository _billRepository;
        private ICarRepository _carRepository;
        public ManagerController(IOrderRepository orderRepository, IBillRepository billRepository, ICarRepository carRepository)
        {
            _orderRepository = orderRepository;
            _billRepository = billRepository;
            _carRepository = carRepository;
        }
        [Route("Manager/Orders")]
        [HttpGet]
        public IActionResult Orders()
        {
            return View(_orderRepository.Orders);
        }        
        [Route("Manager/CreateBill")]
        [HttpGet]
        public IActionResult CreateBill(int orderId)
        {
            var newBill = new Bill();            
            var currOrder = _orderRepository.Orders.Where(w => w.Id == orderId).FirstOrDefault();
            newBill.UserId = currOrder.UserId;
            newBill.OrderId = orderId;
            newBill.TypeBill = TypeBill.Service;
            newBill.ToPay = currOrder.RentalTime * _carRepository.Cars.Where(w => w.Id == currOrder.CarId).FirstOrDefault().RentalPrice;
            newBill.AccountDetails = accountDetails;
            currOrder.Status = StatusOrder.Approved;
            _billRepository.SaveBill(newBill);
            _orderRepository.SaveOrder(currOrder);
            return RedirectToAction("Orders");
        }
        [Route("Manager/Cansel")]
        [HttpGet]
        public IActionResult Cansel(int orderId)
        {
            var order = _orderRepository.Orders.Where(w => w.Id == orderId).FirstOrDefault();
            if(order != null)
            {
                return View(order);
            }
            return RedirectToAction("Orders");
        }
        [Route("Manager/Cansel")]
        [HttpPost]
        public IActionResult Cansel(Order order)
        {
            var tmpOrder = _orderRepository.Orders.Where(w => w.Id == order.Id).FirstOrDefault();
            if (tmpOrder != null)
            {
                tmpOrder.Status = StatusOrder.Сanceled;
                tmpOrder.Comment = order.Comment;
                _orderRepository.SaveOrder(tmpOrder);
                return RedirectToAction("Orders");
            }
            return NotFound();
        }
        [Route("Manager/CompletOrder")]
        [HttpGet]
        public IActionResult CompletOrder(int orderId)
        {
            var currOrder = _orderRepository.Orders.Where(w => w.Id == orderId).FirstOrDefault();
            currOrder.Status = StatusOrder.Completed;
            _orderRepository.SaveOrder(currOrder);
            return RedirectToAction("Orders");
        }
        [Route("Manager/CreateRepairBill")]
        [HttpGet]
        public IActionResult CreateRepairBill(int orderId)
        {
            HttpContext.Session.SetString("OrderId", orderId.ToString());
            return View();
        }
        [Route("Manager/CreateRepairBill")]
        [HttpPost]
        public IActionResult CreateRepairBill(Bill bill)
        {
            var newBill = bill;
            var orderId = int.Parse(HttpContext.Session.GetString("OrderId"));
            var currOrder = _orderRepository.Orders.Where(w => w.Id == orderId).FirstOrDefault();
            newBill.UserId = currOrder.UserId;
            newBill.OrderId = orderId;
            newBill.TypeBill = TypeBill.Repair;
            newBill.ToPay = bill.ToPay;
            newBill.AccountDetails = accountDetails;
            currOrder.Status = StatusOrder.Completed;
            _billRepository.SaveBill(newBill);
            _orderRepository.SaveOrder(currOrder);
            return RedirectToAction("Orders");
        }
    }
}