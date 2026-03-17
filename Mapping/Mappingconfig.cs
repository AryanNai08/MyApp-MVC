using AutoMapper;
using MyApp_MVC.Dtos;
using MyApp_MVC.Models;

namespace MyApp_MVC.Mapping
{
    public class Mappingconfig : Profile
    {
        
            public Mappingconfig()
            {
                // Entity → DTO
                CreateMap<Item, ItemDto>();

                // Create DTO → Entity
                CreateMap<CreateItemDto, Item>();

                // Update DTO → Entity
                CreateMap<UpdateItemDto, Item>();

            CreateMap<ItemDto, Item>();
        }
        
    }
}
