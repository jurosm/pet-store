using AutoMapper;
using PetStoreService.Application.Models.Request.Category;
using PetStoreService.Application.Models.Response.Category;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.CategorySystem;

public class CategoryService(CategoryRepository categoryRepository, IMapper mapper)
{
    private readonly IMapper _mapper = mapper;
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<CategoryUnit>> GetAllAsync()
    {
        return (await _categoryRepository.ReadAllAsync()).Select(_mapper.Map<CategoryUnit>);
    }

    public Task<Category> AddAsync(CategoryUpdateRequest name)
    {
        return _categoryRepository.CreateAsync(new Category() { Name = name.Name });
    }

    public async Task DeleteAsync(int id)
    {
        await _categoryRepository.DeleteAsync(id);
    }

    public Task<Category> EditAsync(int id, CategoryUpdateRequest request)
    {
        return _categoryRepository.UpdateAsync(new Category() { Name = request.Name, Id = id });
    }
}