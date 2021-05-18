using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
using DomainModels.Tickets;

namespace LogicInterfaces
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/07
    /// Updated: 2021/04/10
    /// Updated: 2021/04/17
    /// 
    /// Interface that allows a Driver to review and comment on a Client.
    /// </summary>
    public interface IRideReviewManager
    {
        bool AddRideReviewFromDriver(RideReview rideReviewFromDriver);
        List<RideReviewVM> RetrieveAllRideReviews();
        List<RideReviewVM> RetrieveRideTicketsByEmployeeID(int employeeID);
        bool EditRideReviewFromDriver(RideReview oldRideReview, RideReview newRideReview);
        bool RemoveRideReviewFromDriver(RideReviewVM rideReview);
        List<RideReviewVM> RetrieveRideTicketsByClientID(int clientID);
        List<RideReviewVM> RetrieveAllRideTicketToReview();

        bool AddRideReviewFromClient(RideReview rideReviewFromClient);
        List<RideReviewVM> RetrieveAllClientRideReviews();
        bool EditRideReviewFromClient(RideReview oldRideReview, RideReview newRideReview);
        RideReviewVM RetrieveClientRideReviewByReviewID(int reviewID);
    }
}
