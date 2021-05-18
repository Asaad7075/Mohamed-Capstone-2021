using DomainModels;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class DonationsController : Controller
    {
        private DonationManager _donationManager = new DonationManager();
        private ClientManager _clientManager = new ClientManager();
        private OrderManager _orderManager = new OrderManager();
        // GET: Donations
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewDonations()
        {
            var donations = _donationManager.RetrieveAllDonationList();

            //only shows items that have been approved
            var filteredDonations = donations.Where(d => d.DonationStatus == "approved");

            return View(filteredDonations);
        }

        // GET: Donations/Details/5
        public ActionResult DonationItemDetails(int? id)
        {
            var item = _donationManager.RetrieveDonationByDonationID((int)id);
            return View(item);
        }

        public ActionResult ApplyForDonationItem(int? id)
        {
            int clientId;
            string email = User.Identity.Name;
            try
            {
                // Get client id by email
                clientId = _clientManager.GetClientIDByEmail(email);

                if (clientId.Equals(null))
                {
                    ViewBag.Message = "Client ID does not exist in BoT DB";
                    return View("DonationItemDetails", id);
                }
                else
                {
                    _orderManager.AddOrder(clientId, (int)id, DateTime.Now.ToShortDateString().Replace("/", "-"));
                }


                var donations = _donationManager.RetrieveAllDonationList();

                var filteredDonations = donations.Where(d => d.DonationStatus == "approved");
                return View("ViewDonations", filteredDonations);
            }

            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        /*
      Asaad Mohamed
       Created: 2021/04/30
       This for create new donation
    */
        // GET: Donations/Create
        public ActionResult CreateDonation(bool Message = false)
        {
            ViewBag.message = Message;
            return View();
        }

        // POST: Donations/Create
        [HttpPost]
        public ActionResult CreateDonation(Donation donation)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (donation.DropOff == true && donation.EmailReceipt == true && donation.MailReceipt == true)
                    {
                        donation.DropOff = false;
                        donation.EmailReceipt = false;
                        donation.MailReceipt = false;
                    }
                    if (donation.DonationStatus == null)
                    {
                        donation.DonationStatus = "";

                    }
                    if (_donationManager.InsertDonation(donation))
                    {
                        // TempData["message"] = string.Format("Your application hass been submitted successfuly");
                        // ViewBag.Message = "Your application hass been submitted successfuly";
                        return RedirectToAction("Index", "Donors");
                    }
                }
                catch
                {
                    //TempData["errorMesssage"] = string.Format("application Not submitted");
                    return View();
                }
            }

            return View();

        }

    }
}
