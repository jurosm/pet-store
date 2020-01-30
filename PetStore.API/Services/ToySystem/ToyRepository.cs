using PetStore.API.Db;
using PetStore.API.Services.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetStore.API.Helper.Pagination;
using Microsoft.EntityFrameworkCore;
using PetStore.API.Models.Response.Toy;
using System.IO;
using AutoMapper;
using PetStore.API.Models.Request.Toy;

namespace PetStore.API.Services.ToySystem
{
    public class ToyRepository : Repository<Toy>
    {
        IMapper Mapper;
        public ToyRepository(ContextWrapper<Toy> context, IMapper mapper) : base(context)
        {
            this.Mapper = mapper;
        }

        public PagedResult<Toy> GetToysPaged(int pageSize, int page, int order, string match, int category)
        {
            if (order == 1)
            {
                return PagedResult<Toy>.GetPaged(Context.Table.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(match.ToLower()) && (x.CategoryId == category || x.Category == null || category <= 0)).OrderBy(x => x.Price), page, pageSize);
            }

            else if (order == 2)
            {
                return PagedResult<Toy>.GetPaged(Context.Table.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(match.ToLower()) && (x.CategoryId == category || x.Category == null || category <= 0)).OrderByDescending(x => x.Price), page, pageSize);
            }

            else
            {
                return PagedResult<Toy>.GetPaged(Context.Table.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(match.ToLower()) && (x.CategoryId == category || x.Category == null || category <= 0)), page, pageSize);
            }
        }

        public Toy GetToyById(int id)
        {
            return Context.Table.Include(x => x.Category).FirstOrDefault(x => x.ToyId == id);
        }

        public async Task AddToyAsync(ToyData toyUnit)
        {
            Toy toy = Mapper.Map<Toy>(toyUnit);
            Category category = Context.PSContext.Category.FirstOrDefault(x => x.CategoryId == toyUnit.CategoryId);
            if (category != null) category.Toy.Add(toy); else Context.Table.Add(toy);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateToyAsync(ToyData toyData, int id)
        {
            Toy toy = Mapper.Map<Toy>(toyData);
            toy.ToyId = id;

            if (toyData.CategoryId != null)
            {
                Category category = await Context.PSContext.Category.FirstOrDefaultAsync(x => x.CategoryId == toyData.CategoryId);
                if (category == null) { toy.CategoryId = null; }
            }

            Context.Table.Update(toy);
            await Context.SaveChangesAsync();
        }
    }
}
