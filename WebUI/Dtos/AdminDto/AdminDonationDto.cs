namespace WebUI.Dtos.AdminDto
{
    public class AdminDonationDto
    {
        public int RecepientId { get; set; }
        public int HospitalId { get; set; }
        public int DonationTypeId { get; set; }
        public int NecessityAmount { get; set; }
        public DateTime NecessityStartDate { get; set; }
        public bool IsActive { get; set; }
    }
}
