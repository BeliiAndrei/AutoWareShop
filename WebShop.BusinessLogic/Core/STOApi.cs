using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.STO;

namespace WebShop.BusinessLogic.Core
{
    public class STOApi
    {
        public void CreateSTOApi(STODBTab neSTO)
        {
            using (var context = new STOContext())
            {
                context.STO.Add(neSTO);
                context.SaveChanges();
            }
        }
        public void DeleteSTOApi(int id)
        {
            using (var context = new STOContext())
            {
                var stoToDelete = context.STO.FirstOrDefault(s => s.Id == id);
                if (stoToDelete != null)
                {
                    context.STO.Remove(stoToDelete);
                    context.SaveChanges();
                }
            }
        }
        public void UpdateSTOApi(STODBTab updatedSTO)
        {
            using (var context = new STOContext())
            {
                var stoToUpdate = context.STO.FirstOrDefault(s => s.Id == updatedSTO.Id);
                if (stoToUpdate != null)
                {
                    stoToUpdate.BranchNumber = updatedSTO.BranchNumber;
                    stoToUpdate.UserId = updatedSTO.UserId;
                    stoToUpdate.CarBrand = updatedSTO.CarBrand;
                    stoToUpdate.Model = updatedSTO.Model;
                    stoToUpdate.EngineVolume = updatedSTO.EngineVolume;
                    stoToUpdate.Year = updatedSTO.Year;
                    stoToUpdate.Comment = updatedSTO.Comment;
                    stoToUpdate.Status = updatedSTO.Status;
                    stoToUpdate.Licenseplate = updatedSTO.Licenseplate;
                    stoToUpdate.Vin = updatedSTO.Vin;
                    
                    stoToUpdate.RegistrationData = updatedSTO.RegistrationData;
                    stoToUpdate.TimeOfRegistration = updatedSTO.TimeOfRegistration;
                    context.SaveChanges();
                }
            }
        }
        public List<STODBTab> GetAllSTOApi()
        {
            using (var context = new STOContext())
            {
                return context.STO.ToList();
            }
        }
        public STODBTab GetSTOByIdApi(int id)
        {
            using (var context = new STOContext())
            {
                return context.STO.FirstOrDefault(s => s.Id == id);
            }
        }
    }
}
