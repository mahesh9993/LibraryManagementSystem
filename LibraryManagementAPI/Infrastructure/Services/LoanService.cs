﻿using Dapper;
using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<CommonResponse> CheckBookAvailability(string bookCopyNumber)
        {
            
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("BookCopyNumber", bookCopyNumber, DbType.String, ParameterDirection.Input);

            using var conn = connectionFactory.CreateConnection();
            var result = await conn.QueryAsync<BookAvailabilityOutputModel>("CheckBookAvailability", param: dynamicParameters, commandType: CommandType.StoredProcedure);

            return new CommonResponse(StatusCode.Success, "Success", result); 
        }

        public async Task<CommonResponse> GetLoanDetailsByUser(string userNumber)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("UserNumber", userNumber, DbType.String, ParameterDirection.Input);

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
            await conn.QueryAsync<LoanModel>("SaveLoan",
            param: dynamicBookParameters,
            commandType: CommandType.StoredProcedure);

            var loanId = dynamicBookParameters.Get<int>("LoanID");

            if (loanId < 0)
            {
                return new CommonResponse(StatusCode.Error, "No loans Found", loanId);
            }

            return new CommonResponse(StatusCode.Success, "Success", loanId);
        }

        public async Task<CommonResponse> GetLoansByUser(BookReturnModel model)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("UserNumber", model.UserNumber, DbType.String, ParameterDirection.Input);

            using var conn = connectionFactory.CreateConnection();
            var result = await conn.QueryAsync<LoanModel>("GetLoansByUser", param: dynamicParameters, commandType: CommandType.StoredProcedure);

            if (result.Any())
            {
                return new CommonResponse(StatusCode.Success, "Success", result);
            }
            return new CommonResponse(StatusCode.Error, "No loans Found", result);
        }

        private static DynamicParameters MapToLoanParams(LoanModel loan)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("LoanID", "0", DbType.Int32, ParameterDirection.InputOutput);
            dynamicParameters.Add("UserNumber", loan.UserNumber, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("BookCopyNumber", loan.BookCopyNumber, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("ReturnDate", loan.ReturnDate, DbType.DateTime, ParameterDirection.Input);
            dynamicParameters.Add("CreatedBy", loan.CreatedBy, DbType.Int32, ParameterDirection.Input);

            return dynamicParameters;
        }

        public async Task<CommonResponse> ReturnBookDelete(BookReturnModel model)
        {
            try
            {
                using var connection = connectionFactory.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("BookCopyID", model.BookCopyID, DbType.Int32);
                parameters.Add("Result", "0", DbType.Int32, direction: ParameterDirection.Output);

                await connection.QueryAsync("[dbo].[BookReturnDelete]", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("Result");
                return new CommonResponse(StatusCode.Success, "Returned Successfully", result);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
