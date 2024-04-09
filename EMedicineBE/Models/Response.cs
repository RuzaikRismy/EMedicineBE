using System.Runtime.CompilerServices;

namespace EMedicineBE.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

       /* this will give a list of users*/
        public List<Users> listUsers { get; set; }

       /*     This will give one user*/
        public Users user { get; set; }

        /* This will give a list of medicines */
        public List<Medicines> listMedicines { get; set; }

        /* This will give one medicine */
        public Medicines medicine { get; set; }

        /* This will give a list of carts */
        public List<Cart> listCart { get; set; }

        /* This will give a list of orders */
        public List<Orders> listOrders { get; set; }

        /* This will give one order */
        public Orders order { get; set; }

        /* This will give a list of orderItems */
        public List<OrderItems> listOrderitems { get; set; }

        /* This will give a one orderItem */
        public OrderItems orderItem { get; set; }

    }
}
