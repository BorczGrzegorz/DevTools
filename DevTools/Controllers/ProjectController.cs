using DevTools.Application.Abstract;
using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Controllers
{
    [ApiVersionNeutral]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProjectQuery _projectsQuery;

        public ProjectController(IProductService productService,
                                 IProjectQuery projectsQuery)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _projectsQuery = projectsQuery ?? throw new ArgumentNullException(nameof(projectsQuery));
        }

        [HttpPut("product/{productId}/projects")]
        public ActionResult<ProjectDto[]> AddProjects([FromRoute]ProductId productId, [FromBody]string[] names)
            => _productService.AddProjects(productId, names);

        [HttpGet("projects/{projectId}")]
        public ActionResult<ProjectDto> GetProject([FromRoute]ProjectId projectId)
            => _projectsQuery.GetProject(projectId);

        [HttpDelete("projects/{projectId}")]
        public ActionResult<ProjectDto> DeleteProject([FromRoute]ProjectId projectId)
            => _productService.RemoveProject(projectId);

        [HttpGet("product/{productId}/projects")]
        public ActionResult<ProjectDto[]> GetProjects([FromRoute]ProductId productId)
         => _projectsQuery.GetProjects(productId);

    }
}
