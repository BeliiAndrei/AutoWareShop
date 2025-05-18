using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.BLogic;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.STO;
using WebShop.Filter;
using WebShop.Filters;
using WebShop.Models;

namespace WebShop.Controllers
{
    [UserOnly]
    public class OtherServicesController : Controller
    {

        private readonly ISTO _sto;
        public OtherServicesController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _sto = bl.GetSTOBl();
        }
        public ActionResult CreateSto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSto(STOViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = SessionHelper.User;
                if (user != null)
                {
                    model.UserId = user.Id;
                    STO sto = ViewToSTO(model);
                    sto.Status = "ожидание";
                    var result = _sto.CreateSTO(sto);
                    if (result)
                    {
                        TempData["Message"] = "Success";
                        return RedirectToAction("MySTORecords");
                    }
                }
            }
            TempData["Message"] = "Something went wrong";
            return View(model);
        }




        public ActionResult MySTORecords()
        {
            var user = SessionHelper.User;
            if (user != null)
            {
                var records = _sto.GetAllSTO().Where(x => x.UserId == user.Id).ToList();

                if (!records.Any())
                {
                    ViewBag.NoRecords = true;
                }

                return View(records.Select(STOToview).ToList());
            }

            TempData["Message"] = "You need to be logged in";
            return RedirectToAction("Login", "Account");
        }

        public ActionResult EditSTO(int id)
        {
            var user = SessionHelper.User;
            if (user != null)
            {
                var record = _sto.GetAllSTO().FirstOrDefault(x => x.Id == id && x.UserId == user.Id);
                if (record != null)
                {
                    return View(STOToview(record));
                }
            }

            TempData["Message"] = "Record not found or you don't have permission";
            return RedirectToAction("MySTORecords");
        }

        [HttpPost]
        public ActionResult EditSTO(STOViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = SessionHelper.User;
                if (user != null && model.UserId == user.Id)
                {
                    STO sto = ViewToSTO(model);
                    var result = _sto.UpdateSTO(sto);
                    if (result)
                    {
                        TempData["Message"] = "Record updated successfully";
                        return RedirectToAction("MySTORecords");
                    }
                }
            }

            TempData["Message"] = "Something went wrong";
            return View(model);
        }

        public ActionResult DeleteSTO(int id)
        {
            var user = SessionHelper.User;
            if (user != null)
            {
                var record = _sto.GetAllSTO().FirstOrDefault(x => x.Id == id && x.UserId == user.Id);
                if (record != null)
                {
                    var result = _sto.DeleteSTO(id);
                    if (result)
                    {
                        TempData["Message"] = "Record deleted successfully";
                    }
                    else
                    {
                        TempData["Message"] = "Failed to delete record";
                    }
                }
            }

            return RedirectToAction("MySTORecords");
        }


        public STO ViewToSTO(STOViewModel model)
        {
            return new STO
            {
                Id = model.Id,
                UserId = model.UserId,
                CarBrand = model.CarBrand,
                EngineVolume = model.EngineVolume,
                Year = model.Year,
                Licenseplate = model.Licenseplate,
                Vin = model.Vin,
                BranchNumber = model.BranchNumber,
                Comment = model.Comment,
                Status = model.Status,
                RegistrationData = model.RegistrationData,
                TimeOfRegistration = model.TimeOfRegistration,
            };

        }
        public STOViewModel STOToview(STO model)
        {
            return new STOViewModel
            {
                Id = model.Id,
                UserId = model.UserId,
                CarBrand = model.CarBrand,
                EngineVolume = model.EngineVolume,
                Year = model.Year,
                Licenseplate = model.Licenseplate,
                Vin = model.Vin,
                BranchNumber = model.BranchNumber,
                Comment = model.Comment,
                Status = model.Status,
                RegistrationData = model.RegistrationData,
                TimeOfRegistration = model.TimeOfRegistration,
            };
        }

    }
}