using BingMapsRESTToolkit;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/03/01
    /// 
    /// Interface for Geolocation accessor
    /// </summary>
    public interface IGeoLocationAccessor
    {
        int InsertGeoLocation(string streetAddressLineOne, string streetAddressLineTwo, string zipcode, Coordinate coordinate);
        GeoLocation SelectGeoLocationByAddress(string streetAddressLineOne, string streetAddressLineTwo, string zipcode);

    }
}
