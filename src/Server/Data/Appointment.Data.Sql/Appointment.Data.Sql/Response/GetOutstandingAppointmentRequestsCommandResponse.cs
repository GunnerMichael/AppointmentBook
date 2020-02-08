using Appointment.Data.Sql.Model;
using Appointment.DataContracts.Model;
using Appointment.DataContracts.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Data.Sql.Response
{
    public class GetOutstandingAppointmentRequestsCommandResponse : IGetOutstandingAppointmentRequestsCommandResponse
    {
        private List<IApppointmentRequestModel> _outstanding = new List<IApppointmentRequestModel>();

        public bool Success { get; set; }

        public string Message { get; set; }

        public List<IApppointmentRequestModel> OutstandingAppointmentRequests => _outstanding;




    }
}
