using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
using DataAccessInterfaces;
using System.Collections;
using DataAccessLayer;
using DomainModels.Tickets;

namespace LogicLayer
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/07
    /// 
    /// This logic layer class handles the processing of the ride reviews.
    /// </summary>
    public class RideReviewManager : IRideReviewManager
    {
        private IRideReviewAccessor _rideReviewAccessor;
        private List<RideReviewVM> _tickets;//used to handle retriving the tickets for reviews

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// Default constructor for ride review manager.
        /// </summary>
        public RideReviewManager()
        {
            _rideReviewAccessor = new RideReviewAccessor();
        }
        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// Ride review manager taking the IRideReviewAccessor interface as a parameter to access the ride review ccessor
        /// interface at runtime
        /// </summary>
        /// <param name="rideReviewAccessor"></param>
        public RideReviewManager(IRideReviewAccessor rideReviewAccessor)
        {
            _rideReviewAccessor = rideReviewAccessor;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// 
        /// AddRideReviewFromDriver will allow a driver to Add a review on client
        /// </summary>
        public bool AddRideReviewFromDriver(RideReview rideReviewFromDriver)
        {
            var rrd = rideReviewFromDriver;
            bool result = false;
            try
            {
                if (rrd.DriverComment.Trim().Equals("") || rrd.DriverClientRating > 5 || rrd.DriverClientRating < 1)
                {
                    throw new ApplicationException("Ride Review could not be added to the DB at this time.\n" +
                        "Please check your comment is not blank and your rating\n" +
                        "is between 1 and 5");
                }
                else
                {
                    result = (1 == _rideReviewAccessor.InsertRideReview(rideReviewFromDriver));
                }

                if (result.Equals(0))
                {
                    throw new ApplicationException("Ride Review could not be added to the DB at this time");
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Adding ride review failed \n\n", ex);
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/28
        /// 
        /// EditRideReviewFromDriver will take a selected ride review as the oldRideReview and allow
        /// a user to enter new updated information into it as newRideReview. the newRideReview will 
        /// overwrite the oldRideReview and take its place.
        /// </summary>
        /// <param name="oldRideReview"></param>
        /// <param name="newRideReview"></param>
        /// <returns></returns>
        public bool EditRideReviewFromDriver(RideReview oldRideReview, RideReview newRideReview)
        {
            bool result = false;
            try
            {
                if (oldRideReview == null || newRideReview == null || newRideReview.DriverClientRating > 5
                    || newRideReview.DriverClientRating < 1 || newRideReview.DriverComment.Trim().Equals(""))
                {
                    throw new ApplicationException("Rating, Comment, or both are invalid. \n" +
                                                    "Can not update with the input provided.");
                }
                else
                {
                    result = (1 == _rideReviewAccessor.UpdateRideReviewFromDriver(oldRideReview, newRideReview));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to edit the selected ride review.\n\n" + ex.Message + ex.InnerException);
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/03
        /// 
        /// Removes the ride review that was passed from the list of ride reviews
        /// </summary>
        /// <param name="rideReview"></param>
        /// <returns></returns>
        /// </summary>
        public bool RemoveRideReviewFromDriver(RideReviewVM rideReview)
        {
            bool result = false;
            try
            {
                result = (1 == _rideReviewAccessor.DeleteRideReviewFromDriver(rideReview));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to delete the selected ride review. \n\n" + ex.Message + ex.InnerException);
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// 
        /// RerieveAllRideReviewsFromDriver will retive all the reviews from drivers.
        /// </summary>
        public List<RideReviewVM> RetrieveAllRideReviews()
        {
            List<RideReviewVM> rideReviews = new List<RideReviewVM>();
            try
            {
                rideReviews = _rideReviewAccessor.SelectAllRideReviews();
                if (rideReviews == null)
                {
                    throw new ApplicationException("Select all ride reviews returns null.");
                }
                for (int i = 0; i < rideReviews.Count(); i++)
                {
                    if (rideReviews[i].DriverComment == null)
                    {
                        rideReviews[i].DriverComment = "n";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("RideReview Data is Unavailable.\n\n" 
                    +  ex.Message + "\n\n" + ex.InnerException);
            }
            return rideReviews;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/14
        /// 
        /// Gets ride review tickets for the employee that is actively logged in. This method is passed the
        /// employeeID and returns a list of the ride tickes that employee took.
        /// 
        /// Updated: 2021/04/03
        /// Modified to not allow a duplicate ticket to populate
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<RideReviewVM> RetrieveRideTicketsByEmployeeID(int employeeID)
        {
            _tickets = new List<RideReviewVM>();
            try
            {
                _tickets = _rideReviewAccessor.SelectTicketsByEmployeeID(employeeID);
                if (_tickets.Count() == 0)
                {
                    throw new ApplicationException("Select rides by employeeID returns null.");
                }
                //does not allow duplicate tickets to be shown
                for (int i = 0; i < _tickets.Count; i++)
                {
                    int id = _tickets[i].TicketID;
                    int dub = 0;
                    for (int j = 0; j < _tickets.Count; j++)
                    {
                        if (id == _tickets[j].TicketID)
                        {
                            dub++;
                            if (dub > 1)
                            {
                                _tickets.RemoveAt(j--);//j-- ensures if the last entry will not be a duplivate aswell
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ride Data is Unavailable.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return _tickets;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/06
        /// 
        /// Gets the ride reviews by ticket id and client id. This ensures that there are not duplicate tickets
        /// for the client to review. This also gets only the tickets for that client signed in.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<RideReviewVM> RetrieveRideTicketsByClientID(int clientID)
        {
            _tickets = new List<RideReviewVM>();
            try
            {
                _tickets = _rideReviewAccessor.SelectTicketsByClientID(clientID);
                if (_tickets == null)
                {
                    throw new ApplicationException("Select rides by clientID returns null.");
                }
                //does not allow duplicate tickets to be shown
                for (int i = 0; i < _tickets.Count; i++)
                {
                    int id = _tickets[i].TicketID;
                    int dub = 0;
                    for (int j = 0; j < _tickets.Count; j++)
                    {
                        if (id == _tickets[j].TicketID)
                        {
                            dub++;
                            if (dub > 1)
                            {
                                _tickets.RemoveAt(j--);//j-- ensures if the last entry will not be a duplivate aswell
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ride Data is Unavailable.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return _tickets;
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/09
        /// 
        /// Will retrieve all the ride tickets that can be reviewed
        /// </summary>
        /// <returns></returns>
        public List<RideReviewVM> RetrieveAllRideTicketToReview()
        {
            _tickets = new List<RideReviewVM>();
            try
            {
                _tickets = _rideReviewAccessor.SelectAllTicketsToReview();
                if (_tickets == null)
                {
                    throw new ApplicationException("Select rides by clientID returns null.");
                }
                //does not allow duplicate tickets to be shown
                for (int i = 0; i < _tickets.Count; i++)
                {
                    int id = _tickets[i].TicketID;
                    int dub = 0;
                    for (int j = 0; j < _tickets.Count; j++)
                    {
                        if (id == _tickets[j].TicketID)
                        {
                            dub++;
                            if (dub > 1)
                            {
                                _tickets.RemoveAt(j--);//j-- ensures if the last entry will not be a duplicate aswell
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ride Data is Unavailable.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return _tickets;
        }



        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/10
        /// Adds a ride review from a client 
        /// </summary>
        /// <returns></returns>
        public bool AddRideReviewFromClient(RideReview rideReviewFromClient)
        {
            bool result = false;
            try
            {
                //rideReviewFromClient.DriverComment = "n"; // trying to set default paramiters for driver comment and rating
                //rideReviewFromClient.DriverClientRating = 1;

                bool rideReviewAdded = (1 == _rideReviewAccessor.InsertClientRideReview(rideReviewFromClient));
                if (rideReviewAdded == false)
                {
                    throw new ApplicationException("Ride Review could not be added to the DB at this time");
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Adding ride review failed \n\n", ex);
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/16
        /// retrievs all the reviews all the clients made from the data access layer
        /// </summary>
        /// <returns></returns>
        public List<RideReviewVM> RetrieveAllClientRideReviews()
        {
            List<RideReviewVM> rideReviews = new List<RideReviewVM>();
            try
            {
                rideReviews = _rideReviewAccessor.SelectAllClientRideReviews();
                if (rideReviews == null)
                {
                    throw new ApplicationException("Select all ride reviews returns null.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("RideReview Data is Unavailable.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return rideReviews;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/18
        /// updates an existing ride review with a new comment, rating, or both
        /// </summary>
        /// <returns></returns>
        public bool EditRideReviewFromClient(RideReview oldRideReview, RideReview newRideReview)
        {
            bool result = false;
            try
            {
                if (oldRideReview != null && newRideReview != null)
                {
                    result = (1 == _rideReviewAccessor.UpdateRideReviewFromClient(oldRideReview, newRideReview));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to edit the selected ride review.\n\n" + ex.Message + ex.InnerException);
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/23
        /// Will retrive a ride review by the provided review id if that review exists
        /// </summary>
        /// <returns></returns>
        public RideReviewVM RetrieveClientRideReviewByReviewID(int reviewID)
        {
            RideReviewVM rideReview;
            try
            {
                rideReview = _rideReviewAccessor.SelectRideReviewsByReviewID(reviewID);
                if (rideReview == null)
                {
                    throw new ApplicationException("Selected ride review returns null.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("RideReview Data is Unavailable.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return rideReview;
        }
    }
}
