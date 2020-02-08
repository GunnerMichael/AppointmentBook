using Appointment.DataContracts.Model;
using Appointment.DataContracts.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Data.Sql.Response
{
    public class GetAppointmentRequestDataResponse : IGetAppointmentRequestDataResponse
    {

        public bool Success { get; set; }

        public string Message { get; set; }

        public IApppointmentRequestModel AppointmentRequest { get; set; }

    }
}
