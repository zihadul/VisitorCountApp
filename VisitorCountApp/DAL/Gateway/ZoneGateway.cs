using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using VisitorCountApp.DAL.Model;
using System.Data.SqlClient;

namespace VisitorCountApp.DAL.Gateway
{
    class ZoneGateway
    {
        String connectionString = ConfigurationManager.ConnectionStrings["connectionDb"].ConnectionString;

        public string SaveZone(Zone aZone) {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO ZoneTBL VALUES('"+aZone.ZoneType+"','0')";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return aZone.ZoneType + " Zone Saved Successful!";
        }
        public decimal totalNumberOfVisitors;
        public List<Zone> GetZoneTypeList() {
            totalNumberOfVisitors = 0;
            List<Zone> aZoneList = new List<Zone>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM ZoneTBL";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                Zone aZone = new Zone();
                aZone.Id = int.Parse(reader["Id"].ToString());
                aZone.ZoneType = reader["ZoneType"].ToString();
                aZone.Total = decimal.Parse(reader["Total"].ToString());
                totalNumberOfVisitors += aZone.Total;
                aZoneList.Add(aZone);
            }
            reader.Close();
            connection.Close();
            return aZoneList;
        }
        public void UpDateTotalZoneVisitor(int zoneId)
        {
            decimal total=0; ;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM ZoneTBL WHERE Id='"+zoneId+"' ";
            SqlCommand command = new SqlCommand(query,connection);
            connection.Open();
             SqlDataReader reader = command.ExecuteReader();
             while (reader.Read())
             {
                 total = decimal.Parse(reader["Total"].ToString());
             }
             total = total + 1;
             reader.Close();
             query = "UPDATE ZoneTBL SET Total='"+total+"' WHERE Id='" + zoneId + "' ";
             command = new SqlCommand(query,connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public decimal GetTotalVisitors(int id)
        {
            decimal totalVisitors = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM ZoneTBL WHERE Id='" + id + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                totalVisitors = decimal.Parse(reader["Total"].ToString());
            }
            connection.Close();
            reader.Close();
            return totalVisitors;
        }
        public bool IsZoneTypeExists(string zoneType)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM ZoneTBL WHERE ZoneType='" + zoneType + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                connection.Close();
                return true;
            }
            reader.Close();
            connection.Close();
            return false;

        }
    }
}
