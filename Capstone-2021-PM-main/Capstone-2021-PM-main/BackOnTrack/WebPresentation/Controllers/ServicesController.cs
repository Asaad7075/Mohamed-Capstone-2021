using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DomainModels.Services;
using WebPresentation.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using LogicLayer;
using LogicInterfaces;
using DomainModels.Tickets;

namespace WebPresentation.Controllers
{
    public class ServicesController : Controller
    {
        private ServiceManager _serviceManager = new ServiceManager();
        private ClientManager _clientManager = new ClientManager();
        private IRideTicketManager _rideTicketManager = new RideTicketManager();
        private IServiceProviderManager _serviceProviderManager = new ServiceProviderManager();
        private ServiceReviewManager _serviceReviewManager = new ServiceReviewManager();

        // GET: Services
        public ActionResult Index()
        {
            return View();
        }

        // GET: Services
        public ActionResult Available()
        {
            return View(_serviceManager.RetrieveAllServices());
        }

        // GET: Services/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/24
        /// 
        /// Controller method that allows
        /// clients to view service schedules,
        /// and select a date/time slot.
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        // GET: Services/ServiceSchedules
        [HttpGet]
        public ActionResult ServiceSchedules(int? serviceId, int? scheduleID)
        {
            if (scheduleID == null)
            {
                if (serviceId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(_serviceManager.RetrieveAllServiceSchedulesByID((int)serviceId));
            }
            if (ModelState.IsValid)
            {
                if (serviceId == null || scheduleID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                try
                {
                    var userId = this.User.Identity.GetUserId();

                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = userManager.Users.First(u => u.Id == userId);
                    var email = user.Email;

                    /*Get logged in clientID, fetch the client associate with the ID, 
                     add client id to the serviceschedule
                     */
                    int clientID = _clientManager.GetClientIDByEmail(email);
                    _serviceManager.EditClientSchedule(clientID, (int)scheduleID);
                    return View(_serviceManager.RetrieveAllServiceSchedulesByID((int)serviceId));
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }



        [HttpPost]
        public ActionResult ServiceSchedules(ServiceVM service)
        {
            RideTicketVM rideTicket = new RideTicketVM();
            try
            {
                //gets current user
                var userId = this.User.Identity.GetUserId();
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = userManager.Users.First(u => u.Id == userId);
                var email = user.Email;
                //logged in clients id
                int? clientID = _clientManager.GetClientIDByEmail(email);
                //setting the client id to the schedule
                _serviceManager.EditClientSchedule((int)clientID, service.ServiceID);
                //collects the data needed to make a ride ticket
                rideTicket = _rideTicketManager.DataCollectionForRTVM(service, (int)clientID, email);
                //Client
                rideTicket.FetchStreetAddressLineOne = "";
                rideTicket.FetchStreetAddressLineTwo = "";
                rideTicket.FetchCity = "";
                rideTicket.FetchState = "";
                rideTicket.FetchZipCode = "";
                rideTicket.RequiresHAV = false;

                return RedirectToAction("CreateRideTicket", "RideTicket", rideTicket);
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
        }



        [HttpGet]
        public ActionResult SavedSchedules()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.User.Identity.GetUserId();

                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = userManager.Users.First(u => u.Id == userId);
                    var email = user.Email;

                    /*Get logged in clientID, fetch the client associate with the ID, 
                     add client id to the serviceschedule
                     */
                    int clientID = _clientManager.GetClientIDByEmail(email);
                    return View(_serviceManager.RetrieveAllSavedServiceSchedulesByClientID(clientID));
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return View();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/06
        /// 
        /// Initiates add service review event 
        /// </summary>
        public ActionResult ServiceReview()
        {
            ViewBag.Title = "Add new Review";
            return View();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/06
        /// 
        /// Validates all fields are entered, then add to ServiceReviewList
        /// </summary>
        [HttpPost]
        public ActionResult ServiceReview(ServiceReview serviceReview)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (serviceReview.ServiceName == null)
                    {
                        serviceReview.ServiceName = "";

                    }
                    if (serviceReview.ProviderFirstName == null)
                    {
                        serviceReview.ProviderFirstName = "";

                    }
                    if (serviceReview.ProviderLastName == null)
                    {
                        serviceReview.ProviderLastName = "";

                    }
                    if (serviceReview.Rating == null)
                    {
                        serviceReview.Rating = "";

                    }
                    if (serviceReview.ClientComment == null)
                    {
                        serviceReview.ClientComment = "";

                    }
                    _serviceReviewManager.AddServiceReviewFromUser(serviceReview);
                    return RedirectToAction("ServiceReviewList");
                }
                catch (Exception)
                {
                    return View();
                }
            }

            return View();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/06
        /// 
        /// Displays list of existing service reviews.
        /// </summary>
        public ActionResult ServiceReviewList()
        {
            return View(_serviceReviewManager.RetrieveAllServiceReviews());
        }

        /// <summary>
        /// Chase Martin
        /// Created: 04/19/2021
        /// 
        /// Returns a view for editing selected service by ServiceID
        /// </summary>
        /// <returns></returns>
        public ActionResult EditServiceReview(int id)
        {
            return View(_serviceReviewManager.RetrieveAllServiceReviewsById(id));
        }

        /// <summary>
        /// Chase Martin
        /// Created: 04/19/2021
        /// 
        /// Returns a view for editing selected service by ServiceID with the old data auto filled into to the form for updating
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditServiceReview(ServiceReview oldServiceReview, ServiceReview newServiceReview)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    if (newServiceReview.ServiceName != null)
                    {
                        oldServiceReview.ServiceName = newServiceReview.ServiceName;

                    }
                    if (newServiceReview.ProviderFirstName != null)
                    {
                        oldServiceReview.ProviderFirstName = newServiceReview.ProviderFirstName;

                    }
                    if (newServiceReview.ProviderLastName != null)
                    {
                        oldServiceReview.ProviderLastName = newServiceReview.ProviderLastName;

                    }
                    if (newServiceReview.Rating != null)
                    {
                        oldServiceReview.Rating = newServiceReview.Rating;

                    }
                    if (newServiceReview.ClientComment != null)
                    {
                        oldServiceReview.ClientComment = newServiceReview.ClientComment;

                    }

                    _serviceReviewManager.AddServiceReviewFromUser(newServiceReview);
                    _serviceReviewManager.DeleteServiceReview(oldServiceReview);

                }

                catch (Exception)
                {
                    return RedirectToAction("ServiceReviewList");
                }
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _serviceManager = null;
            }
            base.Dispose(disposing);
        }
    }
}
