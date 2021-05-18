using DomainModels;
using DomainModels.Tickets;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/07
    /// 
    /// Delivery ticket controller.
    /// </summary>
    [Authorize]
    public class DeliveryTicketController : Controller
    {
        private IDeliveryTicketManager _deliveryTicketManager = new DeliveryTicketManager();
        private IClientManager _clientManager = new ClientManager();
        private IDonationManager _donationManager = new DonationManager();
        private IOrderManager _orderManager = new OrderManager();

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/08
        /// 
        /// Displays delivery tickets available to 
        /// the client that have been delivered or
        /// are still in progress.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/04/20
        ///
        /// Removed image display to show
        /// updated reflection of multiple ordered items
        /// in a single delivery ticket.
        /// </remarks>
        [Authorize(Roles = "Client, Admin")]
        // GET: DeliveryTicket
        public ActionResult Index()
        {
            try
            {
                string email = User.Identity.Name;
                //dynamic deliveryTicketModel = new ExpandoObject();


                // Get client id by email
                int clientId = _clientManager.GetClientIDByEmail(email.ToLower());
                if (clientId >= 10000)
                {
                    IEnumerable<DeliveryTicketVM> deliveryTickets = _deliveryTicketManager.RetrieveTicketsByClientId(clientId);
                    // Get Order associations to display images
                    //deliveryTicketModel.Tickets = deliveryTickets;
                    //// Dictionary for delivery ticket and image pairings
                    //Dictionary<int, byte[]> orderIdImagepairings = new Dictionary<int, byte[]>();
                    //if (deliveryTickets.Count() != 0)
                    //{

                    //    // Get donation image
                    //    foreach (var ticket in deliveryTickets)
                    //    {
                    //        int donationID = _orderManager.RetrieveDonationIDByOrderID(ticket.OrderID);
                    //        if (donationID != -1) // If a donation exists for the ticket try to get the image
                    //        {
                    //            orderIdImagepairings.Add(ticket.OrderID, _donationManager.RetrieveDonationImageByDonationID(donationID));
                    //        }
                    //    }
                    //}

                    //deliveryTicketModel.ImagesRepo = orderIdImagepairings;

                    // Display Delivery Tickets
                    return View(deliveryTickets);
                    
                }
                
            }
            catch (Exception)
            {
                return RedirectToAction("LegoError404");
            }

            return RedirectToAction("LegoError404");
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Custom user-facing 404-error page.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Client, Admin")]
        public ActionResult LegoError404()
        {
            return View();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/13
        /// 
        /// Shows details of individual donated
        /// item ordered by a client.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Client, Admin")]
        // GET: DeliveryTicket/Details/5
        public ActionResult Details(int? orderId)
        {
            try
            {
                if (orderId != null)
                {
                    int orderID = (int)orderId;
                    string email = User.Identity.Name;
                    int clientId = _clientManager.GetClientIDByEmail(email.ToLower());
                    // Verify user 
                    bool hasTicket = _orderManager.AuthorizeTicketRetrievalByClientID(orderID, clientId);
                    if (hasTicket)
                    {
                        dynamic deliveryTicketModel = new ExpandoObject();
                        // Get specified delivery ticket
                        DeliveryTicketVM ticket = _deliveryTicketManager.RetrieveTicketByOrderID(orderID);
                        deliveryTicketModel.Ticket = ticket;

                        // Get order information
                        Order order = _orderManager.RetrieveOrderByOrderID(ticket.OrderID);

                        // Get ordered items information
                        for (int i = 0; i < order.Items.Count; i++)
                        {
                            int orderqty = order.Items[order.Items.ElementAt(i).Key].OrderQty; // Retaining a value that would get lost
                            order.Items[order.Items.ElementAt(i).Key] = _donationManager.RetrieveDonationByDonationID(order.Items.ElementAt(i).Key);
                            order.Items[order.Items.ElementAt(i).Key].OrderQty = orderqty;
                        }

                        deliveryTicketModel.Order = order;

                        return View(deliveryTicketModel);
                    }
                    else
                    {
                        return RedirectToAction("LegoError404");
                    }
                }
                else
                {
                    return RedirectToAction("LegoError404");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("LegoError404");
            }
        }

        //// GET: DeliveryTicket/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DeliveryTicket/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DeliveryTicket/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: DeliveryTicket/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DeliveryTicket/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: DeliveryTicket/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
