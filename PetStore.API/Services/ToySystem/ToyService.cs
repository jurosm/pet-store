using AutoMapper;
using PetStore.API.Db;
using PetStore.API.Helper.Pagination;
using PetStore.API.Models.Request.Toy;
using PetStore.API.Models.Response.Category;
using PetStore.API.Models.Response.Toy;
using PetStore.API.Services.CategorySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.ToySystem
{
    public class ToyService(ToyRepository toyRepository, IMapper mapper, CategoryRepository categoryRepository)
    {
        readonly ToyRepository ToyRepository = toyRepository;
        readonly CategoryRepository CategoryRepository = categoryRepository;
        readonly IMapper Mapper = mapper;

        public ToysResponse GetToysPage(int pageSize, int page, int order, string match, int category)
        {

Console.WriteLine("lkajfkldjjf");
            IEnumerable<CategoryUnit> categories = CategoryRepository.ReadAll().Select(x => Mapper.Map<CategoryUnit>(x));
            ToysResponse response = new()
            {
                Categories = categories
            };

            PagedResult<Toy> toys = ToyRepository.GetToysPaged(pageSize, page, order, match, category);

            if (toys.Results != null)
            {
                response.Items = toys.Results.Select(Mapper.Map<ToyUnit>);
                response.NumberOfPages = toys.PageCount;
                return response;
            }

            return new ToysResponse() { Items = null, NumberOfPages = 0, Categories = categories };
        }

        public async Task AddToyAsync(ToyData toyUnit)
        {
            await ToyRepository.AddToyAsync(toyUnit);
        }

        public async Task UpdateToyAsync(ToyData toy, int id)
        {
            await ToyRepository.UpdateToyAsync(toy, id);
        }

        public ToyResponse GetToy(int id)
        {
            Toy toy = ToyRepository.GetToyById(id);
            return toy != null ? Mapper.Map<ToyResponse>(toy) : throw new FileNotFoundException();
        }

        public async Task DeleteToyAsync(int id)
        {
            await ToyRepository.DeleteAsync(id);
        }
    }
}
