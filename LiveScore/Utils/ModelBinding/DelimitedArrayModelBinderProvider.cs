using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace LiveScore.Utils.ModelBinding
{
    public class DelimitedArrayModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.IsEnumerableType && !context.Metadata.ElementMetadata.IsComplexType)
            {
                return new DelimitedArrayModelBinder();
            }

            return null;
        }
    }
}
