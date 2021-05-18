using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using DomainModels.Tickets;
using LogicInterfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicTests
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/03/26
    ///
    /// Unit Tests for Route
    /// <summary>
    [TestClass]
    public class RouteTests
    {
        private IRouteAccessor _routeAccessor;
        private IDeliveryTicketAccessor _deliveryTicketAccessor;
        private IPickUpTicketAccessor _pickupTicketAccessor;
        private IRideTicketAccessor _rideTicketAccessor;
        private List<RouteVM> _routes;

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Constructor sets up our accessors for data access dependency.
        /// <summary>
        [TestInitialize]
        public void TestSetup()
        {
            _routeAccessor = new RouteFake();
            _deliveryTicketAccessor = new DeliveryTicketAccessorFake();
            _pickupTicketAccessor = new PickUpTicketAccessorFake();
            _rideTicketAccessor = new RideTicketAccessorFake();
        }


        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Tests the add route and returns good.
        /// </summary>
        [TestMethod]
        public void TestAddRoute_RouteAdded_ReturnsGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;

            RouteVM route = new RouteVM
            {
                RouteID = 100004,
                DateOfRoute = new DateTime(2020, 02, 05),
                DriverEmployeeID = 185216,
                Active = true,
                LicensePlateNumber = "L-527F"
            };
            // Act
            actualResult = _routeAccessor.InsertRoute(route);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Tests the add route and returns bad.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddRoute_RouteNotAdded_ReturnsBad()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;

            RouteVM route = new RouteVM
            {
                RouteID = 100000,
                DateOfRoute = new DateTime(2020, 02, 05),
                DriverEmployeeID = 185215,
                Active = true,
                LicensePlateNumber = "L-527N"
            };
            // Act
            actualResult = _routeAccessor.InsertRoute(route);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Tests the delete route and returns good.
        /// </summary>
        [TestMethod]
        public void TestDeleteRoute_RouteFoundAndDeleted_ReturnsGood()
        {
            // Arrange
            int expectedResult = 1;
            int actualResult;
            List<RouteVM> routes = _routeAccessor.SelectAllRoutes();

            // Act
            actualResult = _routeAccessor.DeleteRoute(routes[0]);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Tests the delete route and returns bad.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestDeleteRoute_RouteNotFound_ReturnsBad()
        {
            // Arrange
            int actualResult;
            List<RouteVM> routes = _routeAccessor.SelectAllRoutes();
            RouteVM route = routes[0];
            // Act & Assert
            for (int i = 0; i < 2; i++)
            {
                actualResult = _routeAccessor.DeleteRoute(route);
            }
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Tests the routes by date and returns routes by date list.
        /// </summary>
        [TestMethod]
        public void TestRetrieveRoutesByDate_ReturnsRoutesByDateList()
        {
            int expectedResult = 1;
            int actualResult = 0;

            DateTime date = new DateTime(2020, 02, 05);

            List<RouteVM> data = _routeAccessor.SelectRoutesByDate(date);
            int total = data.Count;

            foreach (Route item in data)
            {
                if (item.DateOfRoute == date)
                {
                    actualResult++;
                }
            }
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Tests the routes by driver id and date and returns routes by driver id and date list.
        /// </summary>
        [TestMethod]
        public void TestRetrieveRoutesByDriverIDAndDate_ReturnsRoutesByDriverIDList()
        {
            int expectedResult = 1;
            int actualResult = 0;

            DateTime date = new DateTime(2020, 02, 05);
            int driverID = 185215;

            List<RouteVM> data = _routeAccessor.SelectRoutesByDriverID(driverID);

            int total = data.Count;

            foreach (RouteVM item in data)
            {
                if (item.DriverEmployeeID == driverID && item.DateOfRoute == date)
                {
                    actualResult++;
                }
            }
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Tests the select all routes and returns good.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllRoutes_SelectedAllRoutes_ReturnsGood()
        {
            // arrange
            RouteManager routeManager =
                new RouteManager(_routeAccessor);
            const int expectedDataObjectCount = 4;
            int actualDataObjectCount;

            // act
            _routes = _routeAccessor.SelectAllRoutes();
            actualDataObjectCount = _routes.Count;

            // assert
            Assert.AreEqual(expectedDataObjectCount, actualDataObjectCount);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/08
        ///
        /// Tests the edit route and returns good.
        /// </summary>
        [TestMethod]
        public void TestEditRoute_ReturnsGood()
        {
            RouteVM oldRoute = new RouteVM()
            {
                RouteID = 100000,
                DateOfRoute = new DateTime(2020, 02, 05),
                DriverEmployeeID = 185215,
                Active = true,
                LicensePlateNumber = "L-527N"
            };
            RouteVM newRoute = new RouteVM()
            {
                RouteID = 100000,
                DateOfRoute = new DateTime(2020, 02, 07),
                DriverEmployeeID = 185215,
                Active = true,
                LicensePlateNumber = "L-527N"
            };

            int actualResult = 0;
            int expectedResult = 1;

            // act
            actualResult = _routeAccessor.UpdateRoute(oldRoute, newRoute);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/08
        ///
        /// Tests the edit route and returns bad.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestEditRoute_ReturnsBad()
        {
            RouteVM oldRoute = new RouteVM()
            {
                RouteID = 100000,
                DateOfRoute = new DateTime(2020, 02, 05),
                DriverEmployeeID = 185215,
                Active = true,
                LicensePlateNumber = "L-527N"
            };
            RouteVM newRoute = new RouteVM()
            {
                RouteID = 100004,
                DateOfRoute = new DateTime(2020, 02, 05),
                DriverEmployeeID = 185215,
                Active = true,
                LicensePlateNumber = "L-527N"
            };

            int actualResult = 0;
            int expectedResult = 1;


            actualResult = _routeAccessor.UpdateRoute(oldRoute, newRoute);

            if(actualResult != 1)
            {
                throw new Exception();
            } else
            {
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// </summary>
        [TestMethod]
        public void TestAddRouteReturnsRouteVM()
        {
            //arrange
            IRouteManager routeManager = new RouteManager(_routeAccessor, _rideTicketAccessor, _deliveryTicketAccessor, _pickupTicketAccessor);
            DateTime dateTime = DateTime.Now;
            string licensePlateNumber = "P1ZZA";
            int driverID = 100000;
            int routeID = 100004;

            var expected = new RouteVM()
            {
                RouteID = routeID,
                DateOfRoute = dateTime,
                LicensePlateNumber = licensePlateNumber,
                DriverEmployeeID = driverID
            };

            //act
            RouteVM actual = routeManager.AddRoute(dateTime, licensePlateNumber, driverID);

            //assert
            Assert.AreEqual(expected.RouteID, actual.RouteID);
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// </summary>
        [TestMethod]
        public void TestGetTicketMetaDataReturnsMetaData()
        {
            //arrange
            IRouteManager routeManager = new RouteManager(_routeAccessor, _rideTicketAccessor, _deliveryTicketAccessor, _pickupTicketAccessor);
            TicketMetaData expected = new TicketMetaData()
            {
                TotalUnassigned = 15,
                DeliveryUnassigned = 5,
                PickupUnassigned = 5,
                RideUnassigned = 5
            };
            //act
            var actual = routeManager.GetTicketMetaData();

            //assert
            Assert.AreEqual(expected.TotalUnassigned, actual.DeliveryUnassigned + actual.PickupUnassigned + actual.RideUnassigned);
        }
    }
}
