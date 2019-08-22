namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Entities
{
    public class Reversal
    {
        public Reversal(StockQuote quote, ReversalDirection direction)
        {
            StockQuote = quote;
            Direction = direction;
        }

        public ReversalDirection Direction { get; set; }

        public StockQuote StockQuote { get; set; }
    }
}
