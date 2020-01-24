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

namespace PetStore.API.Services.ToySystem
{
    public class ToyRepository : Repository<Toy>
    {
        public ToyRepository(ContextWrapper<Toy> context) : base(context) { }

        public PagedResult<Toy> GetToysPaged(int pageSize, int page, string order, string match, string category)
        {
            if (order == "asc")
            {
                return PagedResult<Toy>.GetPaged(Context.Table.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(match.ToLower()) && (x.Category.Name.Contains(category) || x.Category == null)).OrderBy(x => x.Price), page, pageSize);
            }
            else
            {
                return PagedResult<Toy>.GetPaged(Context.Table.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(match.ToLower()) && (x.Category.Name.Contains(category) || x.Category == null)).OrderByDescending(x => x.Price), page, pageSize);
            }
        }

        public Toy GetToyById(int id)
        {
            return Context.Table.Include(x => x.Category).FirstOrDefault(x => x.ToyId == id);
        }

        public async Task AddToyAsync(ToyUnit toyUnit)
        {
            Toy toy = new Toy() { Name = toyUnit.Name, Description = toyUnit.Description,ShortDescription = toyUnit.ShortDescription ,Price = toyUnit.Price };
            try
            {
                Category category = await Context.PSContext.Category.FirstAsync(x => x.Name == toyUnit.Category);
                category.Toy.Add(toy);
            }
            catch (Exception e) {
                Context.Table.Add(toy);
            }
            await Context.SaveChangesAsync();
        }

        public async Task UpdateToyAsync(ToyUnit toyUnit)
        {
            Toy toy = Context.Table.First(x => x.ToyId == toyUnit.ToyId);
            try
            {
                Category category = await Context.PSContext.Category.FirstAsync(x => x.Name == toyUnit.Category);
                toy.Category = category;
            }
            catch (Exception e) { 
            
            }
            finally
            {
                toy.Name = toyUnit.Name;
                toy.Description = toyUnit.Description;
                toy.ShortDescription = toyUnit.Description;
                toy.Price = toyUnit.Price;
                await Context.SaveChangesAsync();
            }
        }
    }
}
