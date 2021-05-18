using DataAccessInterfaces;
using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/04/09
    /// 
    /// interface for the ServiceReviewAccessor class
    /// </summary>
    public class ServiceReviewFake : IServiceReviewAccessor
    {
        private List<ServiceReview> _serviceReviews = new List<ServiceReview>();
        private List<ServiceReview> _review = null;
        private List<ServiceReview> _services = new List<ServiceReview>();

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/07
        /// 
        /// Service review fakes constructor to make fake objects to use for service reviews.
        /// </summary>
        public ServiceReviewFake()
        {
            _services.Add(new ServiceReview
            {
                ServiceName = "Haircut",
                ProviderFirstName = "Sydney",
                ProviderLastName = "Hauser",
                Rating = "2",
                ClientComment = "Bad",
                ServiceReviewID = 1003
            });
           _services.Add(new ServiceReview
           {
               ServiceName = "Budget Counseling",
               ProviderFirstName = "Billy",
               ProviderLastName = "Hougse",
               Rating = "4",
               ClientComment = "Good",
               ServiceReviewID = 1004
           });
            _services.Add(new ServiceReview
            {
                ServiceName = "Babysitter",
                ProviderFirstName = "Hunter",
                ProviderLastName = "Hollow",
                Rating = "1",
                ClientComment = "Real bad",
                ServiceReviewID = 1005
            });
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Fake DeleteServiceReview to simulate deleting a service review to a DB.
        /// </summary>
        public int DeleteServiceReview(ServiceReview serviceReview)
        {
            _serviceReviews.Remove(serviceReview);
            return _serviceReviews.Count;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Fake InsertServiceReview to simulate adding a service review to a DB.
        /// </summary>
        public int InsertServiceReview(ServiceReview serviceReview)
        {
            int result = 0;
            if (_serviceReviews.Contains(serviceReview))
            {
                throw new Exception("This review already exists in the database.");
            }
            else
            {
                _serviceReviews.Add((ServiceReview)serviceReview);
                foreach (var currServiceReview in _serviceReviews)
                {
                    if (currServiceReview.Equals(serviceReview))
                    {
                        result = 1;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Fake SelectallServiceReviews to simulate getting all the ServiceReviews from a DB.
        /// </summary>
        public List<ServiceReview> SelectAllServiceReviews()
        {
            return _serviceReviews;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Fake SelectallServiceReviews to simulate getting all the ServiceReviews from a DB.
        /// </summary>
        public ServiceReview SelectServiceReviewsByServiceReviewID(int id)
        {
            return (from s in _review where s.ServiceReviewID == id select s).Single();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/16
        /// fake accessor for updating a service review
        /// </summary>
        /// <param name="oldServiceReview"></param>
        /// <param name="newServiceReview"></param>
        /// <returns></returns>
        public int UpdateServiceReviewFromClient(ServiceReview oldServiceReview, ServiceReview newServiceReview)
            {
                int result = 0;

                try
                {
                    foreach (var r in _serviceReviews)
                    {
                        if (oldServiceReview.ServiceReviewID == newServiceReview.ServiceReviewID)
                        {
                         oldServiceReview.Rating = newServiceReview.Rating;
                         oldServiceReview.ClientComment = newServiceReview.ClientComment;
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

    }
}
