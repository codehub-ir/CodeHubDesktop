namespace CodeHubDesktop.Models
{
    public class CreateSnippetModel
    {
        public string title { get; set; }
        public string detail { get; set; }
        public string script { get; set; }
        public string language { get; set; }
        public string error { get; set; }
    }

    public class GetSnippetModel
    {
        public string SID { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public string script { get; set; }
        public string language { get; set; }
        public string pub_date { get; set; }
        public string link { get; set; }
        public string error { get; set; }
    }
}
