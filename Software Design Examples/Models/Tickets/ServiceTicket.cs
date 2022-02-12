using System;

namespace Software_Design_Examples.Models.Tickets
{
    internal class ServiceTicket
    {
        internal virtual ServiceType TicketType { get; set; }
        internal virtual int Id { get; set; }
        internal virtual DateTime DateOfServiceTicket { get; set; }
        internal virtual string Message { get; set; }

        internal enum ServiceType
        {
            Refill,
            CashDrawerFull,
            CashReservesLow,
            PartsReplacement,
            None,
            Invalid
        }
    }
}
