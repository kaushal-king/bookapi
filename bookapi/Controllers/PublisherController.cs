using System.Linq;
using System.Net;
using bookapi.Models.Models;
using bookapi.Models.ViewModels;
using bookapi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {


        PublisherRepository _publisherRepository = new PublisherRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetBooks(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var books = _publisherRepository.GetPublisherlist(pageIndex, pageSize, keyword);

            ListResponce<PublisherModel> listResponce = new ListResponce<PublisherModel>()
            {
                Results = books.Results.Select(c => new PublisherModel(c)),
                TotalRecords = books.TotalRecords,
            };

            return Ok(listResponce);

        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
            var publisher = _publisherRepository.GetPublisher(id);
            if (publisher == null)
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Not Found");
            PublisherModel publisherModel = new PublisherModel(publisher);


            return Ok(publisherModel);
        }



        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(PublisherModel model)
        {
            if (model == null)
                return BadRequest("model is null");

            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
        };
            var response = _publisherRepository.AddPublisher(publisher);
            PublisherModel publisherModel = new PublisherModel(response);

            return Ok(publisherModel);
        }



        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(PublisherModel model)
        {
            if (model == null)
                return BadRequest("model is null");

            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
            };
            var response = _publisherRepository.UpdatePublisher(publisher);
            PublisherModel publisherModel = new PublisherModel(response);

            return Ok(publisherModel);
        }



        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
                return BadRequest("id is 0");

            var response = _publisherRepository.DeletePublisher(id);
            return Ok(response);
        }


    }
}
