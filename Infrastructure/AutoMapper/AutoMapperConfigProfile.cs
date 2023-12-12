using AutoMapper;
using Domain.DTO.CategoryDTO;
using Domain.DTO.ItemDTO;
using Domain.DTO.PersonDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AutoMapper
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            // Items

            CreateMap<CreateItemDto, Item>();
            CreateMap<Item, GetItemDto>();
            CreateMap<UpdateItemDto, Item>();

            // Persons

            CreateMap<CreatePersonDto, Person>();
            CreateMap<Person, GetPersonDto>();
            CreateMap<UpdatePersonDto, Person>();

            // Category
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();

        }
    }
}
