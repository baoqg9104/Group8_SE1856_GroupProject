using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAdminService
    {
        List<Admin> GetAdmins();
        Admin GetAdmin(int id);
        void AddAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(Admin admin);
    }
}
