using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.DataContracts.Commands
{
    public interface IApproveAppointmentDataCommand
    {
        Response.IApproveAppointmentRequestDataResponse Execute(Guid appointmentRequestId, DateTime appointmentDate, string details);
    }
}
