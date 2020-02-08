using Appointment.DataContracts.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Data.Sql.Response
{
    public class RequestAppointmentCommandResponse : IRequestAppointmentCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid AppointmentRequestId { get; set; }
    }
}
