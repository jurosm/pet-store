using AutoMapper;
using PetStoreService.Application.Helper.Pagination;
using PetStoreService.Application.Models.Request.Toy;
using PetStoreService.Application.Models.Response.Category;
using PetStoreService.Application.Models.Response.Toy;
using PetStoreService.Application.Services.CategorySystem;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.ToySystem;

public class ToyService(ToyRepository toyRepository, IMapper mapper, CategoryRepository categoryRepository)
{
    private readonly ToyRepository _toyRepository = toyRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ToysResponse> GetToysPageAsync(int limit, int offset, ToyOrder order, string match, int? category)
    {
        PagedResult<Toy> toys = await _toyRepository.GetToysPaged(limit, offset, order, match, category);

        return new ToysResponse
        {
            Items = toys.Results!.Select(_mapper.Map<ToyUnit>),
            Offset = toys.Offset,
            Limit = toys.Limit,
            Total = toys.Total
        };
    }

    public async Task<ToyResponse> AddToyAsync(ToyData toyUnit)
    {
        var toy = await _toyRepository.AddToyAsync(toyUnit);
        return _mapper.Map<ToyResponse>(toy);
    }

    public Task<Toy> UpdateToyAsync(ToyData toy, int id)
    {
        return _toyRepository.UpdateToyAsync(toy, id);
    }

    public async Task<ToyResponse> GetToyAsync(int id)
    {
        Toy? toy = await _toyRepository.GetToyById(id);
        return toy != null ? _mapper.Map<ToyResponse>(toy) : throw new FileNotFoundException();
    }

    public async Task DeleteToyAsync(int id)
    {
        await _toyRepository.DeleteAsync(id);
    }
}