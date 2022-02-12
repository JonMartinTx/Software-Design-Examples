namespace Software_Design_Examples.Models.Beverages
{
    public abstract class Beverages
    {
        internal virtual int Id { get; set; }
        internal virtual string Name { get; set; } = string.Empty;
        internal virtual double Price { get; set; }
    }
}
