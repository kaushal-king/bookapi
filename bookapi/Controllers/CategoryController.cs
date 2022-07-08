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
    public class CategoryController : ControllerBase
    {

        CategoryRepository _categoryRepository = new CategoryRepository();

        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(ListResponce<CategoryModel>),(int)HttpStatusCode.OK)]
        public IActionResult GetCatogires(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var categories = _categoryRepository.GetCategories(pageIndex, pageSize, keyword);
            ListResponce<CategoryModel> listResponse = new ListResponce<CategoryModel>()
            {
                Results = categories.Results.Select(c => new CategoryModel(c)),
                TotalRecords = categories.TotalRecords,
            };

            return Ok(listResponse);
        }


        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if (category == null)
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(),"Not Found");
            CategoryModel categoryModel = new CategoryModel(category);

            return Ok(categoryModel);
        }



        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCategory(CategoryModel model)
        {
            if (model == null)
                return BadRequest("model is null");

            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            var response = _categoryRepository.AddCategory(category);
            CategoryModel categoryModel = new CategoryModel(response);

            return Ok(categoryModel);
        }



        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            if(model==null)
                return BadRequest("model is null");

            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            var response = _categoryRepository.UpdateCategory(category);
            CategoryModel categoryModel = new CategoryModel(response);

            return Ok(categoryModel);
        }



        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            if(id==0)
                return BadRequest("id is 0");

            var response = _categoryRepository.DeleteCategory(id);
            return Ok(response);
        }








    }
}
