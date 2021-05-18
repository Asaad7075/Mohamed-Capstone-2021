/// Jakub Kawski
/// Created: 2021/02/13
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class GeoLocation
    {
        public int GeoID { get; set; }
        public Coordinate Coordinate { get; set; }
        public string StreetAddressLineOne { get; set; }
        public string StreetAddressLineTwo { get; set; }
        public string ZipCode { get; set; }

    }
}
