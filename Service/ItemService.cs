using AutoMapper;
using MyApp_MVC.Dtos;
using MyApp_MVC.Models;
using MyApp_MVC.Repository;

namespace MyApp_MVC.Service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public ItemService(IItemRepository itemRepository,IMapper mapper) 
        {
            _itemRepository = itemRepository;
            _mapper=mapper;
        }

        public async Task<ItemDto> CreateItemAsync(CreateItemDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new Exception("Name is required");
            var newitem=_mapper.Map<Item>(dto);
            await _itemRepository.CreateItem(newitem);
            return _mapper.Map<ItemDto>(newitem);
        }

        public async Task<bool> DeleteItem(int id)
        {
           //var item=await _itemRepository.Getitembyid(id);
           var item=await _itemRepository.Getitembyid(id);
            if(item == null)
            {
                throw new Exception($"Item not found with id:{id}");
            }
           await _itemRepository.DeleteItem(id);
            return true;
        }

        public async Task<ItemDto> GetItemById(int id)
        {
            var item = await _itemRepository.Getitembyid(id);
            if(item == null)
            {
                throw new Exception($"No record found for id:{id}!");
            }
            return _mapper.Map<ItemDto>(item);
        }


        public async Task<List<ItemDto>> GetItemsAsync()
        {
            var items= await _itemRepository.ReadItems();
            return _mapper.Map<List<ItemDto>>(items);
        }

        public async Task<bool> UpdateItem(int id, ItemDto dto)
        {
            if (id == 0)
            {
                throw new Exception("Id must be greater than 0");
            }
            var item =await _itemRepository.Getitembyid(id);

            if(item == null)
            {
                throw new Exception("NO item found with id{id}");
            }
            _mapper.Map(dto,item);
            await _itemRepository.UpdateItem(item);
            return true;
        }
    }
}
