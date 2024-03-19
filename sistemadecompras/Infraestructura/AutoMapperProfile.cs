using AutoMapper;
using sistemadecompras.Models;
using sistemadecompras.Models.Domain;
using sistemadecompras.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemadecompras.Infraestructura
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
           CreateMap<RegisterAccountDm, LoginDomainModel>().ReverseMap();

           CreateMap<CursosDomainModel, CursosViewModel>().ReverseMap();
        }

       

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a => {
                a.AddProfile <AutoMapperProfile > ();
            });
        }
    }
}