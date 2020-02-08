using System;

namespace Appointment.DataContracts.Model
{
    public interface IApppointmentRequestModel
    {
        DateTime AppointmentDate { get; set; }

        string Details { get; set; }

        bool Approved { get; set; }

        Guid AppointmentRequestId { get; set; }

        DateTime RequestedDate { get; set; }
    }
}
