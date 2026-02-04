using dotnetAssignment.Models;
using dotnetAssignment.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace dotnetAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITouristPlaceRepository _touristPlaceRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ITouristPlaceRepository touristPlaceRepository, IWebHostEnvironment webHostEnvironment)
        {
            _touristPlaceRepository = touristPlaceRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        public ViewResult Index()
        {
            AllTouristPlaces touristPlaces = new AllTouristPlaces()
            {
                Title = "All tourist places",
                touristPlaces = _touristPlaceRepository.GettAll()
            };
            return View(touristPlaces);
        }
        [HttpGet]
        public ViewResult AddNewTouristPlace()
        {
            TouristPlaceCreateModel model = new TouristPlaceCreateModel();
            model.Title = "Add New Tourist Place";
            return View(model);
        }

        [HttpPost]
        public IActionResult AddNewTouristPlace(TouristPlaceCreateModel placeCreate, long? Id)
        {
            if (ModelState.IsValid)
            {
                TouristPlace PlaceFromDb = null;
                if (Id > 0)
                {
                    PlaceFromDb = _touristPlaceRepository.Get(Id.Value);
                }
                string filePath = null;
                string uniqueFileName = null;
                if (placeCreate.Photo != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + placeCreate.Photo.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    placeCreate.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                
                TouristPlace UpdatedTouristPlace = new TouristPlace()
                {
                    Id = placeCreate.Id,
                    Name = placeCreate.Name,
                    Address = placeCreate.Address,
                    Rating = placeCreate.Rating,
                    Type = placeCreate.Type, 
                    PhotoPath = uniqueFileName
                };
                if (PlaceFromDb != null)
                {
                    UpdatedTouristPlace = _touristPlaceRepository.Update(UpdatedTouristPlace);
                } 
                else
                {
                    UpdatedTouristPlace = _touristPlaceRepository.CreateNew(UpdatedTouristPlace);
                }
                    
                return RedirectToAction("details", new { id = UpdatedTouristPlace.Id });
            }
            return View(placeCreate);
        }

        [HttpGet]
        public ViewResult UpdateTouristPlace(long id)
        {
            TouristPlace place = _touristPlaceRepository.Get(id);
            var PhotoPath = "images/" + (place.PhotoPath ?? "noimage.jpg");
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, PhotoPath);

            using var stream = new FileStream(filePath, FileMode.Open);
            var formFile = new FormFile(stream, 0, stream.Length, "Photo", Path.GetFileName(filePath));
            TouristPlaceCreateModel touristPlaceCreateModel = new TouristPlaceCreateModel()
            {
                Id = place.Id,
                Name = place.Name,
                Address = place.Address,
                Rating = place.Rating,
                Type = place.Type, 
                Photo = formFile, 
                Title = "Update Tourist Place"
            };
            return View("~/Views/Home/AddNewTouristPlace.cshtml", touristPlaceCreateModel);
        }

        public ViewResult Details(long id)
        {
            TouristPlace existingPlace = _touristPlaceRepository.Get(id); 
            if (existingPlace == null)
            {
                Response.StatusCode = 404;
                return View("NotFound", id);
            }
            TouristPlaceViewModel model = new TouristPlaceViewModel()
            {
                Title = "Tourist Place Details",
                TouristPlace = _touristPlaceRepository.Get(id)
            };
            return View(model);
        }

        public IActionResult Delete(long id)
        {
            _touristPlaceRepository.Delete(id);
            return RedirectToAction("index");
        }
    }
}
