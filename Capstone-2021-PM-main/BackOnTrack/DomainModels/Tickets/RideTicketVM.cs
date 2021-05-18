using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Tickets
{
    public class RideTicketVM : RideTicket
    {
        [Display(Name = "First Name:")]
        public string ClientFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string ClientLastName { get; set; }
        public Coordinate FetchCoordinate { get; set; }
        [Required]
        [Display(Name = "Street Address:")]
        public string FetchStreetAddressLineOne { get; set; }
        public string FetchStreetAddressLineTwo{ get; set; }
        [Required]
        [Display(Name = "Zip:")]
        public string FetchZipCode { get; set; }
        [Required]
        [Display(Name = "City:")]
        public string FetchCity { get; set; }
        [Required]
        [Display(Name = "State:")]
        public string FetchState { get; set; }
        public Coordinate DestinationCoordinate { get; set; }
        [Display(Name = "Destination Street:")]
        public string DestinationStreetAddressLineOne { get; set; }
        public string DestinationStreetAddressLineTwo { get; set; }
        [Display(Name = "Destination Zip:")]
        public string DestinationZipCode { get; set; }
        [Display(Name = "Destination City:")]
        public string DestinationCity { get; set; }
        [Display(Name = "Destination State:")]
        public string DestinationState { get; set; }
        public string StatusDescription { get; set; }
        public string ClientFullName
        {
            get
            {
                return ClientFirstName + " " + ClientLastName;
            }
        }
        public string FetchFullAddress
        {
            get
            {
                return FetchStreetAddressLineOne + " " + FetchStreetAddressLineTwo + ", " +
                    FetchCity + ", " + FetchState + ", " + FetchZipCode;
            }
        }
        public string DestinationFullAddress
        {
            get
            {
                return DestinationStreetAddressLineOne + " " + DestinationStreetAddressLineTwo + ", " +
                    DestinationCity + ", " + DestinationState + ", " + DestinationZipCode;
            }
        }
        public override void EnableCopy()
        {
            base.EnableCopy();
            if (WasEdited) 
            { 
            OldTicketCopy = new RideTicketVM(this);
        }
        }
        public RideTicketVM()
        {

        }
        public RideTicketVM(RideTicketVM ticket)
        {
            TicketID = ticket.TicketID;
            TicketType = ticket.TicketType;
            StatusID = ticket.StatusID;
            CreatedAt = ticket.CreatedAt;
            Notes = ticket.Notes;
            ClientID = ticket.ClientID;
            FetchGeoID = ticket.FetchGeoID;
            DestinationGeoID = ticket.DestinationGeoID;
            StopNumber = ticket.StopNumber;
            DateOfRide = ticket.DateOfRide;
            EstimatedArrival = ticket.EstimatedArrival;
            RouteID = ticket.RouteID;
            FetchCoordinate = ticket.FetchCoordinate;
            FetchStreetAddressLineOne = ticket.FetchStreetAddressLineOne;
            FetchStreetAddressLineTwo = ticket.FetchStreetAddressLineTwo;
            FetchZipCode = ticket.FetchZipCode;
            FetchCity = ticket.FetchCity;
            FetchState = ticket.FetchState;
            DestinationCoordinate = ticket.DestinationCoordinate;
            DestinationStreetAddressLineOne = ticket.DestinationStreetAddressLineOne;
            DestinationStreetAddressLineTwo = ticket.DestinationStreetAddressLineTwo;
            DestinationZipCode = ticket.DestinationZipCode;
            DestinationCity = ticket.DestinationCity;
            DestinationState = ticket.DestinationState;
            StatusDescription = ticket.StatusDescription;
            ClientFirstName = ticket.ClientFirstName;
            ClientLastName = ticket.ClientLastName;
            TimeRangeStart = ticket.TimeRangeStart;
            TimeRangeEnd = ticket.TimeRangeEnd;
        }
    }
}
