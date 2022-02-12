using System;
using System.Collections.Generic;

namespace Software_Design_Examples.Models.Tickets
{
    internal class Refill : ServiceTicket
    {
        internal override int Id { get; set; }
        internal override ServiceType TicketType => ServiceType.Refill;
        internal override DateTime DateOfServiceTicket { get; set; }
        internal override string Message { get; set; }

        internal IEnumerable<Beverages.Beverages> BeveragesToRefill { get; set; } = new List<Beverages.Beverages>();


    }
}
