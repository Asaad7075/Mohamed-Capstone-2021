using DomainModels;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    /// <summary>
    /// Asaad Mohamed
    /// created: 2021/04/09
    /// This donor controller that navegate to create new donor information 
    /// in donor information page
    /// </summary>
    public class DonorsController : Controller
    {
        private IDonorManager _donorManager;

        public DonorsController()
        {
            _donorManager = new DonorManager();
        }
        /// <summary>
        /// Asaad Mohamed
        /// created: 2021/04/09
        /// This the home controller for donor 
        /// </summary>
        /// <returns></returns>
        // GET: Donors
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Asaad Mohamed
        /// created: 2021/04/09
        /// </summary>
        /// <returns></returns>
        ///
        // [Authorize[Authorize(Roles = "Admin, Manager")]]
        // GET: Donor
        public ActionResult ViewAllDonors()
        {
            List<Donor> donors = _donorManager.RetrieveAllDonorListByActive();
            return View(donors);

        }

        // GET: Donors/Details/5
        /// <summary>
        /// Asaad Mohamed
        /// Created:  2021/04/09
        /// This is returns a detail of a Donor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DonorDetails(int id)
        {
            var donordetail = _donorManager.GetDonorById(id);
            ViewBag.Title = "Donor Details";
            return View(donordetail);
        }

        /// <summary>
        /// Asaad Mohamed
        /// created: 2021/04/09
        /// creating Donor information
        /// </summary>
        /// <returns></returns>
        // [Authorize(Roles = "Admin, Donor")]
        // GET: Donor/Create
        public ActionResult ApplicationInformation()
        {
            ViewBag.Titel = "Add new donor";
            return View();
        }

        // POST: Donor/Create
        //[Authorize(Roles = "Admin, Donor")]
        [HttpPost]
        public ActionResult ApplicationInformation(Donor donor)
        {

            if (ModelState.IsValid)
            {

                try
                {

                    if (donor.EIN == null)
                    {
                        donor.EIN = "";

                    }
                    if (donor.SS == null)
                    {
                        donor.SS = "";

                    }
                    _donorManager.InsertDonor(donor);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            // return Content("Your application has been submitted successfully!");
            return View();
        }


    }
}

