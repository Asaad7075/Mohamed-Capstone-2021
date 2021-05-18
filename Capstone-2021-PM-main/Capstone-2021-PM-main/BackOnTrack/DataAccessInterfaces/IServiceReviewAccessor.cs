using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/02/02
    /// 
    /// Interface for methods accessing service review data.
    /// </summary>
    
    public interface IServiceReviewAccessor
    {
        List<ServiceReview> SelectAllServiceReviews();
        int InsertServiceReview(ServiceReview serviceReview);
        ServiceReview SelectServiceReviewsByServiceReviewID(int id);
        int UpdateServiceReviewFromClient(ServiceReview oldServiceReview, ServiceReview newServiceReview);
        int DeleteServiceReview(ServiceReview serviceReview);

    }
}
