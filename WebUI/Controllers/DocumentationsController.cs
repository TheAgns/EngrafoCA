using Application.Features.Documentations.Commands.CreateDocumentation;
using Application.Features.Documentations.Queries.GetDocumentation;
using Application.Features.Documentations.Queries.GetDocumentations;
using Application.Features.DocumentationTemplates.Queries.GetDocumentationTemplate;
using Application.Features.DocumentationTemplates.Queries.GetDocumentationTemplates;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

            if (result.IsError)
            {
				var errorViewModel = new ErrorViewModel(result.FirstError.Code, result.FirstError.Description);
				TempData["ErrorViewModel"] = System.Text.Json.JsonSerializer.Serialize(errorViewModel);
				return RedirectToAction("Error", "Error");
			}

            return View(result.Value);
        }

        // Documentation Details
        [HttpGet("Documentation/Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var documentation = await _sender.Send(new GetDocumentationQuery { Id = id });

            /*! Checks if the handler returned an error
                and creates an ErrorViewModel
                and redirects it to the ErrorController
            */

            if (documentation.IsError)
            {
				var errorViewModel = new ErrorViewModel(documentation.FirstError.Code, documentation.FirstError.Description);
				TempData["ErrorViewModel"] = System.Text.Json.JsonSerializer.Serialize(errorViewModel);
				return RedirectToAction("Error", "Error");
            }

            //TODO: REFACTOR

            var templateId = documentation.Value.DocumentationTemplateId;
            var template = await _sender.Send(new GetDocumentationTemplateQuery { Id = templateId});

            if (template.IsError)
            {
				var errorViewModel = new ErrorViewModel(template.FirstError.Code, template.FirstError.Description);
				TempData["ErrorViewModel"] = System.Text.Json.JsonSerializer.Serialize(errorViewModel);
				return RedirectToAction("Error", "Error");
			}

            var vm = new DocumentationDetailsViewModel
            {
                Documentation = documentation.Value,
                DocumentationTemplateHeadings = template.Value.DocumentationTemplateHeadings
            };

            return View(vm);
        }

        // Create Documentation Page
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var templates = await _sender.Send(new GetDocumentationTemplatesQuery());

            if (templates.IsError)
            {
				var errorViewModel = new ErrorViewModel(templates.FirstError.Code, templates.FirstError.Description);
				TempData["ErrorViewModel"] = System.Text.Json.JsonSerializer.Serialize(errorViewModel);
				return RedirectToAction("Error", "Error");
			}

            var vm = new CreateDocumentationViewModel
            {
                DocumentationTemplates = templates.Value
            };

            return View(vm);
        }

        // When submitting the form data to create a new documentation
        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentationCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsError)
            {
				var errorViewModel = new ErrorViewModel(result.FirstError.Code, result.FirstError.Description);
				TempData["ErrorViewModel"] = System.Text.Json.JsonSerializer.Serialize(errorViewModel);
				return RedirectToAction("Error", "Error");
			}

            return RedirectToAction("Details", new { Id = result.Value });
        }

        // Method for generating the headings from the selected template
        [HttpGet]
        public async Task<IActionResult> GetTemplateHeadings(Guid templateId)
        {
            var template = await _sender.Send(new GetDocumentationTemplateQuery() { Id = templateId});

            if (template.IsError)
            {
				var errorViewModel = new ErrorViewModel(template.FirstError.Code, template.FirstError.Description);
				TempData["ErrorViewModel"] = System.Text.Json.JsonSerializer.Serialize(errorViewModel);
				return RedirectToAction("Error", "Error");
			}

            return Json(template.Value.DocumentationTemplateHeadings);
        }



    }
}
