using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgTekEksamenRest.model;

namespace ProgTekEksamenRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeassurementsController : ControllerBase
    {
        private static readonly string ConnectionString = Controllers.Connectionstring.GetConnectionString();
        private static Meassurement ReadMeassurements(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            double pressure = reader.GetDouble(1);
            double humidity = reader.GetDouble(2);
            double temperature = reader.GetDouble(3);
            DateTime timestamp = reader.GetDateTime(4);
            Meassurement meassurement = new Meassurement(id, pressure, humidity, temperature, timestamp)
            {
                Id = id,
                Pressure = pressure,
                Humidity = humidity,
                Temperature = temperature,
                TimeStamp = timestamp
            };
            return meassurement;
        }

        // GET: api/Meassurements
        [HttpGet]
        public IEnumerable<Meassurement> GetAllMeassurements()
        {
            const string selectString = "select * from meassurement order by id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Meassurement> meassurementsList = new List<Meassurement>();
                        while (reader.Read())
                        {
                            Meassurement meassurement = ReadMeassurements(reader);
                            meassurementsList.Add(meassurement);
                        }
                        return meassurementsList;
                    }
                }
            }
        }

        // GET: api/Meassurements/5
        [Route("{id}")]
        public Meassurement GetMeassurementById(int id)
        {
            const string selectString = "select * from meassurement where id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }
                        reader.Read(); // advance cursor to first row
                        return ReadMeassurements(reader);
                    }
                }
            }
        }

        // POST: api/Meassurements
        [HttpPost]
        public int AddMeassurement([FromBody] Meassurement value)
        {
            const string insertString = "insert into meassurement (pressure, humidity, temperature) values (@pressure, @humidity, @temperature)";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertString, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@pressure", value.Pressure);
                    insertCommand.Parameters.AddWithValue("@humidity", value.Humidity);
                    insertCommand.Parameters.AddWithValue("@temperature", value.Temperature);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        // PUT: api/Meassurements/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int DeleteMeassurement(int id)
        {
            const string deleteStatement = "delete from meassurement where id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(deleteStatement, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
