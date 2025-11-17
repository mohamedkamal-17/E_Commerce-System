using E_commerceManagementSystem.BLL.Manager.OrderManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_System.Schema
{
    public class Query
    {   private readonly IOrderManager _orderManager;

        public Query(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        //public IEnumerable<Order> GetOrders()
        //{
        //    var orders = _orderManager.GetAll();
            
        //}
    }
}
