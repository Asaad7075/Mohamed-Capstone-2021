using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Donation
    {
        public int DonationID { get; set; } //  Primary Key
        public int DonorID { get; set; } // Foreign Key from Donor table
        [Required(ErrorMessage = "Name Of Item is  requied")]
        public string NameOfItem { get; set; } // the name of the item
        [Required(ErrorMessage = "Discription is requied")]
        public string Description { get; set; } // the description of the item
        [Required(ErrorMessage = "Price is required")]
        public decimal EstValue { get; set; } // the estmate value 
        [Required(ErrorMessage = "You must enter Age of Item")]
        public int AgeofItem { get; set; } // the age of the item
        public bool DropOff { get; set; } // mark it as  drop off
        public bool PickUp { get; set; } // mark it as  pick up
        [Required(ErrorMessage = "You must select a date")]
        [DataType(DataType.Date)]
        public DateTime PickUpDateTime { get; set; } //  The day that  pick up  
        public bool MailReceipt { get; set; } // mark as a mail receipt
        public bool EmailReceipt { get; set; } //mark it as an email receipt
        public string DonationStatus { get; set; }  // the status of the donation if approve , deny , pending, or submitted.
        public byte[] DonatedImage { get; set; } // this is the images of items
        [Required(ErrorMessage = "You must enter a quantity")]
        public int OrderQty { get; set; }

        public Donation()
        {

        }

        public Donation
            (
            int donationID, int donorID, string nameOfItem,
            string description, decimal estValue, int ageofItem, bool dropOff,
            bool pickUp, DateTime pickUpDateTime, bool mailReceipt,
            bool emailReceipt, string donationStatus//, byte[] donatedImage
            )
        {
            DonationID = donationID;
            DonorID = donorID;
            NameOfItem = nameOfItem;
            Description = description;
            EstValue = estValue;
            AgeofItem = ageofItem;
            DropOff = dropOff;
            PickUp = pickUp;
            PickUpDateTime = pickUpDateTime;
            MailReceipt = mailReceipt;
            EmailReceipt = emailReceipt;
            DonationStatus = donationStatus;
            //DonatedImage = donatedImage;
        }
    }
}
