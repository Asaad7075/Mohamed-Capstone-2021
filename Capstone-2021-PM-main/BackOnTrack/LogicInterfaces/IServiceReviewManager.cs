using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/26
    /// 
    /// Interface for service review manager methods.
    /// </summary>

    public interface IServiceReviewManager
    {
        bool AddServiceReviewFromUser(ServiceReview serviceReview);
        List<ServiceReview> RetrieveAllServiceReviews();
        ServiceReview RetrieveAllServiceReviewsById(int id);
        bool EditServiceReviewFromClient(ServiceReview oldServiceReview, ServiceReview newServiceReview);
        bool DeleteServiceReview (ServiceReview serviceReview);
    }
}
