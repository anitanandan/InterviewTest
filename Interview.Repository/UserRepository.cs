namespace Interview.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using Interview.Domain.Models;
    using Interview.Domain.Repository;

    public class UserRepository : AdoBaseRepository<User>, IUserRepository
    {
        #region IUserRepository Members

        public User GetById(long id)
        {
            return FetchUser(id)[0];
        }

        public User GetByUsername(string userName)
        {
            //throw new Exception("Not implemented");
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = string.Format("SELECT Id, Username, Firstname, Lastname FROM [User] WHERE UPPER(Username) = {0}", userName);
            var result= GetData(sqlCommand, CreateUser);
            if(result.Count > 0)
                    return result[0];
            else
                throw new Exception("No record found");
        }

        public void Save(User entity)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT INTO [User] (Username, Firstname, Lastname) " +
                      "VALUES (@Username, @FirstName, @LastName);select @@identity";
            sqlCommand.Parameters.AddWithValue("@Username", entity.Username);
            sqlCommand.Parameters.AddWithValue("@Firstname", entity.Firstname);
            sqlCommand.Parameters.AddWithValue("@LastName", entity.Lastname);

            var idreturned = ExecuteScalarInTransaction(sqlCommand);
            if (idreturned == 0)
                throw new Exception("User Not Added");

            // Hyrdrate entity with newly inserted Id
            entity.Id = idreturned;
        }

        public List<User> All()
        {
            return FetchUser(null);
        }

        private List<User> FetchUser(long? id)
        {
            var sqlCommand = new SqlCommand();

            if (id == null)
            {
                sqlCommand.CommandText = "SELECT Id, Username, Firstname, Lastname FROM [User]";
            }
            else
            {
                sqlCommand.CommandText = string.Format("SELECT Id, Username, Firstname, Lastname FROM [User] WHERE Id = {0}", id);
            }

            return GetData(sqlCommand, CreateUser);
        }

        public User CreateUser(SqlDataReader reader)
        {
            var user = new User
            {
                Id = Convert.ToInt64(reader[0]),
                Username = (string)reader[1],
                Firstname = (string)reader[2],
                Lastname = (string)reader[3]
            };


            return user;
        }

        #endregion
    }
}
