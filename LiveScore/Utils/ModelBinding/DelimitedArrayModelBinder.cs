﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LiveScore.Utils.ModelBinding
{
    /// <summary>
    /// Model binder that adds support for coma-separated parameter values in requests.
    /// </summary>
    public class DelimitedArrayModelBinder : IModelBinder
    {
        /// <summary>
        /// This method binds coma-separated parameter values to typed array.
        /// </summary>
        /// <param name="bindingContext">Model binding context</param>
        /// <returns>Successfully completed task</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            var values = valueProviderResult
                .ToString()
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var elementType = bindingContext.ModelType.GetElementType();

            if (values.Length == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(Array.CreateInstance(elementType, 0));
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(elementType);
                var typedArray = Array.CreateInstance(elementType, values.Length);

                try
                {
                    for (int i = 0; i < values.Length; ++i)
                    {
                        var value = values[i];
                        var convertedValue = converter.ConvertFromString(value);
                        typedArray.SetValue(convertedValue, i);
                    }
                }
                catch (Exception exception)
                {
                    bindingContext.ModelState.TryAddModelError(
                        modelName,
                        exception,
                        bindingContext.ModelMetadata);
                }

                bindingContext.Result = ModelBindingResult.Success(typedArray);
            }

            return Task.CompletedTask;
        }
    }
}
