/// <summary>
/// Jakub Kawski
/// Created: 2021/02/18
/// 
/// GeoLocation Accessor
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using BingMapsRESTToolkit;
using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    
    public class GeoLocationAccessor : IGeoLocationAccessor
    {
        /// <summary>
        /// Jakub Kawski
        /// Created : 2021/02/19
        /// 
        /// Inserts a new GeoLocation into the DB, returns the ID of the
        /// newly created geolocation
        /// Needs updating when geo cooridnate API is implemented
        /// </summary>
        public int InsertGeoLocation(string streetAddressLineOne, string streetAddressLineTwo, string zipcode, Coordinate coordinate)
        {
            int result = 0;   

            if (streetAddressLineTwo == null)
            {
                streetAddressLineTwo = "";
            }
            if(coordinate == null)
            {
               coordinate = new Coordinate(50.000, 45.000);
            }

          
            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Insert_GeoLocation", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StreetAddressLineOne", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@StreetAddressLineTwo", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 50);           
            cmd.Parameters.Add("@Coordinate", SqlDbType.NVarChar, 255);
            cmd.Parameters["@StreetAddressLineOne"].Value = streetAddressLineOne;
            cmd.Parameters["@StreetAddressLineTwo"].Value = streetAddressLineTwo;
            cmd.Parameters["@ZipCode"].Value = zipcode;
            /* 'POINT(-122.35900 47.65129)'*/
            cmd.Parameters["@Coordinate"].Value = "POINT(" + coordinate.Latitude.ToString() + " " + coordinate.Longitude.ToString() + ")";
            
            try
            {
                conn.Open();
                var scalar = cmd.ExecuteScalar();
                result = Int32.Parse(scalar.ToString());
                if (result == 0)
                {
                    throw new ApplicationException("GeoLocationAccessor.InsertGeoLocation Failed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public GeoLocation SelectGeoLocationByAddress(string streetAddressLineOne, string streetAddressLineTwo, string zipcode)
        {
            GeoLocation geoLocation = new GeoLocation();

            if (streetAddressLineTwo == null)
            {
                streetAddressLineTwo = "";
            }
            
            geoLocation.ZipCode = zipcode;
            geoLocation.StreetAddressLineOne = streetAddressLineOne;
            geoLocation.StreetAddressLineTwo = streetAddressLineTwo;
            geoLocation.Coordinate = new Coordinate(50.000, 45.0000);

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_geolocation_by_address", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StreetAddressLineOne", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@StreetAddressLineTwo", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 50);
            cmd.Parameters["@StreetAddressLineOne"].Value = streetAddressLineOne;
            cmd.Parameters["@StreetAddressLineTwo"].Value = streetAddressLineTwo;
            cmd.Parameters["@ZipCode"].Value = zipcode;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read()) {
                        geoLocation.GeoID = reader.GetInt32(0);
                        //geoLocation.Coordinate = reader.GetString(1);                      
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return geoLocation;
        }
    }
}
