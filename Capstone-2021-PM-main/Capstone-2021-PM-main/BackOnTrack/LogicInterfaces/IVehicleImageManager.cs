using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LogicInterfaces
{
    public interface IVehicleImageManager
    {
        BitmapImage RetrieveVehicleImageByVin(string VinNumber);
        bool AddVehicleImageByVin(byte[] byteImage, string vinNumber);
        Byte[] RetrieveVehicleImageByName(string v);
        bool UpdateVehicleImageByVin(byte[] byteImage, string vinNumber);
    }
}
