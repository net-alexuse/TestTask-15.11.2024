using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;
        private readonly DateTime condition = new DateTime(2015, 1, 1);
        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Author> GetAuthor()
        {
            var author = await _context.Books
                .OrderByDescending(b => b.Title.Length)
                .ThenBy(b => b.AuthorId)
                .Select(b => b.Author)
                .FirstOrDefaultAsync();
            return author;
        }

        public async Task<List<Author>> GetAuthors()
        {
            var authors = await _context.Authors
                .Where(b => b.Books.Any(c => c.PublishDate > condition))
                .Where(b => b.Books.Count(c => c.PublishDate > condition) % 2 ==0)
                .ToListAsync();
            return authors;
        }
    }
}
