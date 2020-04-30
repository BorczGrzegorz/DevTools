using DevTools.Binders;
using DevTools.Infrastructure.Models;
using Extensions.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Extensions.Binders
{
    public class IdBinderProvider : IModelBinderProvider
    {
        private Dictionary<Type, IModelBinder> _binders = new Dictionary<Type, IModelBinder>
        {
            { typeof(MachineId), new MachineIdBinder() },
            { typeof(ProductId), new ProductIdBinder() },
            { typeof(ProjectId), new ProjectIdBinder() },
            { typeof(AddressId), new AddressIdBinder() }
        };

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            _binders.TryGetValue(context.Metadata.ModelType, out IModelBinder binder);
            return binder;
        }
    }
}
