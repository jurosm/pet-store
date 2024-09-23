using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetStoreService.Application.Helper.Pagination;
using PetStoreService.Application.Models.Request.Toy;
using PetStoreService.Domain.Entities;
using PetStoreService.Persistence;

namespace PetStoreService.Application.Services.ToySystem;

public enum ToyOrder
{
    None = 0,
    Ascending = 1,
    Descending = 2
}

public class ToyRepository(PetStoreDBContext context, IMapper mapper) : Repository<Toy>(context)
{
    private readonly IMapper _mapper = mapper;

    public Task<PagedResult<Toy>> GetToysPaged(int pageSize, int page, ToyOrder order, string match, int? category)
    {
        IQueryable<Toy> baseQuery = Table.Include(x => x.Category);

        if (!string.IsNullOrEmpty(match))
        {
            baseQuery = baseQuery.Where(x => EF.Functions.ILike(x.Name, match));
        }

        if (category.HasValue)
        {
            baseQuery = baseQuery.Where(x => x.CategoryId == category);
        }

        if (order == ToyOrder.Ascending)
        {
            return PagedResult<Toy>.GetPaged(baseQuery.OrderBy(x => x.Price), page, pageSize);
        }

        else if (order == ToyOrder.Descending)
        {
            return PagedResult<Toy>.GetPaged(baseQuery.OrderByDescending(x => x.Price), page, pageSize);
        }

        else
        {
            return PagedResult<Toy>.GetPaged(baseQuery, page, pageSize);
        }
    }

    public Task<Toy?> GetToyById(int id)
    {
        return Table.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Toy> AddToyAsync(ToyData toyUnit)
    {
        Toy toy = _mapper.Map<Toy>(toyUnit);
        Category? category = await Context.Category.FirstOrDefaultAsync(x => x.Id == toyUnit.CategoryId);
        if (category != null) category.Toy.Add(toy);
        else
        {
            toy.CategoryId = null;
            await Table.AddAsync(toy);
        }
        await Context.SaveChangesAsync();

        return toy;
    }

    public async Task<Toy> UpdateToyAsync(ToyData toyData, int id)
    {
        Toy toy = _mapper.Map<Toy>(toyData);
        toy.Id = id;

        if (toyData.CategoryId != null)
        {
            Category? category = await Context.Category.FirstOrDefaultAsync(x => x.Id == toyData.CategoryId);
            if (category == null) { toy.CategoryId = null; }
        }
        else toy.CategoryId = null;

        Table.Update(toy);
        await Context.SaveChangesAsync();

        return toy;
    }
}