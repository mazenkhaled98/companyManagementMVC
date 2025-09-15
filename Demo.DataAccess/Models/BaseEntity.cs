namespace Demo.DataAccess.Models
{
    internal class BaseEntity // include common properties
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } //user id
        public DateTime? CreatedOn { get; set; } //date time of creation

        public int ModifedBy { get; set; }

        public DateTime? ModifiedOn { get; set; } //the date time of modification

        public bool IsDeleted { get; set; } //soft delete
    }
}
