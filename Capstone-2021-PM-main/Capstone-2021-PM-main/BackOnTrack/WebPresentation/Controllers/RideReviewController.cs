using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModels;
using DomainModels.Tickets;
using WebPresentation.Models;
using LogicInterfaces;
using System.Dynamic;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebPresentation.Controllers
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/04/06
    /// 
    /// RideReviewController will handle the CRUD functions of the web side ride reviews, 
    /// mostly for clients 
    /// </summary>
    public class RideReviewController : Controller
    {
        private IRideReviewManager _rideReviewManager = new RideReviewManager();
        private IClientManager _clientManager = new ClientManager();

        
        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/06
        /// Views recent rides to see the details or make a review.
        /// Updated: 2021/04/09
        /// </summary>
        /// <returns></returns>
        // GET: Ride to review
        public ActionResult ViewRecentRides()
        {
            string email = User.Identity.Name;
            List<RideReviewVM> rideReviews = null;
            try
            {
                // gets all the ride tickets
                rideReviews = _rideReviewManager.RetrieveAllRideTicketToReview();
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
            return View(rideReviews);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/06
        /// Updated: 2021/04/11, 2021/04/16
        /// Creates the Ride review from the client. This will take the data from the selected
        /// ride ticket and auto populate fields for the ride review. 
        /// </summary>
        /// <param name="rideReview"></param>
        /// <returns></returns>
        //GET: RideReview/AddRideReviewFromClient
        public ActionResult CreateReview(RideReviewVM rideReview)
        {
            return View(rideReview);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/06
        /// Updated: 2021/04/11, 2021/04/16
        /// Creates the Ride review from the client by using the auto populated data and 
        /// the new valid data and adding it to the database
        /// </summary>
        /// <param name="rideReview"></param>
        /// <returns></returns>
        // POST: RideReview/Create
        [HttpPost]
        public ActionResult CreateReview(RideReview rideReview)
        {
            try
            {
                if (rideReview.RideReviewID.Equals(0))
                {
                    _rideReviewManager.AddRideReviewFromClient(rideReview);
                    return RedirectToAction("/ViewAllRideReviews");
                }
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
            return View(rideReview);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/06
        /// Allows control for viewing the ride reviews. 
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAllRideReviews()
        {
            List<RideReviewVM> rideReviews = null;
            try
            {
                rideReviews = _rideReviewManager.RetrieveAllClientRideReviews();
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
            return View(rideReviews);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/11
        /// Controller method for sending the user to an edit page with pre-populated data
        /// for the client to update their review.
        /// </summary>
        /// <param name="ridereview"></param>
        /// <returns></returns>
        // GET: RideReview/Edit/5
        public ActionResult EditRideReview(RideReviewVM ridereview)
        {
            return View(ridereview);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/11
        /// Updated: 2021/04/23
        /// Controller method for the logic of editing the selected ride review
        /// </summary>
        /// <param name="oldRidereview"></param>
        /// <param name="newRidereview"></param>
        /// <returns></returns>
        // POST: RideReview/Edit/5
        [HttpPost]
        public ActionResult EditRideReview(RideReviewVM oldRidereview, RideReviewVM newRidereview)
        {
            RideReviewVM newRR = oldRidereview;
            //set resets old ride review
            RideReviewVM oldRR;// = _rideReviewManager.RetrieveClientRideReviewByReviewID(newRidereview.RideReviewID);

            try
            {
                oldRR = _rideReviewManager.RetrieveClientRideReviewByReviewID(newRidereview.RideReviewID);
                _rideReviewManager.EditRideReviewFromClient(oldRR, newRR);

                return RedirectToAction("/ViewAllRideReviews");
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/23
        /// displays the window to confirm the delete of a ride review
        /// </summary>
        /// <param name="ridereview"></param>
        /// <returns></returns>
        //GET: RideReview/Delete/5
        public ActionResult DeleteRideReview(RideReviewVM ridereview)
        {
            return View(ridereview);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/23
        /// Logic for deleting the ride review selected when a user confirms it is to be deleted.
        /// </summary>
        /// <param name="ridereview"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        //POST: RideReview/Delete/5
        [HttpPost]
        public ActionResult DeleteRideReview(RideReviewVM ridereview, FormCollection collection)
        {
            try
            {
                _rideReviewManager.RemoveRideReviewFromDriver(ridereview);
                return RedirectToAction("/ViewAllRideReviews");
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
        }


    }
}
