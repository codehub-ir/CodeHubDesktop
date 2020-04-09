namespace CodeHubDesktop.Models
{
    public class SnippetsModel : DomainObject
    {
        public string SId { get; set; }
        public string Detail { get; set; }
        public string Script { get; set; }
        public string Language { get; set; }
        public string PubDate { get; set; }
        public string Link { get; set; }
        public string Error { get; set; }
    }
}
