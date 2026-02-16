
namespace dotnetAssignment.Models
{
    public class TouristPlaceRepository : ITouristPlaceRepository
    {
        private readonly AppDbContext _context;

        public TouristPlaceRepository(AppDbContext context)
        {
            _context = context;
        }
        public TouristPlace CreateNew(TouristPlace place)
        {
            _context.Tourists.Add(place);
            _context.SaveChanges();
            return place;
        }

        public TouristPlace Delete(long id)
        {
            TouristPlace place = _context.Tourists.Find(id);
            if (place != null)
            {
                _context.Tourists.Remove(place);
                _context.SaveChanges();
            }
            return place;
        }

        public TouristPlace Get(long id)
        {
            TouristPlace place = _context.Tourists.Find(id);
            return place;
        }

        public List<TouristPlace> GettAll()
        {
            return _context.Tourists.ToList();
        }

        public TouristPlace Update(TouristPlace updatedPlace)
        {
            TouristPlace place = _context.Tourists.Find(updatedPlace.Id);
            if (place != null)
            {
                place.Name = updatedPlace.Name;
                place.Address = updatedPlace.Address;
                place.Rating = updatedPlace.Rating;
                place.Type = updatedPlace.Type;
                place.PhotoPath = updatedPlace.PhotoPath;
                _context.SaveChanges();
            }
            return updatedPlace;
        }
    }
}
