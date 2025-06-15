using Dapper;
using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using System.Data;
using System.Reflection;
using System.Transactions;

namespace LibraryManagementAPI.Infrastructure.Services
{
    public class LoanService : ILoanService
    {
        private readonly IConnectionFactory connectionFactory;

        public LoanService(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<CommonResponse> CheckBookAvailability(int bookcopyID)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("BookCopyID", bookcopyID, DbType.Int32, ParameterDirection.Input);

            using var conn = connectionFactory.CreateConnection();
            var result = await conn.QueryAsync<BookAvailabilityOutputModel>("CheckBookAvailability",param:dynamicParameters, commandType: CommandType.StoredProcedure);

            return new CommonResponse(StatusCode.Success, "Success", result);
        }

        public async Task<CommonResponse> GetLoanDetailsByUser(int userID)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("UserID", userID, DbType.Int32, ParameterDirection.Input);

            using var conn = connectionFactory.CreateConnection();
            var result = await conn.QueryAsync<LoanDetailModel>("GetLoanDetailsByUser", param: dynamicParameters, commandType: CommandType.StoredProcedure);

            if (result.Any())
            {
                return new CommonResponse(StatusCode.Success, "Success", result);
            }
            return new CommonResponse(StatusCode.Error, "No loans Found", result);
        }

        public async Task<CommonResponse> SaveLoan(LoanModel loan)
        {
            DynamicParameters dynamicBookParameters = MapToLoanParams(loan);

            using var conn = connectionFactory.CreateConnection();
            await conn.QueryAsync<Book>("SaveLoan",
            param: dynamicBookParameters,
            commandType: CommandType.StoredProcedure);

            var loanId = dynamicBookParameters.Get<int>("LoanID");

            if (loanId < 0)
            {
                return new CommonResponse(StatusCode.Error, "No loans Found", loanId);
            }

            return new CommonResponse(StatusCode.Success, "Success", loanId);
        }

        private static DynamicParameters MapToLoanParams(LoanModel loan)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("LoanID", "0", DbType.Int32, ParameterDirection.InputOutput);
            dynamicParameters.Add("UserID", loan.UserID, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("BookCopyID", loan.BookCopyID, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("ReturnDate", loan.ReturnDate, DbType.DateTime, ParameterDirection.Input);
            dynamicParameters.Add("CreatedBy", loan.CreatedBy, DbType.Int32, ParameterDirection.Input);

            return dynamicParameters;
        }
    }
}
