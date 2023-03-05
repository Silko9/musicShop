namespace musicShop.Models
{
    public class RecordPerformance
    {
        public int RecordId { get; set; }

        public Record? Record { get; set; }

        public int PerformanceId { get; set; }

        public Performance? Performance { get; set; }
    }
}
