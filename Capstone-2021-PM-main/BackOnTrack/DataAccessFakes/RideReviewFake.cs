using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DomainModels;
using DomainModels.Tickets;

namespace DataAccessFakes
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/07
    /// 
    /// interface for the RideReviewAccessor class
    /// </summary>
    public class RideReviewFake : IRideReviewAccessor
    {
        private List<RideReviewVM> _rideReviews = new List<RideReviewVM>();
        private RideReviewVM _rideReview;

        private List<RideReviewVM> _tickets = new List<RideReviewVM>();

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// 
        /// Ride review fakes constructor to make fake objects to use for ride reviews.
        /// </summary>
        public RideReviewFake()
        {
            _tickets.Add(new RideReviewVM
            {
                ClientID = 100020,
                ClientFirstName = "John",
                ClientLastName = "Doe",
                TicketID = 112233,
                TicketType = 3,
                DriverID = 100000,
                EmployeeFirstName = "Brad",
                EmployeeLastName = "Majors" // copy/past EmployeeLastName from ride review vm
            });
            _tickets.Add(new RideReviewVM
            {
                ClientID = 100020,
                ClientFirstName = "John",
                ClientLastName = "Doe",
                TicketID = 223344,
                TicketType = 3,
                DriverID = 100000,
                EmployeeFirstName = "Brad",
                EmployeeLastName = "Majors"
            });
            _tickets.Add(new RideReviewVM
            {
                ClientID = 100020,
                ClientFirstName = "John",
                ClientLastName = "Doe",
                TicketID = 334455,
                TicketType = 3,
                DriverID = 100001,
                EmployeeFirstName = "Janet",
                EmployeeLastName = "Weiss"
            });


            _rideReviews.Add(new RideReviewVM
            {
                RideReviewID = 200000,
                TicketID = 100000,
                ClientID = 100000,
                ClientDriverRating = 5,
                ClientComment = "Fantastic Fake Ride!",
                DriverID = 100000,
                DriverClientRating = 5,
                DriverComment = "Nice fake rider, even gave a fake tip."
            });

            _rideReviews.Add(new RideReviewVM
            {
                RideReviewID = 100000,
                TicketID = 100004,
                ClientID = 100000,
                DriverID = 100000,
            });

            _rideReviews.Add(new RideReviewVM
            {
                RideReviewID = 100001,
                TicketID = 100001,
                ClientID = 100001,
                ClientDriverRating = 5,
                ClientComment = "Fantastic Fake Ride!",
                DriverID = 100001,
                DriverClientRating = 5,
                DriverComment = "Nice fake rider, even gave a fake tip."
            });

            _rideReviews.Add(new RideReviewVM
            {
                RideReviewID = 100002,
                TicketID = 100002,
                ClientID = 100002,
                DriverID = 100002,
                DriverClientRating = 4,
                DriverComment = "Nice fake rider, even gave a fake tip."
            });

            _rideReviews.Add(new RideReviewVM
            {
                RideReviewID = 100003,
                TicketID = 100003,
                ClientID = 100003,
                ClientDriverRating = 2,
                ClientComment = "OK fake ride....",
                DriverID = 100003,
                DriverClientRating = 2,
                DriverComment = "Angry fake rider, even gave a fake tip."
            });
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/03
        /// 
        /// tests the RemoveRideReviewFromDriver method using fake data
        /// </summary>
        /// <param name="rideReview"></param>
        /// <returns></returns>
        public int DeleteRideReviewFromDriver(RideReviewVM rideReview)
        {
            int result = 0;
            if (_rideReviews.Contains(rideReview))
            {
                _rideReviews.Remove(rideReview);
                if (!_rideReviews.Contains(rideReview))
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// 
        /// Fake InsertRideReview to simulate adding a ride review to a DB.
        /// </summary>
        public int InsertRideReview(RideReview rideReview)
        {
            int result = 0;
            if (_rideReviews.Contains(rideReview))
            {
                throw new Exception("ReviewID: " + rideReview.RideReviewID + "\nTicketID: " + rideReview.TicketID + "\nDriverID: " + rideReview.DriverID
                    + "\nClientID: " + rideReview.ClientID + "\n\nAlready exists in the datavase");
            }
            else
            {
                _rideReviews.Add((RideReviewVM)rideReview);
                foreach (var currRideReview in _rideReviews)
                {
                    if (currRideReview.Equals(rideReview))
                    {
                        result = 1;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// 
        /// Fake SelectallRideReviews to simulate getting all the RideReviews from a DB.
        /// </summary>
        public List<RideReviewVM> SelectAllRideReviews()
        {
            return _rideReviews;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/09
        /// 
        /// Fake SelectAllTicketsToReview to simulate getting all the Ride Tickets from a DB.
        /// </summary>
        public List<RideReviewVM> SelectAllTicketsToReview()
        {
            return _tickets;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/06
        /// 
        /// This data fake simulates getting ride review tickets and data from the database by the clients id
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<RideReviewVM> SelectTicketsByClientID(int clientID)
        {
            List<RideReviewVM> tickets = new List<RideReviewVM>();

            foreach (var ticket in _tickets)
            {//this will get all the 'Ride Tickets' and ignore Delivery & PickUp tickets
                if (ticket.TicketType == 3 && ticket.ClientID == clientID)
                {
                    tickets.Add(ticket);
                }
            }
            return tickets;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/21
        /// 
        /// Fake SelectallRideReviews to simulate getting all the RideReviews from a DB.
        /// </summary>
        public List<RideReviewVM> SelectTicketsByEmployeeID(int employeeID)
        {
            List<RideReviewVM> tickets = new List<RideReviewVM>();

            foreach (var ticket in _tickets)
            {//this will get all the 'Ride Tickets' and ignore Delivery & PickUp tickets
                if (ticket.TicketType == 3 && ticket.DriverID == employeeID)
                {
                    tickets.Add(ticket);
                }
            }
            return tickets;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/28
        /// fake accessor for updating a ride review
        /// </summary>
        /// <param name="oldRideReview"></param>
        /// <param name="newRideReview"></param>
        /// <returns></returns>
        public int UpdateRideReviewFromDriver(RideReview oldRideReview, RideReview newRideReview)
        {
            int result = 0;

            try
            {
                foreach (var r in _rideReviews)
                {
                    if (oldRideReview.RideReviewID == newRideReview.RideReviewID)
                    {
                        oldRideReview.DriverClientRating = newRideReview.DriverClientRating;
                        oldRideReview.DriverComment = newRideReview.DriverComment;
                        result = 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

      
        
        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/14
        /// 
        /// Fake InsertRideReview to simulate adding a ride review from client to a DB.
        /// </summary>
        public int InsertClientRideReview(RideReview rideReview)
        {
            int result = 0;
            if (_rideReviews.Contains(rideReview))
            {
                throw new Exception("ReviewID: " + rideReview.RideReviewID + "\nTicketID: " + rideReview.TicketID + "\nDriverID: " + rideReview.DriverID
                    + "\nClientID: " + rideReview.ClientID + "\n\nAlready exists in the datavase");
            }
            else
            {
                _rideReviews.Add((RideReviewVM)rideReview);
                foreach (var currRideReview in _rideReviews)
                {
                    if (currRideReview.Equals(rideReview))
                    {
                        result = 1;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/14
        /// 
        /// Fake Select all ride reviews to simulate getting all ride review 
        /// from client from a DB.
        /// </summary>
        public List<RideReviewVM> SelectAllClientRideReviews()
        {
            return _rideReviews;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/19
        /// 
        /// Fake update ride reviews to simulate updating ride review 
        /// from client from a DB for testing
        /// </summary>
        public int UpdateRideReviewFromClient(RideReview oldRideReview, RideReview newRideReview)
        {
            int result = 0;

            try
            {
                foreach (var r in _rideReviews)
                {
                    if (oldRideReview.RideReviewID == newRideReview.RideReviewID)
                    {
                        oldRideReview.ClientDriverRating = newRideReview.ClientDriverRating;
                        oldRideReview.ClientComment = newRideReview.ClientComment;
                        result = 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/22
        /// 
        /// Fake Select ride reviews by the review id to for testing getting a ride review 
        /// by the id.
        /// </summary>
        public RideReviewVM SelectRideReviewsByReviewID(int rideReviewID)
        {
            RideReviewVM rideReview = new RideReviewVM();
            rideReview = _rideReviews[2]; //id:100001

            return rideReview;
        }

    }
}
