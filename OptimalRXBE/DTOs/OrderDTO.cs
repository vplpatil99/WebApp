namespace OptimalRXBE.DTOs
{
    public class OrderDTO
    {
        public string Party_code { get; set; } = null!;
        public string Order_No { get; set; } = null!;
        public string GOrderNo { get; set; } = null!;
        public DateTime? OrderEntryTime { get; set; }
        public string Lens_type { get; set; } = null!;
        public string CoatColor { get; set; } = null!;
        public double? Rate { get; set; } = 0;
        public int Qty { get; set; } = 0;
        public string UserName { get; set; } = null!;
        public string Fitting { get; set; } = null!;
        public string stockorder { get; set; }
        public string CurrentStage { get; set; } = null!;
        public string L_OrderNo { get; set; } = null!;
        public string registerno { get; set; } = null!;
        public string party_cust_code { get; set; } = null!;
        public string marketingPerson { get; set; } = null!;
    }
}
