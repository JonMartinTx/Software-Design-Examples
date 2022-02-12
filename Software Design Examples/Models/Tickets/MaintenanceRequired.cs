using System;
using Software_Design_Examples.Models.Inventory_Management;

namespace Software_Design_Examples.Models.Tickets
{
    internal class MaintenanceRequired : ServiceTicket
    {
        internal override int Id { get; set; }
        internal override DateTime DateOfServiceTicket { get; set; }
        internal override ServiceType TicketType => ServiceType.PartsReplacement;
        internal override string Message { get; set; }
        internal CashInventory CashInMachine { get; set; }
    }
}
