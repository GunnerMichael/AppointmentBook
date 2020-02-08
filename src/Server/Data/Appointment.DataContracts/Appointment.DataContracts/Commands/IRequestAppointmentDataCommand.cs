using Appointment.DataContracts.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.DataContracts.Commands
{
    public interface IRequestAppointmentDataCommand
    {
        IRequestAppointmentCommandResponse Execute(DateTime appointmentDate, string details); 
    }
}
