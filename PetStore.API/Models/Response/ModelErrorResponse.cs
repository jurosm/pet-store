using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PetStore.API.Models.Response
{
    public class ModelErrorResponse(ModelStateDictionary modelState)
    {
        public List<ValidationError> Errors { get; } = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
    }
}
