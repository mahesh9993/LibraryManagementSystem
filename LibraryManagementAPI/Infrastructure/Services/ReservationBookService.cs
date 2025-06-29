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

        public async Task<CommonResponse> ReserveBooks(ReservationDetailInputModel reservationDetailInput)
        {
            {
                using var connection = connectionFactory.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("UserNumber", reservationDetailInput.UserNumber, DbType.String);
                parameters.Add("BookNumber", reservationDetailInput.BookNumber, DbType.String);
                parameters.Add("CreatedBy", reservationDetailInput.CreatedBy, DbType.Int32);
                parameters.Add("Result", "0", DbType.Int32, direction: ParameterDirection.Output);

                await connection.QueryAsync("[dbo].[SaveReserveBookDetails]", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("Result");
                return new CommonResponse(StatusCode.Success, "Reserved Successfully", result);
            }
        }

        public async Task<CommonResponse> GetReserveBooks()
        {
            using var connection = connectionFactory.CreateConnection();

            var parameters = new DynamicParameters();

            var result = await connection.QueryAsync<ReserveDetailOutputModel>("[dbo].[GetReserveBookDetails]", parameters, commandType: CommandType.StoredProcedure);
            return new CommonResponse(StatusCode.Success, "Success", result);

        }

        public async Task<CommonResponse> UpdateReservationDetail(UpdateReserveModel inputModel)
        {
            try
            {

                using var connection = connectionFactory.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("BookCopyID", inputModel.BookCopyID, DbType.Int32);
                parameters.Add("Result", "0", DbType.Int32, direction: ParameterDirection.Output);

                await connection.QueryAsync("[dbo].[UpdateReservationDetail]", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("Result");
                return new CommonResponse(StatusCode.Success, "Reserved Successfully", result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    } 
}
