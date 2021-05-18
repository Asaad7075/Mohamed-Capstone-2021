using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DomainModels
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/12
    /// 
    /// View Model for vehicles at the presentation layer.
    /// </summary>
    public class VehicleVM : Vehicle
    {
        public BitmapImage VehicleImage { get; set; }
    }
}
