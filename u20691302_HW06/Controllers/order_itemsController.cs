using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using u20691302_HW06.Models;

namespace u20691302_HW06.Controllers
{
    public class order_itemsController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();
        // GET: Default
        public ActionResult Index( DateTime?orderDate, int? page, DateTime? currentFilterTextbox)
        {
            if (orderDate != default(DateTime))
            {
                page = 1;
            }
            else
            {
                orderDate = currentFilterTextbox;
            }

            ViewBag.CurrentFilterTextbox = orderDate;

            var orders = db.order_items.Include(p => p.product).Include(p => p.order).OrderBy(p => p.order.order_date).ToList();

            if (orderDate == default(DateTime))
            {
                orders = db.order_items.Include(p => p.product).Include(p => p.order).Where(p => p.order.order_date == orderDate).OrderBy(p => p.order.order_date).ToList();
            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
        }
    }
}
