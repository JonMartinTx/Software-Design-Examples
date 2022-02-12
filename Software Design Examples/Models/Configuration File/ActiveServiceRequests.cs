using System;

namespace Software_Design_Examples.Models.Configuration_File
{
    [Serializable]
    public class ActiveServiceRequests
    {
        public int Id { get; set; }
        public string ServiceRequestType { get; set; }
        public string DateOfRequest { get; set; }
        public string Message { get; set; }
    }
}
