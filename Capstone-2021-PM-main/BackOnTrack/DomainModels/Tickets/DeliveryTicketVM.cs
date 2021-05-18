/// Jakub Kawski
/// Created: 2021/02/13
/// 
/// Delivery tickets are used for items that clients request 
/// to be deliveried. 
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

namespace DomainModels.Tickets
{
    public class DeliveryTicketVM : DeliveryTicket 
    {
        public Dictionary<string, int> Items { get; set; }
        public Coordinate Coordinate { get; set; }
        public string StreetAddressLineOne { get; set; }
        public string StreetAddressLineTwo { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StatusDescription { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientFullName
        {
            get
            {
                return ClientFirstName + " " + ClientLastName;
            }
        }
        public string FullAddress
        {
            get
            {
                return StreetAddressLineOne + " " + StreetAddressLineTwo + ", " +
                    City + ", " + State + ", " + ZipCode;
            }
        }
        public override void EnableCopy()
        {
            base.EnableCopy();
            if (WasEdited) 
            { 
                OldTicketCopy = new DeliveryTicketVM(this);
            }
        }
        public DeliveryTicketVM()
        {

        }
        public DeliveryTicketVM (DeliveryTicketVM ticket)
        {
            TicketID = ticket.TicketID;
            TicketType = ticket.TicketType;
            StatusID = ticket.StatusID;
            CreatedAt = ticket.CreatedAt;
            Notes = ticket.Notes;
            OrderID = ticket.OrderID;
            GeoID = ticket.GeoID;
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
            ClientFirstName = ticket.ClientFirstName;
            ClientLastName = ticket.ClientLastName;
        }
    }
}
