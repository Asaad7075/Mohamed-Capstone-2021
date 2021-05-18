using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/02
    /// 
    /// POCO for deserializing JSON data after API call
    /// to NHTSA for latest information on any latest 
    /// vehicle defect recalls.
    /// </summary>
    public class AutoDefectRecall
    {
        public string Manufacturer { get; set; }
        public string NHTSACampaignNumber { get; set; }
        public string ReportReceivedDate { get; set; }
        public string Component { get; set; }
        public string Summary { get; set; }
        public string Conequence { get; set; } // Government Typo enforced to avoid deserialization error
        public string Remedy { get; set; }
        public string Notes { get; set; }
        public string ModelYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/03
        /// 
        /// Override of the Equals method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as AutoDefectRecall;

            if (other == null)
            {
                return false;
            }

            if ((Manufacturer != other.Manufacturer || NHTSACampaignNumber != other.NHTSACampaignNumber) ||
                (ReportReceivedDate != other.ReportReceivedDate || Component != other.Component) ||
                (Summary != other.Summary || Conequence != other.Conequence) ||
                (Remedy != other.Remedy || Notes != other.Notes) ||
                (ModelYear != other.ModelYear || Make != other.Make) ||
                Model != other.Model)
            {
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/03
    /// 
    /// Associated class used to process 
    /// JSON data and deserialize it.
    /// </summary>
    public class AutoDefectNHTSJsonResult
    {
        public int Count { get; set; }
        public string Message { get; set; }
        [JsonExtensionData]
        public Dictionary<string, object> ExtensionData { get; set; } // Results returned for each car defect, processed as a Dictionary object
    }
}
