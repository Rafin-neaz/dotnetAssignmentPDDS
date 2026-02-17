using dotnetAssignment.Models;

namespace dotnetAssignment.Repository
{
    public interface ITouristPlaceRepository
    {
        Task<TouristPlace> Get(long id);
        Task<List<TouristPlace>> GetAll();
        Task<TouristPlace> CreateNew(TouristPlace place);
        Task<TouristPlace> Update(TouristPlace place);
        Task<TouristPlace> Delete(long id);
    }
}
