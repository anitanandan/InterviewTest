namespace Interview.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class AdoBaseRepository<T>
    {
        private readonly string _connectionString;

        public delegate T BuildObject(SqlDataReader reader);

        public AdoBaseRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Users"].ConnectionString;

            if (String.IsNullOrEmpty(_connectionString))
                throw new Exception("Connection Not Set");
        }

        public long ExecuteScalarInTransaction(SqlCommand sqlCommand)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            long returnValue = 0;

            SqlTransaction sqlTransaction = null;

            try
            {
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Transaction = sqlTransaction;
                returnValue = Convert.ToInt64(sqlCommand.ExecuteScalar());
                sqlTransaction.Commit();
            }
            catch
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

            return returnValue;
        }

        public List<T> GetData(SqlCommand sqlCommand, BuildObject createObject)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var collection = new List<T>();
            try
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                var sqlReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (null != sqlReader)
                    while (sqlReader.Read())
                        collection.Add(createObject(sqlReader));
            }
            finally
            {
                sqlConnection.Close();
            }

            return collection;
        }
    }
}
