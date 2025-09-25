namespace Demo.presentation.ViewModels
{
    public class DepartmentEditViewModel
    {
        public string Code { get; set; }=string.Empty;
        public string Name { get; set; }=string.Empty;

        public string? Description { get; set; }
        public DateOnly Createdon { get; set; }
    }
}
