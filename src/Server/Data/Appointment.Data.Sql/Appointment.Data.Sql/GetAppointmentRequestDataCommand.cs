using Appointment.Data.Sql.Model;
using Appointment.Data.Sql.Response;
using Appointment.DataContracts.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace Appointment.DataContracts.Commands
{
    public class GetAppointmentRequestDataCommand :  IGetAppointmentRequestDataCommand
    {
        private string _connectionString;
        private string _storedproc = "dbo.GetAppointmentRequest";

        public GetAppointmentRequestDataCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IGetAppointmentRequestDataResponse Execute(Guid requestId)
        {
            GetAppointmentRequestDataResponse result = new GetAppointmentRequestDataResponse();

            try
            {

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    var dataResult = conn.Query<AppointmentRequest>(
                        _storedproc,
                        new { @AppointmentRequestId = requestId },
                        commandType: System.Data.CommandType.StoredProcedure);

                    if (dataResult != null)
                    {

                        result.AppointmentRequest = dataResult.First();
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
