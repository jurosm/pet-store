using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PetStore.API.Models.Response
{
    public class ModelErrorResponse
    {
        public List<ValidationError> Errors { get; }

        public ModelErrorResponse(ModelStateDictionary modelState)
        {
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}
