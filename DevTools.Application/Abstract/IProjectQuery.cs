using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Application.Abstract
{
    public interface IProjectQuery
    {
        ProjectDto GetProject(ProjectId projectId);
        ProjectDto[] GetProjects(ProductId productId);
    }
}
