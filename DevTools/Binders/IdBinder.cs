using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Extensions.Binders
{
    public abstract class IdBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult =
                bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            string value = valueProviderResult.FirstValue;
            if (!Guid.TryParse(value, out Guid id))
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Id must be Guid");
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(Parse(id));
            return Task.CompletedTask;
        }

        protected abstract T Parse(Guid id);
    }
}
