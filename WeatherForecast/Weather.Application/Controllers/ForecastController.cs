using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.Domain.WebServices;
using Weather.Domain.Repositories;
using Weather.Domain;
using Weather.Application.ViewModels;

namespace Weather.Application.Controllers
{
    public class ForecastController : Controller
    {
        private readonly IWeatherService _service;
        
        public ForecastController(IWeatherService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            LocationViewModel model = new LocationViewModel();
            return View(model);
        }

        public ActionResult BackToSearch()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Name")] LocationViewModel model)
        {           
            try
              {
                  if (ModelState.IsValid)
                  {
                      Session.Abandon();
                      _service.RefreshLocations(model.Name);
                      model.Locations = _service.GetLocations();
                      
                      if (model.Locations == null)
                      {
                          TempData["error"] = String.Format("{0}{1}{2}", "Sorry we can't find ", model.Name, ". Try something else.");
                      }
                  }
              }
              catch (Exception)
              {
                  TempData["error"] = String.Format("{0}{1}{2}", "Sorry we can't get location ", model.Name, ". Try again in a little while.");
                  return RedirectToAction("Index");
              }
            return View(model);
        }


        public ActionResult LocationWeatherDetails(string identifier, string name, LocationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Identifier = identifier;
                    model.Name = name;
                    
                    if (Session["Location"] == null || Convert.ToString(Session["Location"]) != model.Identifier)
                    {
                        _service.RefreshForecasts(model.Identifier);
                    }
                    Session["Location"] = model.Identifier;
                    Session.Timeout = 30;
                    
                    model.Forecasts = _service.GetForecasts();
                    
                    if (model.Forecasts == null)
                    {
                        TempData["error"] = String.Format("{0}{1}{2}", "Sorry but we find any forecasts for ", model.Name, ". Try something else.");
                        return RedirectToAction("Index");
                    }
                }     
            }
            catch (Exception)
            {
                TempData["error"] = String.Format("{0}{1}{2}", "Sorry we can't get forecast for ", model.Name, ". Try again in a little while.");
                return RedirectToAction("Index");
            }
            return View("LocationWeatherDetails", model);
        }
    }       
}