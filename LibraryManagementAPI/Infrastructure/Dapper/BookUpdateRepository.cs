using Dapper;
using ApplicationCore.Models;
using LibraryManagementAPI.Services;
using System.Data;

namespace LibraryManagementAPI.Infrastructure.Dapper
{
    public class BookUpdateRepository : IBookUpdateRepository
    {
        private readonly IConnectionFactory connectionFactory;

        public BookUpdateRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<int> UpdateBooks(BookDetailModel model)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {

                for (int i = 0; i < model.NoofCopies; i++)
                {
                    DynamicParameters dynamicBookCopyParameters = MapToBookCopyParams(model);

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

        private static DynamicParameters MapToBookCopyParams(BookDetailModel item)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("BookCopyID", "0", DbType.Int32, ParameterDirection.InputOutput);
            dynamicParameters.Add("BookID", item.BookID, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("CreatedBy", item.CreatedBy, DbType.Int32, ParameterDirection.Input);

            return dynamicParameters;
        }
    }
}
