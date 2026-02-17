using AutoMapper;
using dotnetAssignment.Models;
using dotnetAssignment.ViewModels;

namespace dotnetAssignment.Mapper
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<TouristPlace, TouristPlaceUpdateViewModel>();
            CreateMap<TouristPlaceUpdateViewModel, TouristPlace>();
        }
    }
}
