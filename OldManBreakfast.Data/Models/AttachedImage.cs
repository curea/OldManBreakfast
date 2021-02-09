namespace OldManBreakfast.Data.Models
{
    public class AttachedImage : BaseEntity
    {
        public string Source { get; set; }
        public string Url { get; set; }
        public string Target { get; set; } = "_blank";
    }
}