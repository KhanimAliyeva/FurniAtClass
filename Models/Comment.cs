namespace FurniMpa201.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string EmployeeId { get; set; }

        public bool IsAccepted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
