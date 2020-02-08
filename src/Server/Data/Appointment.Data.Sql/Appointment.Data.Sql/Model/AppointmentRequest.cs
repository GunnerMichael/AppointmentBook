using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Data.Sql.Model
{
    public class AppointmentRequest : Appointment.DataContracts.Model.IApppointmentRequestModel
    {
        public DateTime AppointmentDate { get; set; }
        public string Details { get; set; }
        public bool Approved { get; set; }
        public Guid AppointmentRequestId { get; set; }
        public DateTime RequestedDate { get; set; }
    }
}
