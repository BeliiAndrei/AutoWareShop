using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.STO;

namespace WebShop.BusinessLogic.BLogic
{
    public class STOBL : STOApi, ISTO
    {
        public bool CreateSTO(STO newSTO)
        {
            var stoDb = MapToSTODBTab(newSTO);
            try
            {
                CreateSTOApi(stoDb);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }
        public bool UpdateSTO(STO updatedSTO)
        {
            var stoDb = MapToSTODBTab(updatedSTO);
            try
            {
                UpdateSTOApi(stoDb);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }
        public bool DeleteSTO(int id)
        {
            try
            {
                DeleteSTOApi(id);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }
        public List<STO> GetAllSTO()
        {
            try
            {
                var stoDb =  GetAllSTOApi();
                return stoDb.Select(MapTOSTO).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                return new List<STO>();
            }
        }
        public STO GetSTOById(int id)
        {
            // Implementation for getting a single STO by ID can be added here.
            throw new NotImplementedException();
        }
        public STO MapTOSTO(STODBTab db)
        {
            return new STO
            {
                BranchNumber = db.BranchNumber,
                Id = db.Id,
                UserId = db.UserId,
                CarBrand = db.CarBrand,
                Model = db.Model,
                EngineVolume = db.EngineVolume,
                Year = db.Year,
                Comment = db.Comment,
                Status = db.Status,
                Licenseplate = db.Licenseplate,
                Vin = db.Vin,
                RegistrationData = db.RegistrationData,
                TimeOfRegistration = db.TimeOfRegistration
            };
        }
        public STODBTab MapToSTODBTab(STO sto)
        {
            return new STODBTab
            {
                BranchNumber = sto.BranchNumber,
                Id = sto.Id,
                UserId = sto.UserId,
                CarBrand = sto.CarBrand,
                Model = sto.Model,
                EngineVolume = sto.EngineVolume,
                Year = sto.Year,
                Comment = sto.Comment,
                Status = sto.Status,
                Licenseplate = sto.Licenseplate,
                Vin = sto.Vin,
                RegistrationData = sto.RegistrationData,
                TimeOfRegistration = sto.TimeOfRegistration
            };
        }
    }

}

