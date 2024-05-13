using Application.Features.Documentation.Queries.GetDocumentation;
using Application.Features.Documentation.Queries.GetDocumentations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class DocumentationsController : Controller
    {
        private readonly ISender _sender;

        public DocumentationsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _sender.Send(new GetDocumentationsQuery());


            return View(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Documentation(Guid id)
        {
            var result = await _sender.Send(new GetDocumentationQuery { Id = id});

            return View(result);
        }

        
    }
}
