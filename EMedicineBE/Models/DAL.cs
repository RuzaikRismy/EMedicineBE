using System.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace EMedicineBE.Models
{

    /*DAL stand for Data Access Layer, Inside this we write database logics*/
    public class DAL
    {
        /*create function(API) for registration,*/
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            string sp_register = "INSERT INTO Users (FirstName, LastName, Password, Email, Fund, Type, Status, CreatedOn) " +
                          "VALUES (@FirstName, @LastName, @Password, @Email, @Fund, @Type, @Status, @CreatedOn)";

            SqlCommand cmd = new SqlCommand(sp_register, connection);
/*            cmd.CommandType = CommandType.StoredProcedure;*/
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0); // when registering, by default Fund is 0
            cmd.Parameters.AddWithValue("@Type", users.Type); // when registering, by default Type is Users
            cmd.Parameters.AddWithValue("@Status", 0); //when registering, by default Status is 0
            cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now); // //when registering, by default createdOn is currentdate and time
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User Registered Successfully..!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User Registration Failed..!";
            }
            return response;
        }

        /*create function(API) for loggin,*/
        public Response loggin(Users users, SqlConnection connection)
        {
            string sp_login = "SELECT  * FROM Users WHERE Email = @Email AND Password = @Password";
            SqlDataAdapter da = new SqlDataAdapter(sp_login, connection);
            /*da.SelectCommand.CommandType = CommandType.StoredProcedure;*/
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if(dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid..!";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid..!";
                response.user = null;
            }
            return response;

        }

        public Response viewUsers(Users users, SqlConnection connection)
        {
            string sp_viewUser = "SELECT  * FROM Users";
            SqlDataAdapter da = new SqlDataAdapter(sp_viewUser, connection);
            /*da.SelectCommand.CommandType = CommandType.StoredProcedure;*/
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt); // This will return a single user data
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode = 200;
                response.StatusMessage = "User exists..!";
           /*     response.user = user;*/
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exists..!";
                response.user = user;
            }
            return response;
        }
        public Response updateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();
            string sqlUpdateUser = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Password = @Password WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(sqlUpdateUser, connection);
            /*cmd.CommandType = CommandType.StoredProcedure;*/
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User updated successfully..!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User updated failed..!";
            }
            return response;

        }

        public Response addToCart(Cart cart , SqlConnection connection)
        {
            Response response = new Response();
            string sp_AddToCart = "INSERT INTO Cart (UserId, MedicineID, UnitPrice, Discount, Quantity, TotalPrice) " +
               " VALUES (@UserId, @MedicineID, @UnitPrice, @Discount, @Quantity, @TotalPrice)";
            SqlCommand cmd = new SqlCommand(sp_AddToCart, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", cart.UserId);
            cmd.Parameters.AddWithValue("@MedicineID", cart.MedicineID);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            connection.Open();
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item added successfully..!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added..!";
            }
            return response;
        }

        public Response placeAnOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order placed successfully..!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order placed failed..!";
            }
            return response;
        }

        public Response orderList(Users users, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();

            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            if(dt.Rows.Count  > 0)
            {
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    // Here going to create a single object of orders
                    Orders order = new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    listOrder.Add(order);
                }
                if(listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched Successfully..!";
                    response.listOrders = listOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details fetching Failed..!";
                    response.listOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order details are not available..!";
                response.listOrders = null;
            }
            return response;
        }

        // Using this we can manage both add and update storedProcedures using this
        public Response addUpdateMedicine(Medicines medicines, SqlConnection connection)
        {
            Response response = new Response();
            string sp_AddUpdateMedicine = "INSERT INTO Medicines (Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status, Type)" +
                "VALUES(@Name, @Manufacturer, @UnitPrice, @Discount, @Quantity, @ExpDate, @ImageUrl, @Status, @Type) ";
            SqlCommand cmd = new SqlCommand(sp_AddUpdateMedicine, connection); 
           /* cmd.CommandType = CommandType.StoredProcedure;*/
            cmd.Parameters.AddWithValue("@Name", medicines.Name);
            cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
            cmd.Parameters.AddWithValue("@UnitPrice", medicines.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
            cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
            cmd.Parameters.AddWithValue("@ExpDate", medicines.ExpDate);
            cmd.Parameters.AddWithValue("@ImageUrl", medicines.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", medicines.Status);
            cmd.Parameters.AddWithValue("@Type", medicines.Type);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine added successfully..!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Medicine adding failed..!";
            }
            return response;
        }

        public Response listAllUsers(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();

            string sp_userList = "SELECT * FROM Users";
            SqlDataAdapter da = new SqlDataAdapter(sp_userList, connection);
          /*  da.SelectCommand.CommandType = CommandType.StoredProcedure;*/
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Here going to create a single object of orders
                    Users user = new Users();

                    user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                   /* user.Type = Convert.ToString(dt.Rows[i]["Type"]);*/
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);

                    listUsers.Add(user);
                }
                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Users details fetched Successfully..!";
                    response.listUsers = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Users details fetching Failed..!";
                    response.listOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Users details are not available..!";
                response.listOrders = null;
            }
            return response;
        }
    }

}
