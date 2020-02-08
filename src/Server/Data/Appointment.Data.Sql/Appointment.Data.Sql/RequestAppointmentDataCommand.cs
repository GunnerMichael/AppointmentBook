using Appointment.Data.Sql.Response;
using Appointment.DataContracts.Commands;
using Appointment.DataContracts.Response;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Appointment.Data.Sql
{
    public class RequestAppointmentDataCommand : IRequestAppointmentDataCommand
    {
        private string _connectionString;
        private string _storedproc = "NewAppointmentRequest";

        public RequestAppointmentDataCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IRequestAppointmentCommandResponse Execute(DateTime appointmentDate, string details)
        {
            RequestAppointmentCommandResponse requestAppointmentDataCommand = new RequestAppointmentCommandResponse();

            try
            {
                using(SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    var dataResult = conn.Query<Model.NewAppointmentRequestEntity>(
                        _storedproc, 
                        new { AppointmentDate = appointmentDate, Details = details },
                        commandType: System.Data.CommandType.StoredProcedure);

                    if (dataResult != null && dataResult.Count() == 1)
                    {
                        requestAppointmentDataCommand.AppointmentRequestId = dataResult.First().AppointmentRequestId;
                        requestAppointmentDataCommand.Success = true;
                    }
                }
            }
            catch(SqlException ex)
            {
                requestAppointmentDataCommand.Success = false;
                requestAppointmentDataCommand.Message = ex.ToString();
            }


            return requestAppointmentDataCommand;
        }
    }
}
