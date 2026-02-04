namespace dotnetAssignment.Models
{
    public interface ITouristPlaceRepository
    {
        TouristPlace Get(long id);
        List<TouristPlace> GettAll();
        TouristPlace CreateNew(TouristPlace place);
        TouristPlace Update(TouristPlace place);
        TouristPlace Delete(long id);
    }
}
