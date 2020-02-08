using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.DataContracts.Commands
{
    public interface IGetOutstandingAppointmentRequestDataCommand
    {
        Response.IGetOutstandingAppointmentRequestsCommandResponse Execute();
    }
}
