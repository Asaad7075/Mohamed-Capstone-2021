using DataAccessInterfaces;
using DataAccessLayer;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LogicLayer
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/12
    /// 
    /// Image manager class for pulling images from
    /// the database.
    /// </summary>
    public class VehicleImageManager : IVehicleImageManager
    {
        IVehicleImageAccessor _imageAccessor;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Zero-Argument Constructor
        /// </summary>
        public VehicleImageManager()
        {
            _imageAccessor = new VehicleImageAccessor();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/22
        /// 
        /// Inserts new vehicle images based on supplied vin
        /// numbers.
        /// </summary>
        /// <param name="byteImage"></param>
        /// <param name="vinNumber"></param>
        public bool AddVehicleImageByVin(byte[] byteImage, string vinNumber)
        {
            bool savedImageResult = false;

            try
            {
                savedImageResult = _imageAccessor.InsertImageByVin(byteImage, vinNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle images could not be saved.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException.Message));
            }

            return savedImageResult;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/24
        /// 
        /// Accessor for vehicle related
        /// images.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Byte[] RetrieveVehicleImageByName(string name)
        {
            Byte[] result = null;

            try
            {
                result = _imageAccessor.SelectVehicleImageByName(name);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle images could not be retrieved.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException.Message));
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Method that retrieves vehicle
        /// images based on vin numbers.
        /// </summary>
        /// <param name="vinNumber"></param>
        /// <returns></returns>
        public BitmapImage RetrieveVehicleImageByVin(string vinNumber)
        {
            BitmapImage result = new BitmapImage();

            try
            {
                result = _imageAccessor.SelectImageByVin(vinNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle images could not be retrieved.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException.Message));
            }
            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/24
        /// 
        /// Method that updates vehicles based
        /// on vin numbers.
        /// </summary>
        /// <param name="byteImage"></param>
        /// <param name="vinNumber"></param>
        /// <returns></returns>
        public bool UpdateVehicleImageByVin(byte[] byteImage, string vinNumber)
        {
            bool savedImageResult = false;

            try
            {
                savedImageResult = _imageAccessor.UpdateImageByVin(byteImage, vinNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle images could not be updated.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException.Message));
            }

            return savedImageResult;
        }
    }
}
