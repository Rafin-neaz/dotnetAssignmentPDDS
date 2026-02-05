namespace dotnetAssignment.Models
{
    public class MockTouristPlaceRepository : ITouristPlaceRepository
    {
        private List<TouristPlace> _touristPlaces = new List<TouristPlace>();

        public MockTouristPlaceRepository()
        {
            _touristPlaces = new List<TouristPlace>() {
                new TouristPlace() { Id = 1, Address="Cox's Bazar", Name="Cox bazar Sea Beach", Rating=3, Type=PlaceType.Beach},
                new TouristPlace() { Id = 2, Address="Khulna", Name="SundarBan", Rating=4,Type=PlaceType.Landmark},
                new TouristPlace() { Id = 3, Address="Lalbag", Name="Lalbag Fort", Rating=3, Type=PlaceType.Landmark},
                new TouristPlace() { Id = 4, Address="Cumilla", Name="Mohasthan Gor", Rating=4, Type=PlaceType.Hills}
            };
        }

        public TouristPlace Get(long Id)
        {
            return _touristPlaces.FirstOrDefault(place => place.Id == Id) ?? new TouristPlace();
        }

        public List<TouristPlace> GettAll()
        {
            return _touristPlaces;
        }

        public TouristPlace CreateNew(TouristPlace place)
        {
            long v = _touristPlaces.Max(place => place.Id) + 1;
            place.Id = v;
            _touristPlaces.Add(place);
            return place;
        }

        public TouristPlace Update(TouristPlace place)
        {
            TouristPlace existingPlace = _touristPlaces.FirstOrDefault(tp => tp.Id == place.Id);
            if (existingPlace != null)
            {
                existingPlace.Id = place.Id;
                existingPlace.Address = place.Address;
                existingPlace.Name = place.Name;
                existingPlace.Type = place.Type;
                existingPlace.Rating = place.Rating;
                existingPlace.PhotoPath = place.PhotoPath;
            }
            return place;
        }

        public TouristPlace Delete(long id)
        {
            TouristPlace place = _touristPlaces.FirstOrDefault(place =>place.Id == id);
            if (place != null)
            {
                _touristPlaces.Remove(place);
            }
            return place;
        }
    }
}
