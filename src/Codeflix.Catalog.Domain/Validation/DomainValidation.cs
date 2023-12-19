﻿using Codeflix.Catalog.Domain.Exceptions;

namespace Codeflix.Catalog.Domain.Validation;
public class DomainValidation
{
    public static void NotNull(object target, string fieldName)
    {
        if (target is null)
        {
            throw new EntityValidationException($"{fieldName} should not be null");
        }

    }
}
