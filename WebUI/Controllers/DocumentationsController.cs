using Application.Features.Documentations.Commands.CreateDocumentation;
using Application.Features.Documentations.Queries.GetDocumentation;
using Application.Features.Documentations.Queries.GetDocumentations;
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

        [HttpPost]
        //! Retrieve the userId from the active user
        public async Task<IActionResult> Create(CreateDocumentationCommand command)
        {
            var result = await _sender.Send(command);

            return RedirectToAction("Test");
            //return await Details(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _sender.Send(new GetDocumentationQuery { Id = id});

            return View(result);
        }

        
    }
}
