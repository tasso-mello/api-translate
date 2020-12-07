namespace domain.translate.Configurations
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public string Secret { get; set; }
        public string TimeExpire { get; set; }
    }
}
