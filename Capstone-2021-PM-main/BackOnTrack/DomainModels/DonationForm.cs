using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Asaad Mohamed
    /// Created: 2021/02/04
    /// 
    /// Donor Form data storage model class for Back On Track
    ///  This information for the donor form which is aprove or deny by inventory auditor
    /// </summary>
   public class DonationForm
    {
        public int DonorFormID { get; set; } //  Primary Key
        public int DonorID { get; set; } // Foreign Key from Donor table
        public DateTime DateCreated { get; set; } //  The day that the form created 
        public string Status { get; set; } // the status of the form if approve , deny , pending, or submitted.

        public override string ToString() // this override the status
        {
            return Status;
        }
    }
}
