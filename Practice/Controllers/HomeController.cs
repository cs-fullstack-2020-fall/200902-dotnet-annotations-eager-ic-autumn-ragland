using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Practice.DAO;
using Practice.Models;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // reference to DataBase
        private readonly BandDbContext _context;

        public HomeController(ILogger<HomeController> logger, BandDbContext context)
        {
            _logger = logger;
            // set database on start up
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        // add a band with bound model
        [HttpPost]
        public IActionResult AddBand([Bind("id,bandName,musicGenre")] BandModel newBand)
        {
            // if values passed in body of request meet validation annotations set in model
            if(ModelState.IsValid)
            {
                // add new band from body of request to data set
                _context.Add(newBand);
                // save changes to database (added record)
                _context.SaveChanges();
                // retrun confirmation message
                return Content($"Addd Band {newBand.bandName}");
            } else
            // if values passed in body of request DOES NOT meet validation annotations set in model
            {
                // return errors from method
                return Content(DisplayString(ModelState));
            }
        }
        // list all bands
        public IActionResult ListBands()
        {
            // string to append to
            string displyStr = "";
            // for each band in the database (including associated albums)
            foreach (BandModel band in  _context.bands.Include(band => band.albums).ToList())
            {
                // append the properties of the band
                displyStr += $"Band ID : {band.id}\nName : {band.bandName}\nGenre : {band.musicGenre}\n----\nAlbums :\n";
                // for each album associated with each band
                foreach (AlbumModel album in band.albums)
                {
                    // append the properties of the album
                    displyStr += $"Album ID : {album.id}\nTitle : {album.albumTitle}\nDescription : {album.albumDesc}\nRating : {album.albumRating}\n";
                }
                // simple line seperator 
                displyStr += "\n---\n";
            }
            // display built string
            return Content(displyStr);
        }
        // add an album with bound model
        [HttpPost]
        public IActionResult AddAlbum([Bind("id,albumTitle,albumDesc,albumRating")] AlbumModel newAlbum, int bandID)
        {
            // find matching band based on bandID passed into body of request with access to the list of albums
            BandModel matchingBand = _context.bands.Include(band => band.albums).FirstOrDefault(band => band.id == bandID);
            // if values passed in body of request meet validation annotations set in model
            if(ModelState.IsValid)
            {
                // add new album from body of request to the data set
                _context.albums.Add(newAlbum);
                // add album to matching band in database
                matchingBand.albums.Add(newAlbum);
                // save changes to database (added record)
                _context.SaveChanges();
                // retrun confirmation message
                return Content($"Add Album {newAlbum.albumTitle}");
            } else
            // if values passed in body of request DOES NOT meet validation annotations set in model
            {
                // return errors from method
                return Content(DisplayString(ModelState));
            }
        }
        // output all model state errors - COPIED CODE
        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
        // format model start errors for display
        public static string DisplayString(ModelStateDictionary modelState)
        {
                string displayError = ""; // string to append to
                // for each error returned from the list of errors in above method > append error to display string
                GetErrorListFromModelState(modelState).ForEach(error => displayError += error);
                // return display string
                return (displayError);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
