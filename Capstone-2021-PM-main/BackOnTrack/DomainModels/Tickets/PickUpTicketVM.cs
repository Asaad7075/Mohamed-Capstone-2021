using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Tickets
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/12/03
    /// 
    /// Domain ViewModel for PickupTickets
    /// </summary>
    public class PickUpTicketVM : PickUpTicket
    {
        public Dictionary<string, int> Items { get; set; }
        public Coordinate Coordinate { get; set; }
        public string StreetAddressLineOne { get; set; }
        public string StreetAddressLineTwo { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StatusDescription { get; set; }
        public override void EnableCopy()
        {
            base.EnableCopy();
            if (WasEdited)
            {
                OldTicketCopy = new PickUpTicketVM(this);
            }           
        }
        public string FullAddress { 
            get 
            {
                return StreetAddressLineOne + " " + StreetAddressLineTwo + ", " +
                    City + ", " + State + ", " + ZipCode;
            } 
        }
        public PickUpTicketVM()
        {

        }
        public PickUpTicketVM(PickUpTicketVM ticket)
        {
            TicketID = ticket.TicketID;
            TicketType = ticket.TicketType;
            StatusID = ticket.StatusID;
            CreatedAt = ticket.CreatedAt;
            Notes = ticket.Notes;
            DonationID = ticket.DonationID;
            GeoID = ticket.GeoID;
            TimeRangeStart = ticket.TimeRangeStart;
            TimeRangeEnd = ticket.TimeRangeEnd;
            RequestDateStart = ticket.RequestDateStart;
            RequestDateEnd = ticket.RequestDateEnd;
            StopNumber = ticket.StopNumber;
            EstimatedArrival = ticket.EstimatedArrival;
            RouteID = ticket.RouteID;
            Items = ticket.Items;
            Coordinate = ticket.Coordinate;
            StreetAddressLineOne = ticket.StreetAddressLineOne;
            StreetAddressLineTwo = ticket.StreetAddressLineTwo;
            ZipCode = ticket.ZipCode;
            City = ticket.City;
            State = ticket.State;
            StatusDescription = ticket.StatusDescription;
        }
    }
}
