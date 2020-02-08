using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using Appointment.Data.Sql.Response;
using Appointment.Data.Sql.Model;

namespace Appointment.DataContracts.Commands
{
    public class GetOutstandingAppointmentRequestDataCommand :  IGetOutstandingAppointmentRequestDataCommand
    {
        private string _connectionString;
        private string _storedproc = "dbo.GetOutstandingAppointmentRequest";

        public GetOutstandingAppointmentRequestDataCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Response.IGetOutstandingAppointmentRequestsCommandResponse Execute()
        {
            GetOutstandingAppointmentRequestsCommandResponse result = new GetOutstandingAppointmentRequestsCommandResponse();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    var dataResult = conn.Query<AppointmentRequest>(
                        _storedproc,
                        commandType: System.Data.CommandType.StoredProcedure);

                    if (dataResult != null)
                    {
                        foreach(var item in dataResult.ToList())
                        {
                            result.OutstandingAppointmentRequests.Add(item);
                        }

                        result.Success = true;
                    }
                }
            }
            catch (SqlException ex)
            {

                result.Message = ex.ToString();
                result.Success = false;
            }

            return result;
        }
    }
}
