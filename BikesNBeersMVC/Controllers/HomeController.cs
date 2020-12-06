﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BikesNBeersMVC.Models;
using BikesNBeersMVC.Services;
using Microsoft.AspNetCore.Authorization;

namespace BikesNBeersMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       // [Authorize]
        public IActionResult Index()
        {
            var coordinate = new CoordinateHandler();
            var hotelResponse = new HotelHandler(coordinate);
            var breweryResponse = new BrewHandler(coordinate);
            var viewModel = new ViewModel();
            var routeService = new RouteHandler();
            var Coordinate1 = new Coordinate();
            var Coordinate2 = new Coordinate();
            Coordinate1.results = new BikesNBeersMVC.Models.Result[1];
            Coordinate2.results = new BikesNBeersMVC.Models.Result[1];

            Coordinate1.results[0] = new Models.Result();
            Coordinate2.results[0] = new Models.Result();

            Coordinate1.results[0].geometry = new Models.Geometry();
            Coordinate2.results[0].geometry = new Models.Geometry();

            Coordinate1.results[0].geometry.location = new Models.Location();
            Coordinate2.results[0].geometry.location = new Models.Location();



            Coordinate1.results[0].geometry.location.lat = 42.4806F;
            Coordinate1.results[0].geometry.location.lng = -83.47555F;
            Coordinate2.results[0].geometry.location.lat = 42.3684F;
            Coordinate2.results[0].geometry.location.lng = -83.3527F;
            var testRoute = routeService.GetRoute(Coordinate1, Coordinate2);
            var testCoordinate = coordinate.GetCoordinates(90210);
            var testHotelResponse = hotelResponse.GetHotel(90210);
            var testHotelResponseResult = testHotelResponse;
            var testbreweryResponse = breweryResponse.GetBrewery(90210);
            var testbreweryResponseResult = testbreweryResponse;
            viewModel.Breweries = testbreweryResponseResult;
            viewModel.HotelResponses = testHotelResponseResult;
            viewModel.Routes = testRoute.routes;

            return View(viewModel);
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
