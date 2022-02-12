using System;

namespace Software_Design_Examples.Models.Payments
{
    internal class CardPayment : CustomerPayment
    {
        internal override int TransactionId { get; set; }
        internal override double TransactionAmount { get; set; }
        internal override bool IsValidPayment { get; set; }
        internal override bool IsApproved { get; set; }

        internal long CardNumber { get; set; }
        internal DateTime ExpirationDate { get; set; }
        internal int Ccv { get; set; }
        internal CardServicer Servicer { get; set; }

        internal enum CardServicer
        {
            Visa,
            MasterCard,
            Discovery,
            AmericanExpress,
            Invalid
        }
    }
}
