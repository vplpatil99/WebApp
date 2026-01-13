namespace OptimalRXBE.DTOs
{
    public class OrderDTO
    {
        public string Party_code { get; set; } = null!;
        public string Order_No { get; set; } = null!;
        public string GOrderNo { get; set; } = null!;
        public DateTime? ReceivedOnDate { get; set; }
        public DateTime? Order_Date { get; set; }
        public string Lens_type { get; set; } = null!;
        public DateTime? OrderEntryTime { get; set; }
        public string CoatColor { get; set; } = null!;
        public string Fitting { get; set; } = null!;
        public string TintColor { get; set; } = null!;
        public string CurrentStage { get; set; } = null!;
        public string L_OrderNo { get; set; } = null!;
        public string registerno { get; set; } = null!;
        public string party_cust_code { get; set; } = null!;
        public string Loc_code { get; set; } = null!;
        public string stockorder { get; set; }
        public double? Rate { get; set; }
        public double? ActualRate { get; set; }
    }
}
