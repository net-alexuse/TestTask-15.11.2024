﻿using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly DateTime carolusRexAlbumDate = new DateTime(2012, 5, 22, 12, 0, 0);
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetBook()
        {
            var book = await _context.Books
                .OrderByDescending(b => b.Price * b.QuantityPublished)
                .FirstOrDefaultAsync();
            return book;
        }

        public async Task<List<Book>> GetBooks()
        {
            var books = await _context.Books
                .Where(b => b.Title.Contains("Red") && b.PublishDate > carolusRexAlbumDate)
                .ToListAsync();
            return books;
        }
    }
}
