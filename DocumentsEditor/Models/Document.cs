namespace DocumentsEditor.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HtmlText { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
