using System.Collections.Generic;
using AutoMapper;
using LunchBackend.DbAccess.Models.Entities;
using LunchBackend.Models.Requests;
using LunchBackend.Models.Responses;

namespace LunchBackend.Configs.Profiles
{
    public class DeliveryProfile: Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Delivery, DeliveryResponse>();
            CreateMap<DeliveryRequest, Delivery>();
            CreateMap<ICollection<DeliveryRequest>, ICollection<Delivery>>();
        }
    }
}
