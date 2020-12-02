using GraphQL.Types;
using GraphQLBookstore.Models;
using GraphQLBookstore.Repositories;

namespace  GraphQLBookstore.GraphQL.Types 
{
    class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType(AuthorRepository authorRepository)
        {
            Name = "Author";
            Field(x => x.Id);
            Field(x => x.Name).Description("Name of the author");
            Field<ListGraphType<BookType>>(
                "books",
                resolve: context => {
                    return authorRepository.BooksFromAuthor(context.Source.Id);
                }
            );
        }
    }
}