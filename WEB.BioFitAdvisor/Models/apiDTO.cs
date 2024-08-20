namespace WEB.BioFitAdvisor.Models
{
    public class apiDTO
    {
        public bool success { get; set; }
        public int status { get; set; }
        public string response { get; set; }

        public apiDTO()
        {
            success = false;
            response = string.Empty;
        }
    }
}
