

using Car.Domain.Entities;

namespace Car.Application.ViewModels
{
    public class HomeVM
    {
        public List<Category> Markas { get; set; }
        public List<Category> Models { get; set; }
        public List<Ban> Bans { get; set; }
        public List<City> Cities { get; set; }
        public List<Year> Years { get; set; }
    }
}
