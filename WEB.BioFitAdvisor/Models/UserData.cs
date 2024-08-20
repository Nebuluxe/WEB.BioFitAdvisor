namespace WEB.BioFitAdvisor.Models
{
    public class UserData
    {
        public string Token { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int Company_ID { get; set; }

        public DateTime ExpieredDate { get; set; }

        public bool KeepSession { get; set; }

        public UserData()
        {
            Token = string.Empty;
            ExpieredDate = Convert.ToDateTime("01-01-1990");
            Email = string.Empty;
            Name = string.Empty;
        }
    }
}
