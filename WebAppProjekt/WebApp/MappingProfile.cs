using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Osoba, OsobaRest>().ReverseMap();
            CreateMap<Job, JobRest>().ReverseMap();
        }
    }
}