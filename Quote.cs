namespace Doodle
{
    internal class Quote
    {
        public Guid QuoteId { get; set; }
        public Account Account { get; set; }

        public Guid CustomerId { get; set; }
    }
}