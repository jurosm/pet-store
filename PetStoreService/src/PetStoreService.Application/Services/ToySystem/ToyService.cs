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

    public async Task<ToysResponse> GetToysPageAsync(int pageSize, int page, ToyOrder order, string match, int? category)
    {
        var dbCategories = await _categoryRepository.ReadAllAsync();

        IEnumerable<CategoryUnit> categories = dbCategories.Select(_mapper.Map<CategoryUnit>);
        ToysResponse response = new()
        {
            Categories = categories
        };

        PagedResult<Toy> toys = await _toyRepository.GetToysPaged(pageSize, page, order, match, category);

        if (toys.Results != null)
        {
            response.Items = toys.Results.Select(_mapper.Map<ToyUnit>);
            response.NumberOfPages = toys.PageCount;
            return response;
        }

        return new ToysResponse() { Items = null, NumberOfPages = 0, Categories = categories };
    }

    public Task<Toy> AddToyAsync(ToyData toyUnit)
    {
        return _toyRepository.AddToyAsync(toyUnit);
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