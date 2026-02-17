using dotnetAssignment.Models;
using dotnetAssignment.Repository;
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

        public async Task<IActionResult> Index(string? searchString, string? sortOrder)
        {
            var places = await _touristPlaceRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                places = places
                    .Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder.ToLower())
                {
                    case "asc":
                        places = places.OrderBy(p => p.Rating).ToList();
                        break;
                    case "desc":
                        places = places.OrderByDescending(p => p.Rating).ToList();
                        break;
                    default: 
                        break;
                }
            }

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSort = sortOrder;
            var model = new AllTouristPlaces
            {
                Title = "All tourist places",
                touristPlaces = places
            };
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_TouristPlaceTableDataPartialView", model);
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult AddNewTouristPlace(string? searchString, string? sortOrder)
        {
            TouristPlaceCreateModel model = new TouristPlaceCreateModel();
            model.Title = "Add New Tourist Place";

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSort = sortOrder;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTouristPlace(TouristPlaceCreateModel placeCreate, long? Id, string? searchString, string? sortOrder)
        {
            if (ModelState.IsValid)
            {
                TouristPlace PlaceFromDb = null;
                if (Id > 0)
                {
                    PlaceFromDb = await _touristPlaceRepository.Get(Id.Value);
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
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        placeCreate.Photo.CopyTo(stream); 
                    }
                }
                else if (placeCreate.ExistingPhotoPath != null)
                {
                    uniqueFileName = placeCreate.ExistingPhotoPath;
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
                    UpdatedTouristPlace = await _touristPlaceRepository.Update(UpdatedTouristPlace);
                }
                else
                {
                    UpdatedTouristPlace = await _touristPlaceRepository.CreateNew(UpdatedTouristPlace);
                }

                return RedirectToAction("details", new { id = UpdatedTouristPlace.Id, searchString = searchString, sortOrder = sortOrder });
            }

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSort = sortOrder;

            return View(placeCreate);
        }

        [HttpGet]
        public async Task<ViewResult> UpdateTouristPlace(long id, string? searchString, string? sortOrder)
        {
            TouristPlace place = await _touristPlaceRepository.Get(id);
            var PhotoPath = "images/" + (place.PhotoPath ?? "noimage.jpg");
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, PhotoPath);

            TouristPlaceCreateModel touristPlaceCreateModel = new TouristPlaceCreateModel();
            FormFile formFile;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                formFile = new FormFile(stream, 0, stream.Length, "Photo", Path.GetFileName(filePath));
            }

            touristPlaceCreateModel = new TouristPlaceCreateModel()
            {
                Id = place.Id,
                Name = place.Name,
                Address = place.Address,
                Rating = place.Rating,
                Type = place.Type,
                Photo = formFile,
                Title = "Update Tourist Place",
                ExistingPhotoPath = place.PhotoPath
            };

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSort = sortOrder;

            return View("~/Views/Home/AddNewTouristPlace.cshtml", touristPlaceCreateModel);
        }

        public async Task<ViewResult> Details(long id, string? searchString, string? sortOrder)
        {
            TouristPlace existingPlace = await _touristPlaceRepository.Get(id);
            if (existingPlace == null || existingPlace.Id == 0)
            {
                Response.StatusCode = 404;
                return View("NotFound", id);
            }
            TouristPlaceViewModel model = new TouristPlaceViewModel()
            {
                Title = "Tourist Place Details",
                TouristPlace = await _touristPlaceRepository.Get(id)
            };

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSort = sortOrder;

            return View(model);
        }

        public IActionResult Delete(long id, string? searchString, string? sortOrder)
        {
            _touristPlaceRepository.Delete(id);
            return RedirectToAction("index", new { searchString = searchString, sortOrder = sortOrder });
        }
    }
}