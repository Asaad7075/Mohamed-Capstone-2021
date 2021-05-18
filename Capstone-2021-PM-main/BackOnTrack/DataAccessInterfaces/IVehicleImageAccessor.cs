using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/12
    /// 
    /// Interface for accessing vehicle images
    /// </summary>
    public interface IVehicleImageAccessor
    {
        BitmapImage SelectImageByVin(string VinNumber);
        bool InsertImageByVin(byte[] byteImage, string vinNumber);
        Byte[] SelectVehicleImageByName(string name);
        bool UpdateImageByVin(byte[] byteImage, string vinNumber);
    }
}
