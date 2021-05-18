using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/02
    /// 
    /// API Accessor for NHTSA auto
    /// defect data.
    /// </summary>
    public class AutoDefectRecallAccessor : IAutoDefectRecallAccessor
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/02
        /// 
        /// Makes a call to the API and deserializes
        /// the JSON data and converts it to an auto
        /// defect recall object.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AutoDefectRecall>> RetrieveAutoDefectRecallAsync(List<Vehicle> vehicles)
        {
            List<AutoDefectRecall> _autoDefectRecalls = new List<AutoDefectRecall>();

            try
            {

                // Add each recall to the list
                foreach (Vehicle vehicle in vehicles)
                {
                    // Make api calls for each vehicle
                    IEnumerable<AutoDefectRecall> results = await GetAutoDefectRecallAsync(vehicle);

                    if (results != null)
                    {
                        foreach (AutoDefectRecall item in results)
                        {
                            _autoDefectRecalls.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve auto-defect recall data." + ex.Message);
            }

            return _autoDefectRecalls;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/04
        /// 
        /// Private helper method to make the API call.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        private async Task<IEnumerable<AutoDefectRecall>> GetAutoDefectRecallAsync(Vehicle vehicle)
        {
            List<AutoDefectRecall> result = new List<AutoDefectRecall>();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(
                "https://one.nhtsa.gov/webapi/api/Recalls/vehicle/modelyear/" +
                vehicle.VehicleYear + "/make/" + vehicle.VehicleMake + "/model/" +
                vehicle.VehicleModel + "?format=json");

            var rawJsonResult = await response.Content.ReadAsStringAsync();

            result = TransformJsonData(rawJsonResult.ToString());

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/04
        /// 
        /// Private helper method to transform retrieved
        /// API data.
        /// Deserialization research done via Microsoft's API.
        /// </summary>
        /// <param name="rawJsonResult"></param>
        /// <returns></returns>
        private List<AutoDefectRecall> TransformJsonData(string rawJsonResult)
        {
            List<AutoDefectRecall> result = new List<AutoDefectRecall>();

            List<AutoDefectNHTSJsonResult> jsonResults = new List<AutoDefectNHTSJsonResult>();
            jsonResults.Add(JsonSerializer.Deserialize<AutoDefectNHTSJsonResult>(rawJsonResult.ToString()));
            foreach (var item in jsonResults)
            {
                if (item.Count >= 1) // If at least one result was returned iterate through them
                {
                    Dictionary<string, object> tempDict = item.ExtensionData;
                    string autoDefectResultsReturned = item.ExtensionData["Results"].ToString(); // The results array contains all the defects returned
                    using JsonDocument doc = JsonDocument.Parse(autoDefectResultsReturned);
                    JsonElement root = doc.RootElement;
                    var autoRecalls = root.EnumerateArray();

                    while (autoRecalls.MoveNext()) // Moving through each auto recall 
                    {
                        var currAutoRecall = autoRecalls.Current;
                        // var properties = currVal.EnumerateObject(); // Tester code

                        // Deserialize data and save it to the current domain model
                        AutoDefectRecall currADR = JsonSerializer.Deserialize<AutoDefectRecall>(currAutoRecall.ToString());
                        result.Add(currADR); // Add instance to the list
                    }

                }

            }

            return result;
        }
    }
}
