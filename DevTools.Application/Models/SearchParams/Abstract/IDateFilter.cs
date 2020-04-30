using System;

namespace DevTools.Application.Models.SearchParams.Abstract
{
    public interface IDateFilter
    {
        DateTime? StartFrom { get; }
    }
}
