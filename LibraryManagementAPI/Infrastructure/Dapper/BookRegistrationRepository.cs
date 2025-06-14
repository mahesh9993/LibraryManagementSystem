using Dapper;
using System.Data;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Infrastructure.Dapper
{
    public class BookRegistrationRepository : IBookRegistrationRepository
    {
        private readonly IConnectionFactory connectionFactory;

        public BookRegistrationRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<int> SaveBooks(Book model)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                
                DynamicParameters dynamicBookParameters = MapToBookParams(model);

                await connection.QueryAsync<Book>("SaveBook",
                param: dynamicBookParameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure);

                var bookID = dynamicBookParameters.Get<int>("BookID");

                if (bookID < 0)
                {
                    transaction.Rollback();
                    return 0;
                }

                for (int i = 0; i < model.NoofCopies; i++)
                {
                    DynamicParameters dynamicBookCopyParameters = MapToBookCopyParams(model, bookID);

                    await connection.QueryAsync<Book>("SaveBookCopy",
                    param: dynamicBookCopyParameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure);

                    var bookCopyID = dynamicBookCopyParameters.Get<int>("BookCopyID");

                    if (bookCopyID < 0)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }

                transaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return -1;
            }
        }

        private static DynamicParameters MapToBookParams(Book item)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("BookID", "0", DbType.Int32, ParameterDirection.InputOutput);
            dynamicParameters.Add("BookCategoryID", item.BookCategoryID, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("ISBN", item.ISBN, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("BookCategoryID", item.BookCategoryID, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("Title", item.Title, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("Author", item.Author, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("Publisher", item.Publisher, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("CreatedBy", item.CreatedBy, DbType.Int32, ParameterDirection.Input);

            return dynamicParameters;
        }
        private static DynamicParameters MapToBookCopyParams(Book item, int bookID)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("BookCopyID", "0", DbType.Int32, ParameterDirection.InputOutput);
            dynamicParameters.Add("BookID", bookID, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("CreatedBy", item.CreatedBy, DbType.Int32, ParameterDirection.Input);

            return dynamicParameters;
        }
    }
}
