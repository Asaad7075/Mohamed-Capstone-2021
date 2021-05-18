using BingMapsRESTToolkit;
using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/26/02
    /// 
    /// Geolocation manager class
    /// </summary>
    public class GeoLocationManager : IGeoLocationManager
    {
        private IGeoLocationAccessor _geoLocationAccessor;
        public GeoLocationManager()
        {
            _geoLocationAccessor = new GeoLocationAccessor();
        }
        public GeoLocationManager(IGeoLocationAccessor geoLocationAccessor)
        {
            _geoLocationAccessor = geoLocationAccessor;
        }
        /// Jakub Kawski 
        /// 2021/26/02
        /// <summary>
        /// This method trys to retrieve an already created geoID, if not found
        /// will create a new geoid/geolocation.
        /// </summary>
        /// <param name="streetAddress"></param>
        /// <param name="zipcode"></param>
        /// <returns></returns>
        public GeoLocation RetrieveGeoLocation(string streetAddressLineOne, string streetAddressLineTwo, string zipcode)
        {
            GeoLocation result = null;
            try
            {
                result = _geoLocationAccessor.SelectGeoLocationByAddress(streetAddressLineOne, streetAddressLineTwo, zipcode);
                if(result.GeoID == 0)
                {
                    // This will need to be updated when we can actually retrieve coordinates
                    Coordinate coordinate = new Coordinate(50.000, 45.0000);
                    int geoid = _geoLocationAccessor.InsertGeoLocation(streetAddressLineOne, streetAddressLineTwo, zipcode, coordinate);

                    result = new GeoLocation()
                    {
                        GeoID = geoid,
                        StreetAddressLineOne = streetAddressLineOne,
                        StreetAddressLineTwo = streetAddressLineTwo,
                        ZipCode = zipcode,
                        Coordinate = coordinate
                    };                  
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
