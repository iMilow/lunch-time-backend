using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchBackend.Configs.Profiles;

namespace LunchBackend.Configs
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
               cfg.AddProfile<DeliveryProfile>();
               cfg.AddProfile<OrderProfile>();
            });
        }
    }
}
