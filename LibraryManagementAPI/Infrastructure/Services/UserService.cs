using Dapper;
using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using System.Data;

namespace LibraryManagementAPI.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IConnectionFactory _connectionFactory;

        public UserService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<CommonResponse> SaveUserDetails(UserDetailInputModel userDetailInput)
        {
            {
                using var connection = _connectionFactory.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("UserNumber", userDetailInput.UserNumber, DbType.String);
                parameters.Add("FirstName", userDetailInput.FirstName, DbType.String);
                parameters.Add("LastName", userDetailInput.LastName, DbType.String);
                parameters.Add("Gender", userDetailInput.Gender, DbType.String);
                parameters.Add("NIC", userDetailInput.NIC, DbType.String);
                parameters.Add("Address", userDetailInput.Address, DbType.String);
                parameters.Add("UserType", userDetailInput.UserType, DbType.String);
                parameters.Add("CreatedBy", userDetailInput.CreatedBy, DbType.Int32);
                parameters.Add("Result", "0", DbType.Int32, direction: ParameterDirection.Output);

                await connection.QueryAsync("[dbo].[SaveUserDetails]", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("Result");
                return new CommonResponse(StatusCode.Success, "Save Successfully", result);
            }
        }


        public async Task<CommonResponse> GetUserDetail(string userNumber)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("UserNumber", userNumber, DbType.String);

            var result = await connection.QueryAsync<UserDetailOutputModel>("[dbo].[GetUserDetails]", parameters, commandType: CommandType.StoredProcedure);
            return new CommonResponse(StatusCode.Success, "Success", result);
        }
    }
}

