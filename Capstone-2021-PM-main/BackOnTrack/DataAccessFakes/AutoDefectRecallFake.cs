using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataAccessFakes
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/02
    /// 
    /// Fake auto defect recall class.
    /// </summary>
    public class AutoDefectRecallFake : IAutoDefectRecallAccessor
    {
        private string _autoDefectRecallJSON;
        private Task<IEnumerable<AutoDefectRecall>> _autoDefectRecalls;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/03
        /// 
        /// No argument constructor for intializing the data fake.
        /// </summary>
        public AutoDefectRecallFake()
        {
            _autoDefectRecallJSON = getAutoDefectRecallString();
            _autoDefectRecalls = deserializeAutoDefectRecall();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/03
        /// 
        /// Helper method to deserialize the Json data
        /// and convert it to an auto defect recall object.
        /// Deserialization research done via Microsoft's API.
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<AutoDefectRecall>> deserializeAutoDefectRecall()
        {
            List<AutoDefectRecall> autoDefectRecalls = new List<AutoDefectRecall>();

            try
            {
                List<AutoDefectNHTSJsonResult> test = new List<AutoDefectNHTSJsonResult>();
                test.Add(JsonSerializer.Deserialize<AutoDefectNHTSJsonResult>(_autoDefectRecallJSON));
                foreach (var item in test)
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
                            autoDefectRecalls.Add(currADR); // Add instance to the list
                        }

                    }
                }
                return autoDefectRecalls;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error when retrieving recall status data." + "\n\n" + ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/03
        /// 
        /// Private helper Json-formatted string
        /// used to test the JsonSerializer helper methods.
        /// </summary>
        /// <returns></returns>
        private string getAutoDefectRecallString()
        {
            return @"{""Count"":1,""Message"":""Results returned successfully"",""Results"":[{""Manufacturer"":""BMW OF NORTH AMERICA, LLC"",""NHTSACampaignNumber"":""12V176000"",""ReportReceivedDate"":""\/Date(1334894400000-0400)\/"",""Component"":""SEATS:FRONT ASSEMBLY:HEAD RESTRAINT"",""Summary"":""BMW IS RECALLING CERTAIN MODEL YEAR 2012 BMW 3-SERIES VEHICLES MANUFACTURED FROM OCTOBER 19, 2011, THROUGH MARCH 18, 2012, THAT HAVE FRONT SEAT HEAD RESTRAINTS THAT EXCEED THE DOWNWARD MOVEMENT LIMIT OF 25MM IN THE HIGHEST POSITION.THUS, THESE VEHICLES FAIL TO COMPLY WITH THE REQUIREMENTS OF FEDERAL MOTOR VEHICLE SAFETY STANDARD NO. 202A,\""HEAD RESTRAINTS.\""   "",""Conequence"":""IN THE EVENT OF A VEHICLE CRASH, THE HEAD RESTRAINT MAY UNEXPECTEDLY MOVE DOWN SLIGHTLY IF IT WAS ADJUSTED TO THE FULLY EXTENDED POSITION, INCREASING THE RISK OF PERSONAL INJURY. "",""Remedy"":""BMW WILL NOTIFY OWNERS, AND DEALERS WILL ATTACH A CLAMP TO THE FRONT SEAT HEAD RESTRAINT POSTS, FREE OF CHARGE.  THE SAFETY RECALL BEGAN ON MAY 30, 2012.  OWNERS MAY CONTACT BMW CUSTOMER RELATIONS AND SERVICES AT 1-800-525-7417.  "",""Notes"":""CUSTOMERS MAY CONTACT THE NATIONAL HIGHWAY TRAFFIC SAFETY ADMINISTRATION'S VEHICLE SAFETY HOTLINE AT 1-888-327-4236 (TTY: 1-800-424-9153); OR GO TO HTTP://WWW.SAFERCAR.GOV."",""ModelYear"":""2012"",""Make"":""BMW"",""Model"":""3-SERIES""}]}";
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/03
        /// 
        /// Method that returns all auto defects.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<AutoDefectRecall>> RetrieveAutoDefectRecallAsync(List<Vehicle> vehicles)
        {
            // Making a live API call is impractical because it will not necessarily always return
            // what is expected, so a static JSON string is used to test
            return _autoDefectRecalls;
        }
    }
}
