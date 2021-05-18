using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
using DomainModels.Tickets;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/07
    /// Updated: 2021/04/10
    /// Updated: 2021/04/17
    /// interface for the RideReviewAccessor class
    /// </summary>
    public interface IRideReviewAccessor
    {
        List<RideReviewVM> SelectAllRideReviews();
        int InsertRideReview(RideReview rideReview);
        List<RideReviewVM> SelectTicketsByEmployeeID(int employeeID);
        int UpdateRideReviewFromDriver(RideReview oldRideReview, RideReview newRideReview);
        int DeleteRideReviewFromDriver(RideReviewVM rideReview);
        List<RideReviewVM> SelectTicketsByClientID(int clientID);
        List<RideReviewVM> SelectAllTicketsToReview();

        int InsertClientRideReview(RideReview rideReviewFromClient);
        List<RideReviewVM> SelectAllClientRideReviews();
        int UpdateRideReviewFromClient(RideReview oldRideReview, RideReview newRideReview);
        RideReviewVM SelectRideReviewsByReviewID(int rideReviewID);

    }
}
