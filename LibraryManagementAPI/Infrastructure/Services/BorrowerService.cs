using System.Data;
using Dapper;
using ApplicationCore.Models;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Infrastructure.Services
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IConnectionFactory connectionFactory;

        public BorrowerService(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Borrower>> GetAllAsync()
        {
            using var conn = connectionFactory.CreateConnection();
            return await conn.QueryAsync<Borrower>("GetAllBorrowers", commandType: CommandType.StoredProcedure);
        }

        public async Task<Borrower?> GetByIdAsync(int id)
        {
            using var conn = connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Borrower>(
                "GetBorrowerById]",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
