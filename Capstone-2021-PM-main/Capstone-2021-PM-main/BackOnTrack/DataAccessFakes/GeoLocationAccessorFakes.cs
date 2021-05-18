using BingMapsRESTToolkit;
using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/03/01
    /// 
    /// Fakes for geolocation manager unit tests
    /// </summary>
    public class GeoLocationAccessorFakes : IGeoLocationAccessor
    {
        private List<GeoLocation> _geoLocations = new List<GeoLocation>();
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/01
        /// 
        /// Constructor that intializes a list
        /// </summary>
        public GeoLocationAccessorFakes()
        {
            _geoLocations.Add(new GeoLocation()
            {
                GeoID = 10000,
                StreetAddressLineOne = "123 Raindbow Ave",
                StreetAddressLineTwo = "",
                ZipCode = "55522",
                Coordinate = new Coordinate(50.000, 45.0000)
            });
            _geoLocations.Add(new GeoLocation()
            {
                GeoID = 10001,
                StreetAddressLineOne = "10 Main Ave",
                StreetAddressLineTwo = "",
                ZipCode = "55522",
                Coordinate = new Coordinate(50.000, 45.0000)
            });
            _geoLocations.Add(new GeoLocation()
            {
                GeoID = 10002,
                StreetAddressLineOne = "23 Dog St",
                StreetAddressLineTwo = "",
                ZipCode = "55521",
                Coordinate = new Coordinate(50.000, 45.0000)
            });
            _geoLocations.Add(new GeoLocation()
            {
                GeoID = 10003,
                StreetAddressLineOne = "45 Cat Rd",
                StreetAddressLineTwo = "",
                ZipCode = "55521",
                Coordinate = new Coordinate(50.000, 45.0000)
            });
            _geoLocations.Add(new GeoLocation()
            {
                GeoID = 10004,
                StreetAddressLineOne = "20 Backwater Rd",
                StreetAddressLineTwo = "",
                ZipCode = "55522",
                Coordinate = new Coordinate(50.000, 45.0000)
            });
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/01
        /// </summary>
        /// <param name="streetAddressLineOne"></param>
        /// <param name="streetAddressLineTwo"></param>
        /// <param name="zipcode"></param>
        /// <returns></returns>
        public int InsertGeoLocation(string streetAddressLineOne, string streetAddressLineTwo, string zipcode, Coordinate coordinate)
        {
            int result = 0;
            _geoLocations.Add(new GeoLocation() {
                GeoID = _geoLocations.Count + 10000,
                Coordinate = coordinate,
                StreetAddressLineOne = streetAddressLineOne,
                StreetAddressLineTwo = streetAddressLineTwo,
                ZipCode = zipcode
            });
            result = (int)_geoLocations.Last().GeoID;

            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/01
        /// </summary>
        /// <param name="streetAddressLineOne"></param>
        /// <param name="streetAddressLineTwo"></param>
        /// <param name="zipcode"></param>
        /// <returns></returns>
        public GeoLocation SelectGeoLocationByAddress(string streetAddressLineOne, string streetAddressLineTwo, string zipcode)
        {
            GeoLocation geoLocation = null;
            geoLocation = _geoLocations.Find(g => g.StreetAddressLineOne.CompareTo(streetAddressLineOne) == 0
                                               && g.StreetAddressLineTwo.CompareTo(streetAddressLineTwo) == 0
                                               && g.ZipCode.CompareTo(zipcode) == 0);
            if(geoLocation == null)
            {
                geoLocation = new GeoLocation()
                {
                    StreetAddressLineOne = streetAddressLineOne,
                    StreetAddressLineTwo = streetAddressLineTwo,
                    ZipCode = zipcode,
                    Coordinate = new Coordinate(50.000, 45.0000)
                };
            }
            return geoLocation;
        }
    }
}
