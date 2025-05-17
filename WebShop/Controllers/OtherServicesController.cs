using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Domain.STO;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class OtherServicesController : Controller
    {
        // GET: OtherServices
        public ActionResult CreateSto()
        {
            return View();
        }

        public STO ViewToSTO(STOViewModel model)
        {
            return new STO
            {
                Id = model.Id,
                UserId = model.UserId,
                CarBrand = model.CarBrand,
                Model = model.Model,
                EngineVolume = model.EngineVolume,
                Year = model.Year,
                Licenseplate = model.Licenseplate,
                Vin = model.Vin,
                BranchNumber = model.BranchNumber,
                Comment = model.Comment,
                Status = model.Status,
                RegistrationData = DateTime.Now,
                TimeOfRegistration = DateTime.Now.ToString("HH:mm:ss")
            };

        }
        public STOViewModel STOToview(STO model)
        {
            return new STOViewModel
            {
                Id = model.Id,
                UserId = model.UserId,
                CarBrand = model.CarBrand,
                Model = model.Model,
                EngineVolume = model.EngineVolume,
                Year = model.Year,
                Licenseplate = model.Licenseplate,
                Vin = model.Vin,
                BranchNumber = model.BranchNumber,
                Comment = model.Comment,
                Status = model.Status,
                RegistrationData = DateTime.Now,
                TimeOfRegistration = DateTime.Now.ToString("HH:mm:ss")
            };
        }

    }
}