using EMedicineBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace EMedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _configuration;


        public MedicinesController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
           /* Response response = dal.addToCart(cart, connection);*/
            response = dal.addToCart(cart, connection);  // Here we pass cart object and connection object
            return response;
        }


        [HttpPost]
        [Route("placeAnOrder")]
        public Response placeAnOrder(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = dal.placeAnOrder(users, connection);  // Here we pass cart object and connection object
            return response;
        }

        [HttpPost]
        [Route("orderList")]
        public Response orderList(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = dal.orderList(users, connection);  
            return response;
        }

    }
}
