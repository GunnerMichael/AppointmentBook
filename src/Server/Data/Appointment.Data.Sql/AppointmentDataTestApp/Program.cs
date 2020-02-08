using Appointment.Data.Sql;
using Appointment.Data.Sql.Response;
using Appointment.DataContracts.Commands;
using Appointment.DataContracts.Response;
using System;

namespace AppointmentDataTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyAppointment;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            IRequestAppointmentDataCommand command = new RequestAppointmentDataCommand(
                connectionString);

            var result = command.Execute(new DateTime(2020, 10, 23), "Hello, this is a test request for an appointment");

            if (result.Success)
            {
                Console.WriteLine($"ID: {result.AppointmentRequestId}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            IGetAppointmentRequestDataCommand getCommand = new GetAppointmentRequestDataCommand(connectionString);

            var getResult = getCommand.Execute(result.AppointmentRequestId);

            if (getResult.Success)
            {
                Console.WriteLine($"{getResult.AppointmentRequest.AppointmentDate} - {getResult.AppointmentRequest.Details}");

                IApproveAppointmentDataCommand approveCommand = new ApproveAppointmentDataCommand(connectionString);

                var approveResult = approveCommand.Execute(getResult.AppointmentRequest.AppointmentRequestId, getResult.AppointmentRequest.AppointmentDate, getResult.AppointmentRequest.Details);
                if (approveResult.Success)
                {
                    Console.WriteLine(approveResult.AppointmentId);
                }
                else
                {
                    Console.WriteLine(approveResult.Message);
                }
            }
            else
            {
                Console.WriteLine(getResult.Message);
            }

            IGetOutstandingAppointmentRequestDataCommand outCommand = new GetOutstandingAppointmentRequestDataCommand(connectionString);

            var outResult = outCommand.Execute();

            if (outResult.Success)
            {
                foreach (var item in outResult.OutstandingAppointmentRequests)
                {
                    Console.WriteLine($"{item.AppointmentRequestId} - {item.AppointmentDate} - {item.Details}");
                }
            }
            else
            {
                Console.WriteLine(outResult.Message);
            }
            
        }
    }
}
