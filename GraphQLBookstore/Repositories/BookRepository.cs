using GraphQLBookstore.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQLBookstore.GraphQL;

namespace GraphQLBookstore.Repositories
{
    class BookRepository
    {
        private readonly DataBaseContext _context;
        public BookRepository(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> All(ResolveFieldContext<object> context, bool order){
            var results = from books in _context.Books select books;
            if (context.HasArgument("name"))
            {
                var value = context.GetArgument<string>("name");
                results = results.Where(a => a.Name.Contains(value));
            }
            if (context.HasArgument("description"))
            {
                var value = context.GetArgument<string>("description");
                results = results.Where(a => a.Description.Contains(value));
            }
            if (context.HasArgument("price"))
            {
                var value = context.GetArgument<double>("price");
                results = results.Where(a => a.Price == value);
            }
            if (context.HasArgument("authorId"))
            {
                var value = context.GetArgument<long>("authorId");
                results = results.Where(a => a.AuthorId == value);
            }
            results =  order?results.OrderBy(x => x.Name): results; 
            return PaginatedList<Book>.Paginate(results, context);
        }

        public Book Find(long id){
            return _context.Books.Find(id);
        }

        public async Task<Book> Add(Book book) {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Update(long id, Book book) {
            book.Id = id;
            var updated = (_context.Books.Update(book)).Entity;
            if (updated == null)
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return updated;
        }

        public async Task<Book> Remove(long id) {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}