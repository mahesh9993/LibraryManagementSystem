using Dapper;
using LibraryManagementAPI.Common;
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

        public async Task<CommonResponse> ReserveBooks(string userNumber, string bookNumber, int CreatedBy)
        {
            {
                using var connection = connectionFactory.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("UserNumber", userNumber, DbType.String);
                parameters.Add("BookNumber", bookNumber, DbType.String);
                parameters.Add("CreatedBy", CreatedBy, DbType.Int32);
                parameters.Add("Result", "0", DbType.Int32, direction: ParameterDirection.Output);

                await connection.QueryAsync("[dbo].[SaveReserveBookDetails]", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("Result");
                return new CommonResponse(StatusCode.Success, "Reserved Successfully", result);
            }
        }

        public async Task<CommonResponse> GetReserveBooks(string? userNumber, string? bookNumber)
        {
            using var connection = connectionFactory.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("UserNumber", userNumber, DbType.String);
            parameters.Add("BookNumber", bookNumber, DbType.String);

            var result = await connection.QueryFirstOrDefaultAsync<ReserveDetailOutputModel>("[dbo].[GetReserveBookDetails]", parameters, commandType: CommandType.StoredProcedure);
            return new CommonResponse(StatusCode.Success, "Success", result);

        }
    }
}
