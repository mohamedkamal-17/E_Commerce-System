using E_commerceManagementSystem.DAL.Data.Dphelper;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.GeneralRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceManagementSystem.DAL.Reposatories.ShippingRepository
{
    public class ShippingRepo:Repository<Shipping>
    {
        private readonly ApplicationDbContext context;

        public ShippingRepo(ApplicationDbContext Context):base(Context)
        {
            context = Context;
        }
        
            
        }

    }

