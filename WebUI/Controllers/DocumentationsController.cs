using System.Diagnostics;
using Application.Features.Documentations.Commands.CreateDocumentation;
using Application.Features.Documentations.Queries.GetDocumentation;
using Application.Features.Documentations.Queries.GetDocumentations;
using Application.Features.DocumentationTemplates.Queries.GetDocumentationTemplates;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class DocumentationsController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public DocumentationsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        // Overview of all Documentations
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _sender.Send(new GetDocumentationsQuery());

            return View(result);
        }

        // Documentation Details
        [HttpGet("Documentation/Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var documentation = await _sender.Send(new GetDocumentationQuery { Id = id });
            var templateId = documentation.DocumentationTemplateId; // Assuming you have access to the template ID
            var template = await _sender.Send(new GetDocumentationTemplatesQuery());
            var selectedTemplate = template.FirstOrDefault(t => t.Id == templateId);

            var vm = new DocumentationDetailsViewModel
            {
                Documentation = documentation,
                DocumentationTemplateHeadings = selectedTemplate.DocumentationTemplateHeadings
            };

            return View(vm);
        }

        // Create Documentation Page
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var templates = await _sender.Send(new GetDocumentationTemplatesQuery());
            var vm = new CreateDocumentationViewModel
            {
                DocumentationTemplates = templates
            };

            return View(vm);
        }

        // When submitting the form data to create a new documentation
        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentationCommand command)
        {

			var result = await _sender.Send(command);

			return RedirectToAction("Details", new { Id = result });
		}

        // Method for generating the headings from the selected template
        [HttpGet]
        public async Task<IActionResult> GetTemplateHeadings(Guid templateId)
        {
            var template = await _sender.Send(new GetDocumentationTemplatesQuery());
            var selectedTemplate = template.FirstOrDefault(t => t.Id == templateId);
            if (selectedTemplate == null)
            {
                return NotFound();
            }

            return Json(selectedTemplate.DocumentationTemplateHeadings);
        }


        
    }
}
