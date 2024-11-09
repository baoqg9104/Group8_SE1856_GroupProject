using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class AdminDAO
    {
        public static List<Admin> GetAdmins()
        {
            var list = new List<Admin>();
            try
            {
                using var context = new JobFinderContext();
                list = context.Admins.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static Admin GetAdmin(int id)
        {
            using var context = new JobFinderContext();
            return context.Admins.FirstOrDefault(a => a.AdminId == id);
        }

        public static void AddAdmin(Admin admin)
        {
            using var context = new JobFinderContext();
            context.Admins.Add(admin);
            context.SaveChanges();
        }

        public static void UpdateAdmin(Admin admin)
        {
            using var context = new JobFinderContext();
            context.Entry<Admin>(admin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteAdmin(Admin admin)
        {
            using var context = new JobFinderContext();
            context.Admins.Remove(admin);
            context.SaveChanges();
        }
    }
}
