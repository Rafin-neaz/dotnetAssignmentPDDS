namespace dotnetAssignment.Models
{
    public class MockTouristPlaceRepository : ITouristPlaceRepository
    {
        private List<TouristPlace> _touristPlaces = new List<TouristPlace>();

        public MockTouristPlaceRepository()
        {
            _touristPlaces = new List<TouristPlace>
                {
                    new TouristPlace { Id = 1, Name = "Cox's Bazar", Address = "Chittagong", Rating = 3, Type = PlaceType.Beach, PhotoPath = "noimage.jpg" },
                    new TouristPlace { Id = 2, Name = "Sundarbans", Address = "Khulna", Rating = 2, Type = PlaceType.Beach, PhotoPath = "noimage.jpg" },
                    new TouristPlace { Id = 3, Name = "Sajek Valley", Address = "Rangamati", Rating = 4, Type = PlaceType.Beach, PhotoPath = "noimage.jpg" },
                    new TouristPlace { Id = 4, Name = "Saint Martin's Island", Address = "Cox's Bazar", Rating = 5, Type = PlaceType.Beach, PhotoPath = "noimage.jpg" },
                    new TouristPlace { Id = 5, Name = "Kuakata Beach", Address = "Patuakhali", Rating = 1, Type = PlaceType.Beach, PhotoPath = "noimage.jpg" },
                    new TouristPlace { Id = 6, Name = "Ratargul Swamp Forest", Address = "Sylhet", Rating = 4, Type = PlaceType.Beach, PhotoPath = "noimage.jpg" },
                    new TouristPlace { Id = 7, Name = "Srimangal Tea Gardens", Address = "Moulvibazar", Rating = 3, Type = PlaceType.Beach, PhotoPath = "noimage.jpg" },
                    new TouristPlace { Id = 8, Name = "Paharpur", Address = "Naogaon", Rating = 2, Type = PlaceType.Beach, PhotoPath = "noimage.jpg" }
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
