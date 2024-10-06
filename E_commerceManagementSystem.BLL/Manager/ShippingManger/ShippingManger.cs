using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_commerceManagementSystem.BLL.Dto.ShippingDto;
using E_commerceManagementSystem.BLL.Manager.GeneralManager;
using E_commerceManagementSystem.DAL.Data.Models;
using E_commerceManagementSystem.DAL.Reposatories.ProductRepository;
using E_commerceManagementSystem.DAL.Reposatories.ShippingRepository;

namespace E_commerceManagementSystem.BLL.Manager.ShippingManger
{
    public class ShippingManger:Manager<Shipping,AddShippingDto,UpdateShippingDto>,IShippingManger
    {
        
   
    public ShippingManger(IShippingRepo rpository, IMapper mapper)
       
   
        : base(rpository, mapper) { }
    
}}
