namespace Software_Design_Examples.Models.Beverages
{
    internal class Lemonade : Beverages
    {
        internal override int Id { get; set; }
        internal override string Name { get; set; } = string.Empty;
        internal override double Price { get; set; }
    }
}
