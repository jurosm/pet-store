using AutoMapper;
using PetStore.API.Db;
using PetStore.API.Helper.Pagination;
using PetStore.API.Models.Response;
using PetStore.API.Models.Response.Toy;
using PetStore.API.Services.CategorySystem;
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
        CategoryRepository CategoryRepository;
        IMapper Mapper;
        public ToyService(ToyRepository toyRepository, IMapper mapper, CategoryRepository categoryRepository) 
        { 
            this.ToyRepository = toyRepository;
            this.CategoryRepository = categoryRepository;
            this.Mapper = mapper;
        }

        public ToysResponse GetToysPage(int pageSize, int page, string order, string match, string category)
        {
            string[] categories = CategoryRepository.ReadAll().Select(x => x.Name).ToArray();
            ToysResponse response = new ToysResponse();
            response.Categories = categories;

            PagedResult<Toy> toys = ToyRepository.GetToysPaged(pageSize, page, order, match, category);

            if (toys.Results != null)
            {
                response.Items = toys.Results.Select( x => { return Mapper.Map<ToyUnit>(x); });
                response.NumberOfPages = toys.PageCount;
                return response;
            }

            return new ToysResponse() { Items = null, NumberOfPages = 0, Categories = categories};
        }

        public async Task AddToyAsync(ToyUnit toyUnit)
        {
            await ToyRepository.AddToyAsync(toyUnit);
        }

        public async Task UpdateToyAsync(ToyChangeRequest toy)
        {
            await ToyRepository.UpdateToyAsync(toy);
        }

        public ToyResponse GetToy(int id)
        {
            Toy toy = ToyRepository.GetToyById(id);
            if (toy != null)
            {
                return Mapper.Map<ToyResponse>(toy);
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
