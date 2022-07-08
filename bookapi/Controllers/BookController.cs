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
    public class BookController : ControllerBase
    {

        BookRepository _bookRepository = new BookRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetBooks(string keyword,int pageIndex=1,int pageSize = 10)
        {
             var books= _bookRepository.GetBookList(pageIndex, pageSize, keyword);

            ListResponce<BookModel> listResponce = new ListResponce<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel(c)),
                TotalRecords = books.TotalRecords,
            };

            return Ok(listResponce);

        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Not Found");
            BookModel bookModel = new BookModel(book);

            
            return Ok(bookModel);
        }



        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            if (model == null)
                return BadRequest("model is null");

            Book book = new Book()
            {
                Id = model.Id ,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
        };
            var response = _bookRepository.AddBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }



        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            if (model == null)
                return BadRequest("model is null");

            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,

            };
            var response = _bookRepository.UpdateBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }



        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
                return BadRequest("id is 0");

            var response = _bookRepository.DeleteBook(id);
            return Ok(response);
        }




    }
}
