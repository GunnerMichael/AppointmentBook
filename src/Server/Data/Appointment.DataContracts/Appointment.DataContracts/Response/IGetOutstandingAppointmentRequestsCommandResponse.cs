using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.DataContracts.Response
{
    public interface IGetOutstandingAppointmentRequestsCommandResponse
    {
        bool Success { get; set; }

        string Message { get; set; }

        public List<Model.IApppointmentRequestModel> OutstandingAppointmentRequests { get; }
    }
}
