using AutoMapper;
using ClockRestoration.Entities;
using ClockRestoration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockRestoration.BusinessLogic.Mappers
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Brand, BrandViewItem>();
                cfg.CreateMap<Delivery, DeliveryViewItem>();
                cfg.CreateMap<Payment, PaymentViewItem>();
                cfg.CreateMap<ClockType, ClockTypeViewItem>();
                cfg.CreateMap<RequestOrderView, Order>();
            });
        }
    }
}
