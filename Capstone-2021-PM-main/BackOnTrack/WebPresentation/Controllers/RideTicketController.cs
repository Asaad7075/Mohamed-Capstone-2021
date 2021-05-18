using DomainModels;
using DomainModels.Services;
using DomainModels.Tickets;
using LogicInterfaces;
using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    
    public class RideTicketController : Controller
    {
        private IServiceManager _serviceManager = new ServiceManager();
        private IClientManager _clientManager = new ClientManager();
        private IRideTicketManager _rideTicketManager = new RideTicketManager();
        private IServiceProviderManager _serviceProviderManager = new ServiceProviderManager();
        

        // GET: RideTicket/Edit/5
        public ActionResult CreateRideTicket(RideTicketVM rideTicket)
        {
            try
            {
                rideTicket.FetchStreetAddressLineOne = "";
                rideTicket.FetchStreetAddressLineTwo = "";
                rideTicket.FetchCity = "";
                rideTicket.FetchState = "";
                rideTicket.FetchZipCode = "";
                rideTicket.RequiresHAV = false;
                View(rideTicket);
            }
            catch (Exception)
            {

                return new HttpNotFoundResult();
            }

            return View(rideTicket);
        }



        // POST: RideTicket/Edit/5
        [HttpPost]
        public ActionResult CreateRideTicket(RideTicketVM rideTicket, RideTicketVM rideticket)
        {
            bool ticketAdded = false;
            try
            {
                ////ticket standards
                rideTicket.TicketType = 3;
                rideTicket.WasEdited = false;
                ticketAdded = _rideTicketManager.AddTicket(rideticket);
                return RedirectToAction("Index", "Home", rideTicket);
            }
            catch
            {
                return new HttpNotFoundResult();
            }
        }

    }
}
