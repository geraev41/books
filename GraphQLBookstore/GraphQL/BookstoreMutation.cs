using GraphQL.Types;
using GraphQLBookstore.Repositories;
using GraphQLBookstore.GraphQL.Types;
using GraphQLBookstore.Models;

namespace  GraphQLBookstore.GraphQL {
    class BookstoreMutation : ObjectGraphType
    {
        public BookstoreMutation(AuthorRepository authorRepository, BookRepository bookRepository)
        {
            Field<AuthorType>("createAuthor",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "input" }),
                              resolve: context => {
                                  return authorRepository.Add(context.GetArgument<Author>("input"));
                              });
            Field<AuthorType>("updateAuthor",
                              arguments: new QueryArguments(
                                  new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                                  new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "input" }
                              ),
                              resolve: context => {
                                  var id = context.GetArgument<long>("id");
                                  var author = context.GetArgument<Author>("input");
                                  return authorRepository.Update(id, author);
                              });
            Field<AuthorType>("deleteAuthor",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                              resolve: context => {
                                  return authorRepository.Remove(context.GetArgument<long>("id"));
                              });
            
            Field<BookType>("createBook",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<BookInputType>> { Name = "input" }),
                              resolve: context => {
                                  return bookRepository.Add(context.GetArgument<Book>("input"));
                              });
            Field<BookType>("updateBook",
                              arguments: new QueryArguments(
                                  new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                                  new QueryArgument<NonNullGraphType<BookInputType>> { Name = "input" }
                              ),
                              resolve: context => {
                                  var id = context.GetArgument<long>("id");
                                  var book = context.GetArgument<Book>("input");
                                  return bookRepository.Update(id, book);
                              });
            Field<BookType>("deleteBook",
                              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                              resolve: context => {
                                  return bookRepository.Remove(context.GetArgument<long>("id"));
                              });
        }
    }
}