using DomainModels;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace WebPresentation.Controllers
{
    public class OrdersController : Controller
    {

        private DonationManager _donationManager = new DonationManager();
        private IClientManager _clientManager = new ClientManager();
        private OrderManager _orderManager = new OrderManager();
        // GET: Orders
        public ActionResult ViewMyOrders()
        {
            string email = User.Identity.Name;
            int clientId = _clientManager.GetClientIDByEmail(email);
            var clientOrders = new List<Order>();

            try
            {
                var orders = _orderManager.GetOrdersByClientID(clientId);

                clientOrders = orders.OrderBy(x => x.DateRequested)
                                     .GroupBy(x => x.DateRequested)
                                     .Select(x => x.First()).ToList();

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(clientOrders);
        }

        public ActionResult ViewOrderDetails(string dateRequested)
        {
            string email = User.Identity.Name;
            int clientId = _clientManager.GetClientIDByEmail(email);
            var clientRequests = _orderManager.GetOrdersByClientID(clientId);
            List<Donation> orderItems = new List<Donation>();
            foreach (var d in clientRequests)
            {
                if (dateRequested == d.DateRequested)
                {
                    var temp = _donationManager.RetrieveDonationByDonationID((int)d.DonationID);
                    orderItems.Add(temp);
                }
            }

            return View(orderItems);
        }
    }
}
