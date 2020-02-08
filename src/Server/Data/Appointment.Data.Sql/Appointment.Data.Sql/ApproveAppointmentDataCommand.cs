using Appointment.Data.Sql.Response;
using Appointment.DataContracts.Commands;
using Appointment.DataContracts.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using Appointment.Data.Sql.Model;
using System.Linq;

namespace Appointment.Data.Sql
{
    public class ApproveAppointmentDataCommand : IApproveAppointmentDataCommand
    {
        private string _connectionString;
        private string _storedproc = "dbo.ApproveAppointmentRequest";

        public ApproveAppointmentDataCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IApproveAppointmentRequestDataResponse Execute(Guid appointmentRequestId, DateTime appointmentDate, string details)
        {
            IApproveAppointmentRequestDataResponse response = new ApproveAppointmentRequestDataResponse();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                   var dataResult = conn.Query<NewAppointmentEntity>(
                        _storedproc,
                        new {
                            AppointmentRequestId = appointmentRequestId,
                            AppointmentDate = appointmentDate, Details = details },
                        commandType: System.Data.CommandType.StoredProcedure);

                    if (dataResult != null && dataResult.Count() == 1)
                    {
                        response.Success = true;
                        response.AppointmentId = dataResult.First().AppointmentId;
                    }


                    response.Success = true;
                }
            }
            catch (SqlException ex)
            {
               response.Success = false;
               response.Message = ex.ToString();
            }

            return response;
        }
    }
}
