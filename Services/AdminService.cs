using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService()
        {
            adminRepository = new AdminRepository();
        }

        public void AddAdmin(Admin admin)
        {
            adminRepository.AddAdmin(admin);
        }

        public void DeleteAdmin(Admin admin)
        {
            adminRepository.DeleteAdmin(admin);
        }

        public Admin GetAdmin(int id)
        {
            return adminRepository.GetAdmin(id);
        }

        public List<Admin> GetAdmins()
        {
            return adminRepository.GetAdmins();
        }

        public void UpdateAdmin(Admin admin)
        {
            adminRepository.UpdateAdmin(admin);
        }
    }
}
