using Dapper;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using System.Data;

namespace LibraryManagementAPI.Infrastructure.Services
{
    public class ReservationBookService : IReservationService
    {
        private readonly IConnectionFactory connectionFactory;
        public ReservationBookService(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<bool> ReserveBooks(string userNumber, string bookNumber, int CreatedBy)
        {
            { 
                using var connection = connectionFactory.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("UserNumber", userNumber, DbType.String);
                parameters.Add("BookNumber", bookNumber, DbType.String);
                parameters.Add("CreatedBy", CreatedBy, DbType.Int32);
                parameters.Add("Result", DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("[dbo].[SaveReserveBookDetails]", parameters, commandType: CommandType.StoredProcedure);
                int result = parameters.Get<int>("Result");
                return result > 0;
            }
        }

        public async Task<ReserveDetailOutputModel?> GetReserveBooks(string? userNumber, string? bookNumber)
        {
            using var connection = connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("UserNumber", userNumber, DbType.String);
            parameters.Add("BookNumber", bookNumber, DbType.String);
            var result = await connection.QueryFirstOrDefaultAsync<ReserveDetailOutputModel>("[dbo].[GetReserveBookDetails]", parameters, commandType: CommandType.StoredProcedure);
            return result;
            
        }
    }
}
