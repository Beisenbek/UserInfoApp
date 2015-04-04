using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoApp.DAL
{
    public class DataHub
    {
        private List<Model.UserCard> userCards = new List<Model.UserCard>();
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\beise\Documents\visual studio 2013\Projects\UserInfoApp\UserInfoApp\UserInfoApp.mdf';Integrated Security=True");
          
        public DataHub()
        {    
        }

        public void Create(Model.UserCard userCard)
        {
            SqlCommand commandForUserInsert = new SqlCommand(string.Format("insert into [dbo].[User] values ('{0}','{1}','{2}','{3}')",
                                                                userCard.UserInfo.Id,userCard.UserInfo.Name,
                                                                userCard.UserInfo.Surname,userCard.UserInfo.Cash),
                                                connection);

            SqlCommand commandForLocationInsert = new SqlCommand(string.Format("insert into [dbo].[Location] values ('{0}','{1}','{2}')",
                                                                userCard.LocationInfo.Name, userCard.LocationInfo.Zip, userCard.LocationInfo.UserId),
                                                connection);

            connection.Open();

            commandForUserInsert.ExecuteNonQuery();
            commandForLocationInsert.ExecuteNonQuery();

            connection.Close();

        }

        public int Count()
        {
            return userCards.Count;
        }


        public Model.UserCard Read(int index)
        {
            return userCards[index];
        }

        public void Read()
        {
            userCards = new List<Model.UserCard>();

            SqlCommand command = new SqlCommand("select u.Id as Id, u.Name as Name,"+ 
                                                "u.Surname as Surname, u.Cash as Cash,"+
                                                "l.Name as Location, l.Zip as Zip from [dbo].[User] "+
                                                "u inner join [dbo].[Location] l on u.Id = l.UserId", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Model.User user = new Model.User();
                user.Id = reader["Id"].ToString();
                user.Name = reader["Name"].ToString();
                user.Surname = reader["Surname"].ToString();
                user.Cash = reader["Cash"].ToString();
               
                Model.Location location = new Model.Location();
                location.Name = reader["Location"].ToString();
                location.UserId = reader["Id"].ToString();
                location.Zip = reader["Zip"].ToString();

                Model.UserCard userCard = new Model.UserCard();
                userCard.UserInfo = user;
                userCard.LocationInfo = location;

                userCards.Add(userCard);
            }

            connection.Close();

        }

        public void Update(Model.UserCard userCard)
        {
            SqlCommand commandForUserUpdate = new SqlCommand(string.Format("update [dbo].[User]"+
                                                                            "set Name = '{0}'," +
                                                                             "Surname = '{1}'," +
                                                                                "Cash = '{2}'" +
                                                                            "where Id = '{3}'", 
                                                                                userCard.UserInfo.Name,
                                                                                userCard.UserInfo.Surname,
                                                                                userCard.UserInfo.Cash,
                                                                                userCard.UserInfo.Id),
                                              connection);

            SqlCommand commandForLocationDelete = new SqlCommand(string.Format("delete from [dbo].[Location] where UserId = '{0}'", userCard.UserInfo.Id),
                                               connection);


            SqlCommand commandForLocationInsert = new SqlCommand(string.Format("insert into [dbo].[Location] values ('{0}','{1}','{2}')",
                                                               userCard.LocationInfo.Name, userCard.LocationInfo.Zip, userCard.LocationInfo.UserId),
                                               connection);

            connection.Open();

            commandForUserUpdate.ExecuteNonQuery();
            commandForLocationDelete.ExecuteNonQuery();
            commandForLocationInsert.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete(Model.UserCard userCard)
        {
            SqlCommand commandForUserDelete = new SqlCommand(string.Format("delete from [dbo].[User] where Id = '{0}'", userCard.UserInfo.Id),
                                               connection);

            SqlCommand commandForLocationDelete = new SqlCommand(string.Format("delete from [dbo].[Location] where UserId = '{0}'", userCard.UserInfo.Id),
                                               connection);

            connection.Open();

            commandForUserDelete.ExecuteNonQuery();
            commandForLocationDelete.ExecuteNonQuery();

            connection.Close();
        }
    }
}
