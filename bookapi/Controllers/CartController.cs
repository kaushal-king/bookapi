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
    public class CartController : ControllerBase
    {


        CartRepository _cartRepository = new CartRepository();

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponce<CartModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCartItem(string keyword, int pageIndex = 1, int pageSize = 10)
        {


            var cartitem = _cartRepository.GetCartList(pageIndex, pageSize, keyword);
            ListResponce<CartModel> listResponce = new ListResponce<CartModel>()
            {
                Results=cartitem.Results.Select(c=>new CartModel(c)),
                TotalRecords=cartitem.TotalRecords,
            };

            return Ok(listResponce);
        }



        [HttpGet]
        [Route("list2")]
        [ProducesResponseType(typeof(ListResponce<Clartresponce>), (int)HttpStatusCode.OK)]
        public IActionResult GetCartItem2(int UserId)
        {


            var cartitem = _cartRepository.GetCartListall(UserId);
            ListResponce<Clartresponce> listResponce = new ListResponce<Clartresponce>()
            {
                Results = cartitem.Results.Select(c => new Clartresponce(c)),
                TotalRecords = cartitem.TotalRecords,
            };

            return Ok(listResponce);
        }



        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCart(CartModel cartModel)
        {
            if(cartModel==null)
                return BadRequest("model is null");

            Cart cart = new Cart()
            {
                Id = cartModel.Id,
                Bookid = cartModel.BookId,
                Userid = cartModel.UserId,
                Quantity = cartModel.Quantity,
            };
            var response = _cartRepository.AddItem(cart);
            CartModel cartt = new CartModel(response);

            return Ok(cartt);
        }




        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCart(CartModel cartModel)
        {
            if (cartModel == null)
                return BadRequest("model is null");

            Cart cart = new Cart()
            {
                Id = cartModel.Id,
                Bookid = cartModel.BookId,
                Userid = cartModel.UserId,
                Quantity = cartModel.Quantity,
            };
            var response = _cartRepository.UpdateCart(cart);
            CartModel cartt = new CartModel(response);

            return Ok(cartt);
        }


        
        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Deleteart(int id)
        {
            if (id == 0)
                return BadRequest("id is 0");

            var response = _cartRepository.DeleteCart(id);
            return Ok(response);
        }


    }
}
