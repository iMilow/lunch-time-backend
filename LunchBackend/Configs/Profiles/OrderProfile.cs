using AutoMapper;
using LunchBackend.DbAccess.Models.Entities;
using LunchBackend.Models.Requests;
using LunchBackend.Models.Responses;
using System.Collections;
using System.Collections.Generic;

namespace LunchBackend.Configs.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderRequest, Order>()
                .ForMember(v => v.Deliver, opt => opt.Ignore());

            CreateMap<string, double>().ConvertUsing(new DoubleTypeConverter());
            
            CreateMap<Order, OrderResponse>();

            CreateMap<ICollection<OrderRequest>, ICollection<Order>>();
        }
    }
    
    public class DoubleTypeConverter : ITypeConverter<string, double> {
        public double Convert(string source, double destination, ResolutionContext context)
        {
            if (double.TryParse(source, out double result))
            {
                return result;
            }

            return -1;
        }
    }
}
