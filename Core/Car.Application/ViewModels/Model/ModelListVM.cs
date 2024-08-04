namespace Car.Application.ViewModels
{
    public class ModelListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public Guid? ParentId { get; set; }
        public bool Status { get; set; }
        public int CarCount { get; set; }
    }
}
