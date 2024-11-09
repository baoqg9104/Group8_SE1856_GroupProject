using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public void AddAdmin(Admin admin)
        {
            AdminDAO.AddAdmin(admin);
        }

        public void DeleteAdmin(Admin admin)
        {
            AdminDAO.DeleteAdmin(admin);
        }

        public Admin GetAdmin(int id)
        {
            return AdminDAO.GetAdmin(id);
        }

        public List<Admin> GetAdmins()
        {
            return AdminDAO.GetAdmins();
        }

        public void UpdateAdmin(Admin admin)
        {
            AdminDAO.UpdateAdmin(admin);
        }
    }
}
