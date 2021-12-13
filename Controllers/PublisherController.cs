using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using My_Book.ActionResult;
using My_Book.Data.Models;
using My_Book.Data.Models.ViewModels;
using My_Book.Data.Services;
using My_Book.Exceptions;
using System;
using System.Collections.Generic;

namespace My_Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        public PublishersService _publishersService;
        public readonly ILogger<PublisherController> _logger;
        public PublisherController(PublishersService publishersService, ILogger<PublisherController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }


        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchStr, int pageNumber)
        {
            //throw new Exception("Exception to sql database from getallpublisher()");
            try
            {
                _logger.LogInformation("Info in GetAllPublishers()");
                var _result = _publishersService.GetAllPulishers(sortBy, searchStr, pageNumber);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest("Couldn't load all publishers");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPubliher([FromBody] PublisherVM author)
        {
            try
            {
                var newpublisher = _publishersService.AddPublisher(author);
                return Created(nameof(AddPubliher), newpublisher);
            }
            //catch (PublisherNameException ex)
            //{
            //    return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            //}
            //or like code below
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-publisher-by-id/{id}")]
        public ActionResult<Publisher> GetPublisherById(int id)     // or use IActionResult 
        {
            //throw new Exception("This is an exception is handled by middleware");   // working on buildinmiddleware exception
            var response = _publishersService.GetPublisherById(id);

            if (response != null)
            {
                return Ok(response);
                // return response;   // If returning type Publisher
            }
            else
            {
                return NotFound();
                //return null;        //If returning type Publisher
            }
        }

        [HttpGet("get-custom_actionresult(publisher)-by-id/{id}")]
        public CustomActionResult GetCustomActionResultPublisherById(int id)
        {
            var response = _publishersService.GetPublisherById(id);

            if (response != null)
            {
                var responseObject = new CustomActionResultVM()
                {
                    Publisher = response,
                };
                return new CustomActionResult(responseObject);
            }
            else
            {
                var responseObject = new CustomActionResultVM()
                {
                    Exception = new Exception("Exception from publisher controller")
                };
                return new CustomActionResult(responseObject);
            }
        }

        [HttpGet("get-one-publisher-with-bookdetails/{id}")]
        public IActionResult GetPublisherWithBooks(int id)
        {
            //throw new Exception("This is an exception is handled by middleware");   // working on buildinmiddleware exception
            var response = _publishersService.GetPublisherWithBooks(id);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher-books-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var response = _publishersService.GetPublisherData(id);
            return Ok(response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
