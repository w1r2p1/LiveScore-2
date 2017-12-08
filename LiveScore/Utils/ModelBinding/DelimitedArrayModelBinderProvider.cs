using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace LiveScore.Utils.ModelBinding
{
    /// <summary>
    /// Model binder provider for coma-separated parameter values.
    /// </summary>
    public class DelimitedArrayModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// This method returns <see cref="DelimitedArrayModelBinder"/> for value type collection parameters.
        /// </summary>
        /// <param name="context">Model binder provider context</param>
        /// <returns><see cref="DelimitedArrayModelBinder"/></returns>
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
