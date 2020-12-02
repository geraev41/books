using GraphQL.Types;
using GraphQLBookstore.Models;
using GraphQLBookstore.Repositories;

namespace  GraphQLBookstore.GraphQL.Types 
{
    class BookType : ObjectGraphType<Book>
    {
        public BookType(AuthorRepository authorRepository)
        {
            Name = "Book";
            Field(x => x.Id);
            Field(x => x.Name).Description("Name of the book");
            Field(x => x.Description).Description("Description of the book");
            Field(x => x.Price);
            Field(x => x.AuthorId);
            Field<AuthorType>(
                "author",
                resolve: context => {
                    return authorRepository.Find(context.Source.Id);
                }
            );
        }
    }
}