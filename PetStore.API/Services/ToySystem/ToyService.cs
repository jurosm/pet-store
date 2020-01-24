using PetStore.API.Db;
using PetStore.API.Helper.Pagination;
using PetStore.API.Models.Response;
using PetStore.API.Models.Response.Toy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.ToySystem
{
    public class ToyService
    {
        ToyRepository ToyRepository;
        public ToyService(ToyRepository toyRepository) 
        { 
            this.ToyRepository = toyRepository; 
        }

        public PageResponse<ToyUnit> GetToysPage(int pageSize, int page, string order, string match, string category)
        {
            PagedResult<Toy> toys = ToyRepository.GetToysPaged(pageSize, page, order, match, category);
            if (toys.Results != null)
            {
                List<Toy> toysList = toys.Results.ToList();
                PageResponse<ToyUnit> response = new PageResponse<ToyUnit>();

                foreach (Toy toy in toysList)
                {
                    ToyUnit toyUnit = new ToyUnit() { ToyId = toy.ToyId, Name = toy.Name, Description = toy.Description, ShortDescription = toy.ShortDescription, Price = toy.Price };
                    if(toy.Category != null)
                    {
                        toyUnit.Category = toy.Category.Name;
                    }
                    response.Items.Add(toyUnit);
                }
                response.NumberOfPages = toys.PageCount;
                return response;
            }
            else return new PageResponse<ToyUnit>() { Items = null, NumberOfPages = 0};
        }

        public async Task AddToyAsync(ToyUnit toyUnit)
        {
            await ToyRepository.AddToyAsync(toyUnit);
        }

        public async Task UpdateToyAsync(ToyUnit toyUnit)
        {
            await ToyRepository.UpdateToyAsync(toyUnit);
        }

        public ToyUnit GetToy(int id)
        {
            Toy toy = ToyRepository.GetToyById(id);
            if (toy != null)
            {
                return new ToyUnit() { ToyId = toy.ToyId, Name = toy.Name, Category = toy.Category.Name, Description = toy.Description, Price = toy.Price };
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public async Task DeleteToyAsync(int id)
        {
            await ToyRepository.DeleteAsync(id);
        }

    }
}
