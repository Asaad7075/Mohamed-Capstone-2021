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
using DomainModels.Services;

namespace LogicLayer
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/04/09
    /// 
    /// This logic layer class handles the processing of the service reviews.
    /// </summary>
    public class ServiceReviewManager : IServiceReviewManager
    {
        private IServiceReviewAccessor _serviceReviewAccessor;
        private List<ServiceReview> _serviceReviews;
        //private List<ServiceReview> _serviceReviews;

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Default constructor for service review manager.
        /// </summary>
        public ServiceReviewManager()
        {
            _serviceReviewAccessor = new ServiceReviewAccessor();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        //  Service review manager taking the IServiceReviewAccessor interface as a parameter to access the service review accessor
        /// interface at runtime
        /// </summary>
        /// <param name="serviceReviewAccessor"></param>
        public ServiceReviewManager(IServiceReviewAccessor serviceReviewAccessor)
        {
            _serviceReviewAccessor = serviceReviewAccessor;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// AddServiceReviewFromUser will allow a user to Add a review on client
        /// </summary>
        public bool AddServiceReviewFromUser(ServiceReview serviceReview)
        {
            bool result = false;
            try
            {
                bool serviceReviewAdded = (1 == _serviceReviewAccessor.InsertServiceReview(serviceReview));
                if (serviceReviewAdded.Equals(0))
                {
                    throw new ApplicationException("Service Review could not be added to the DB at this time.");
                }
                else
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Service Review failed \n\n", ex);
            }
            return result;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// DeleteServiceReview will allow a user to delete a review on client
        /// </summary>
        public bool DeleteServiceReview(ServiceReview serviceReview)
        {
            bool result = false;
            try
            {
                result = (0 != _serviceReviewAccessor.DeleteServiceReview(serviceReview));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        /// <summary>
        /// Chase Martin
        /// Created 2021/04/16
        /// 
        /// EditRideReviewFromClient will take a selected service review as the oldServiceReview and allow
        /// a user to enter new updated information into it as newServiceReview. the newServiceReview will 
        /// overwrite the oldServiceReview and take its place.
        /// </summary>
        /// <param name="oldServiceReview"></param>
        /// <param name="newServiceReview"></param>
        /// <returns></returns>
        public bool EditServiceReviewFromClient(ServiceReview oldServiceReview, ServiceReview newServiceReview)
        {
                bool result = false;
                try
                {
                    if (oldServiceReview != null && newServiceReview != null)
                    {
                        result = (1 == _serviceReviewAccessor.UpdateServiceReviewFromClient(oldServiceReview, newServiceReview));
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to edit the selected service review.\n\n" + ex.Message + ex.InnerException);
                }
                return result;
        }
        

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// RerieveAllServiceReviewsFromUser will retive all the reviews from users.
        /// </summary>
        public List<ServiceReview> RetrieveAllServiceReviews()
        {
            List<ServiceReview> serviceReviews = new List<ServiceReview>();
            try
            {
                serviceReviews = _serviceReviewAccessor.SelectAllServiceReviews();
                if (serviceReviews == null)
                {
                    throw new ApplicationException("Select all service reviews returns null.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Service Review Data is Unavailable. \n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return serviceReviews;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// Gets service reviews for the user that is actively logged in. This method is passed to the
        /// serviceID and returns a list of the service that user had.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public ServiceReview RetrieveAllServiceReviewsById(int id)
        {
            try
            {
                return _serviceReviewAccessor.SelectServiceReviewsByServiceReviewID(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
