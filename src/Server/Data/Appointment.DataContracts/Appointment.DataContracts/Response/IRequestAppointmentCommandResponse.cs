using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.DataContracts.Response
{
    public interface IRequestAppointmentCommandResponse
    {
        bool Success { get; set; }

        string Message { get; set; }

        Guid AppointmentRequestId { get; set; }
    }
}
