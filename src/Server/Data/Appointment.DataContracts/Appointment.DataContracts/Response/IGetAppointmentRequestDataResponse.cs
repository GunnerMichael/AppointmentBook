using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.DataContracts.Response
{
    public interface IGetAppointmentRequestDataResponse
    {
        bool Success { get; set; }

        string Message { get; set; }

        Model.IApppointmentRequestModel AppointmentRequest { get; }
    }
}
