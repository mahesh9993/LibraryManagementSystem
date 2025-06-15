using Dapper;
using ApplicationCore.Common;
using ApplicationCore.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryManagementAPI.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRegistrationRepository bookRegistrationRepository;
        private readonly IBookUpdateRepository bookUpdateRepository;
        private readonly LibraryManagementAPI.Services.IConnectionFactory connectionFactory;

        public BookService(IBookRegistrationRepository bookRegistrationRepository, LibraryManagementAPI.Services.IConnectionFactory connectionFactory, IBookUpdateRepository bookUpdateRepository)
        {
            this.bookRegistrationRepository = bookRegistrationRepository;
            this.connectionFactory = connectionFactory;
            this.bookUpdateRepository = bookUpdateRepository;
        }

        public async Task<CommonResponse> RegisterBook(Book book)
        {
            var result = await bookRegistrationRepository.SaveBooks(book);

            if (result > 0) 
            {
                return new CommonResponse(StatusCode.Success,"Registration Success",result);
            }
            return new CommonResponse(StatusCode.Error, "Registration Failed", result);
        }

        public async Task<CommonResponse> UpdateBook(BookDetailModel book)
        {
            var result = await bookUpdateRepository.UpdateBooks(book);

            if (result > 0)
            {
                return new CommonResponse(StatusCode.Success, "Registration Success", result);
            }
            return new CommonResponse(StatusCode.Error, "Registration Failed", result);
        }

        public async Task<CommonResponse> GetAllBooks()
        {
            using var conn = connectionFactory.CreateConnection();
            var result = await conn.QueryAsync<BookDetailModel>("GetAllBooks", commandType: CommandType.StoredProcedure);
            
            if(result.Any())
            {
                return new CommonResponse(StatusCode.Success,"Success",result);
            }
            return new CommonResponse(StatusCode.Error, "No Books Found", result);
        }

        public async Task<CommonResponse> GetAllBookCategories()
        {
            using var conn = connectionFactory.CreateConnection();
            var result = await conn.QueryAsync<BookCategoryModel>("GetAllBookCategories", commandType: CommandType.StoredProcedure);

            if (result.Any())
            {
                return new CommonResponse(StatusCode.Success, "Success", result);
            }
            return new CommonResponse(StatusCode.Error, "No Books Found", result);
        }
    }
}
