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

namespace PetStore.API.Services.ToySystem
{
    public class ToyRepository : Repository<Toy>
    {
        IMapper Mapper;
        public ToyRepository(ContextWrapper<Toy> context, IMapper mapper) : base(context) {
            this.Mapper = mapper;
        }

        public PagedResult<Toy> GetToysPaged(int pageSize, int page, string order, string match, string category)
        {
            if (order == "asc")
            {
                return PagedResult<Toy>.GetPaged(Context.Table.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(match.ToLower()) && (x.Category.Name.Contains(category) || x.Category == null || category == "all")).OrderBy(x => x.Price), page, pageSize);
            } 

            else if (order == "desc")
            {
                return PagedResult<Toy>.GetPaged(Context.Table.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(match.ToLower()) && (x.Category.Name.Contains(category) || x.Category == null || category == "all")).OrderByDescending(x => x.Price), page, pageSize);
            } 

            else
            {
                return PagedResult<Toy>.GetPaged(Context.Table.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(match.ToLower()) && (x.Category.Name.Contains(category) || x.Category == null || category == "all")), page, pageSize);
            }
        }

        public Toy GetToyById(int id)
        {
            return Context.Table.Include(x => x.Category).FirstOrDefault(x => x.ToyId == id);
        }

        public async Task AddToyAsync(ToyUnit toyUnit)
        {
            Toy toy = Mapper.Map<Toy>(toyUnit);
            Category category = Context.PSContext.Category.FirstOrDefault(x => x.Name == toyUnit.Category);
            if (category != null) category.Toy.Add(toy); else Context.Table.Add(toy);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateToyAsync(ToyChangeRequest toyChange)
        {
            Toy toy = Context.Table.First(x => x.ToyId == toyChange.ToyId);

            Category category = await Context.PSContext.Category.FirstOrDefaultAsync(x => x.Name == toyChange.Category);
            if(category != null) toy.Category = category;
       
            toy.Name = toyChange.Name;
            toy.Description = toyChange.Description;
            toy.ShortDescription = toyChange.Description;
            toy.Price = toyChange.Price;
            await Context.SaveChangesAsync();
        }
    }
}
