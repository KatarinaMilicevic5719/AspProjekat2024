using FluentValidation;
using System;
using System.Collections.Generic;

namespace Asp.API.Extensions
{
    public static class PictureValidatorExtension
    {
        private static readonly List<string>  SupportedExtensions=new List<string> { ".jpg", ".png", ".txt" };
        public static void ValidateExtension(this string extension)
        {
            if (!SupportedExtensions.Contains(extension))
            {
                throw new ValidationException("Invalid file extension. They must be .png or .jpeg or .jpg, .giff");
            }
        }
    }
}
