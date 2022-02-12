using System;
using System.Collections.Generic;

namespace Software_Design_Examples.Models.Configuration_File
{
    [Serializable]
    public class ConfigFile
    {
        public List<Inventory> Inventories { get; set; } = new List<Inventory>();
        public CashLedger Ledger { get; set; } = new CashLedger();
        public List<ActiveServiceRequests> ActiveRequests { get; set; } = new List<ActiveServiceRequests>();
    }
}
