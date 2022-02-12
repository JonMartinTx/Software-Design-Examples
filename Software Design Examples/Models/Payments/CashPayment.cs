namespace Software_Design_Examples.Models.Payments
{
    internal class CashPayment : CustomerPayment
    {
        internal override int TransactionId { get; set; }
        internal override double TransactionAmount { get; set; }
        internal override bool IsValidPayment { get; set; }
        internal override bool IsApproved { get; set; }

        internal CashDenominations Denomination { get; set; }
        internal bool MeetsPurchacePrice { get; set; }
        internal bool SurpassesPurchacePrice { get; set; }
        internal double ChangeAmount { get; set; }
        internal bool HaveEnoughCashToPayRefund { get; set; } //todo write simple method to check cashinventory to see if we can produce change.
        

        internal enum CashDenominations
        {
            Penny,
            Nickel,
            Dime,
            Quarter,
            HalfDollar,
            Dollar,
            TwoDollar,
            FiveDollar,
            TenDollar,
            TwentyDollar,
            Invalid,
            UnrecognizedDenomination
        }
    }
}
