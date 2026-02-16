using Microsoft.EntityFrameworkCore;

namespace dotnetAssignment.Models
{
    public class TouristPlaceRepository : ITouristPlaceRepository
    {
        private readonly AppDbContext _context;

        public TouristPlaceRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TouristPlace CreateNew(TouristPlace place)
        {
            if (place == null)
                throw new ArgumentNullException(nameof(place));

            _context.Tourists.Add(place);
            _context.SaveChanges();

            return place;
        }

        public TouristPlace Delete(long id)
        {
            var place = _context.Tourists.Find(id);
            if (place == null)
                return null;

            _context.Tourists.Remove(place);
            _context.SaveChanges();

            return place;
        }

        public TouristPlace Get(long id)
        {
            return _context.Tourists
                           .AsNoTracking()
                           .FirstOrDefault(x => x.Id == id);
        }

        public List<TouristPlace> GettAll()
        {
            return _context.Tourists
                           .AsNoTracking()
                           .ToList();
        }

        public TouristPlace Update(TouristPlace updatedPlace)
        {
            if (updatedPlace == null)
                throw new ArgumentNullException(nameof(updatedPlace));

            var existingPlace = _context.Tourists.Find(updatedPlace.Id);
            if (existingPlace == null)
                return null;

            existingPlace.Name = updatedPlace.Name;
            existingPlace.Address = updatedPlace.Address;
            existingPlace.Rating = updatedPlace.Rating;
            existingPlace.Type = updatedPlace.Type;
            existingPlace.PhotoPath = updatedPlace.PhotoPath;

            _context.SaveChanges();

            return existingPlace;
        }
    }
}
