﻿using Microsoft.Extensions.Localization;

namespace MyProject.Admin.Api.Helpers.Localization
{
    public interface IGenericControllerLocalizer<T> : IStringLocalizer<T>
    {

    }
}