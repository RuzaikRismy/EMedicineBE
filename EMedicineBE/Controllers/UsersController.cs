using EMedicineBE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;

namespace EMedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        [HttpPost]
        [Route("registration")]
       /* here register is a function and, inside that function we pass Users Model calss as parameter*/
        public Response register(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = dal.register(users, connection);
            return response;
        }
       
        [HttpPost]
        [Route("login")]
        /* here register is a function and, inside that function we pass Users Model calss as parameter*/
        public Response loggin(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response  = dal.loggin(users, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUsers")]
        /* here register is a function and, inside that function we pass Users Model calss as parameter*/
        public Response viewUsers(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.viewUsers(users, connection);
            return response;
        }

        [HttpPost]
        [Route("updateProfile")]
        /* here register is a function and, inside that function we pass Users Model calss as parameter*/
        public Response updateProfile(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.updateProfile(users, connection);
            return response;
        }
    }
}
