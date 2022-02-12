namespace Software_Design_Examples.Models.Payments
{
    internal abstract class CustomerPayment
    {
        internal virtual int TransactionId { get; set; }
        internal virtual double TransactionAmount { get; set; }
        internal virtual bool IsValidPayment { get; set; }
        internal virtual bool IsApproved { get; set; }
    }
}
