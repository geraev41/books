using GraphQLBookstore.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQLBookstore.GraphQL;

namespace GraphQLBookstore.Repositories
{
    class AuthorRepository
    {
        private readonly DataBaseContext _context;
        public AuthorRepository(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> All(ResolveFieldContext<object> context){
            var results = from authors in _context.Authors select authors;
            if (context.HasArgument("name"))
            {
                var value = context.GetArgument<string>("name");
                results = results.Where(a => a.Name.Contains(value));
            }
            return PaginatedList<Author>.Paginate(results, context);
        }

        public IEnumerable<Book> BooksFromAuthor(long authorId){
            var results = from books in _context.Books select books;
            results = results.Where(a => a.AuthorId == authorId);
            return results;
        }

        public Author Find(long id){
            return _context.Authors.Find(id);
        }

        public async Task<Author> Add(Author author) {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Update(long id, Author author) {
            author.Id = id;
            var updated = (_context.Authors.Update(author)).Entity;
            if (updated == null)
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return updated;
        }

        public async Task<Author> Remove(long id) {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return null;
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return author;
        }

    }
}