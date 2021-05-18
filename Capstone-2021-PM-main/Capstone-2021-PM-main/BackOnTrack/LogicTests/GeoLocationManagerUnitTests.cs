/// Jakub Kawski
/// Created: 2021/02/13
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainModels;
using DataAccessInterfaces;
using DataAccessFakes;
using LogicInterfaces;
using LogicLayer;
using System.Device.Location;
using BingMapsRESTToolkit;

namespace LogicTests
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/03/01
    /// </summary>
    [TestClass]
    public class GeoLocationManagerUnitTests
    {
        private List<GeoLocation> _geoLocations;
        private IGeoLocationAccessor _geoLocationAccessor;

        [TestInitialize]
        public void TestSetup()
        {
            _geoLocationAccessor = new GeoLocationAccessorFakes();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/01
        /// 
        /// Tests that RetrieveGeoLocation returns a matched geolocation if
        /// there is one that matches in the database
        /// </summary>
        [TestMethod]
        public void TestGeoLocationManagerReturnsMatchedGeoLocation()
        {
            //arrange
            IGeoLocationManager geoLocationManager = new GeoLocationManager(_geoLocationAccessor);
            GeoLocation expectedMatch = new GeoLocation()
            {
                GeoID = 10002,
                StreetAddressLineOne = "23 Dog St",
                StreetAddressLineTwo = "",
                ZipCode = "55521",
                Coordinate = new Coordinate(50.000, 45.0000)
            };
            GeoLocation actualMatch;

            //act
            actualMatch = geoLocationManager.RetrieveGeoLocation("23 Dog St", "", "55521");
            //assert
            Assert.AreEqual(expectedMatch.GeoID, actualMatch.GeoID);
           
        }
        [TestMethod]
        public void TestGeoLocationManagerReturnsNewGeoLocation()
        {
            //arrange
            IGeoLocationManager geoLocationManager = new GeoLocationManager(_geoLocationAccessor);
            GeoLocation expectedMatch = new GeoLocation()
            {
                GeoID = 10005,
                StreetAddressLineOne = "556 Hop Dr",
                StreetAddressLineTwo = "",
                ZipCode = "55521",
                Coordinate = new Coordinate(50.000, 45.0000)
            };
            GeoLocation actualMatch;

            //act
            actualMatch = geoLocationManager.RetrieveGeoLocation("556 Hop Dr","", "55521");
            //assert
            Assert.AreEqual(expectedMatch.GeoID, actualMatch.GeoID);
        }
    }
}
