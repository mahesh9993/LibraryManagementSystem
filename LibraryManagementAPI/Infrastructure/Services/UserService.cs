using Dapper;
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

        public async Task<bool> SaveUserDetails(UserDetailInputModel userDetailInput)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("UserNumber", userDetailInput.UserNumber, DbType.Int32);
            parameters.Add("FirstName", userDetailInput.FirstName, DbType.String);
            parameters.Add("LastName", userDetailInput.LastName, DbType.String);
            parameters.Add("Gender", userDetailInput.Gender, DbType.Int32);
            parameters.Add("NIC", userDetailInput.NIC, DbType.String);
            parameters.Add("Address", userDetailInput.Address, DbType.String);
            parameters.Add("RoleID", userDetailInput.RoleID, DbType.Int32);
            parameters.Add("CreatedBy", userDetailInput.CreatedBy, DbType.Int32);
            parameters.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("[dbo].[SaveUserDetails]", parameters, commandType: CommandType.StoredProcedure);
            int result = parameters.Get<int>("Result");
            return result > 0;
        }

        public async Task<UserDetailOutputModel?> GetUserDetail(string userNumber)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("UserNumber", userNumber, DbType.String);

            var result = await connection.QueryFirstOrDefaultAsync<UserDetailOutputModel>("[dbo].[GetUserDetails]",parameters,commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}

