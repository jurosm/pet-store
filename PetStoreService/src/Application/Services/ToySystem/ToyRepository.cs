using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetStoreService.Application.Helper.Pagination;
using PetStoreService.Application.Models.Request.Toy;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.ToySystem;

public enum ToyOrder
{
    None = 0,
    Ascending = 1,
    Descending = 2
}

public class ToyRepository(ContextWrapper<Toy> context, IMapper mapper) : Repository<Toy>(context)
{
    private readonly IMapper _mapper = mapper;

    public PagedResult<Toy> GetToysPaged(int pageSize, int page, ToyOrder order, string match, int? category)
    {
        IQueryable<Toy> baseQuery = Context.Table.Include(x => x.Category);

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

    public Toy GetToyById(int id)
    {
        return Context.Table.Include(x => x.Category).FirstOrDefault(x => x.ToyId == id);
    }

    public async Task AddToyAsync(ToyData toyUnit)
    {
        Toy toy = _mapper.Map<Toy>(toyUnit);
        Category category = Context.PSContext.Category.FirstOrDefault(x => x.CategoryId == toyUnit.CategoryId);
        if (category != null) category.Toy.Add(toy);
        else
        {
            toy.CategoryId = null;
            Context.Table.Add(toy);
        }
        await Context.SaveChangesAsync();
    }

    public async Task UpdateToyAsync(ToyData toyData, int id)
    {
        Toy toy = _mapper.Map<Toy>(toyData);
        toy.ToyId = id;

        if (toyData.CategoryId != null)
        {
            Category category = await Context.PSContext.Category.FirstOrDefaultAsync(x => x.CategoryId == toyData.CategoryId);
            if (category == null) { toy.CategoryId = null; }
        }
        else toy.CategoryId = null;

        Context.Table.Update(toy);
        await Context.SaveChangesAsync();
    }
}
