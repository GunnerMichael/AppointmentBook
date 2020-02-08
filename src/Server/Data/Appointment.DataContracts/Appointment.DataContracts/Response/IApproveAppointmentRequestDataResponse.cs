using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.DataContracts.Response
{
    public interface IApproveAppointmentRequestDataResponse
    {
        bool Success { get; set; }

        string Message { get; set; }

        public int AppointmentId { get; set; }
    }
}
