using AutoMapper;
using PetStoreService.Application.Models.Request.Category;
using PetStoreService.Application.Models.Response.Category;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.CategorySystem;

public class CategoryService(CategoryRepository categoryRepository, IMapper mapper)
{
    private readonly IMapper _mapper = mapper;
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public IEnumerable<CategoryUnit> GetAll()
    {
        return _categoryRepository.ReadAll().Select(_mapper.Map<CategoryUnit>);
    }

    public async Task AddAsync(CategoryUpdateRequest name)
    {
        await _categoryRepository.CreateAsync(new Category() { Name = name.Name });
    }

    public async Task DeleteAsync(int id)
    {
        await _categoryRepository.DeleteAsync(id);
    }

    public async Task EditAsync(int id, CategoryUpdateRequest request)
    {
        await _categoryRepository.UpdateAsync(new Category() { Name = request.Name, CategoryId = id });
    }
}