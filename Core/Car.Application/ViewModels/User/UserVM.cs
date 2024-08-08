
namespace Car.Application.ViewModels
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public int CarCount { get; set; }
    }
}
