﻿using Codeflix.Catalog.Domain.Exceptions;

namespace Codeflix.Catalog.Domain.Validation;
public class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target is null)
        {
            throw new EntityValidationException($"{fieldName} should not be null");
        }

    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{fieldName} should not be null or empty");

    }

    public static void MinLength(string target, int minLenght, string fieldName)
    {
        if (target.Length < minLenght)
            throw new EntityValidationException($"{fieldName} should not be less than {minLenght} characters long.");
        
    }


}
