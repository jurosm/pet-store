using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.API.Models.Response
{
    public class ModelErrorResponse(ModelStateDictionary modelState)
    {
        public List<ValidationError> Errors { get; } = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
    }
}