using dotnetAssignment.Models;

namespace dotnetAssignment.ViewModels
{
    public class AllTouristPlaces
    {
        public string Title { get; set; }
        public List<TouristPlace> touristPlaces { get; set; }
    }
}
