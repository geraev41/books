using GraphQL.Types;
using GraphQLBookstore.Repositories;
using GraphQLBookstore.GraphQL.Types;

namespace  GraphQLBookstore.GraphQL {
    class BookstoreQuery : ObjectGraphType
    {
        public BookstoreQuery(AuthorRepository authorRepository, BookRepository bookRepository)
        {
            Field<ListGraphType<AuthorType>>("authors",
                                             arguments: new QueryArguments(
                                                 new QueryArgument<StringGraphType> { Name = "name" },
                                                 new QueryArgument<IntGraphType> { Name = "page", DefaultValue = 1 },
                                                 new QueryArgument<IntGraphType> { Name = "pageSize", DefaultValue = 12 }
                                             ),
                                             resolve: context => {
                                                 return authorRepository.All(context);
                                             });
            Field<AuthorType>("author",
                              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                              resolve: context => {
                                  return authorRepository.Find(context.GetArgument<long>("id"));
                              });
            
            Field<ListGraphType<BookType>>("books",
                                             arguments: new QueryArguments(
                                                 new QueryArgument<StringGraphType> { Name = "name" },
                                                 new QueryArgument<StringGraphType> { Name = "description" },
                                                 new QueryArgument<FloatGraphType> { Name = "price" },
                                                 new QueryArgument<IntGraphType> { Name = "authorId" },
                                                 new QueryArgument<IntGraphType> { Name = "page", DefaultValue = 1 },
                                                 new QueryArgument<IntGraphType> { Name = "pageSize", DefaultValue = 12 },
                                                 new QueryArgument<IntGraphType> { Name = "order" }

                                             ),
                                             resolve: context => {
                                                 return bookRepository.All(context);
                                             });
            Field<BookType>("book",
                              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                              resolve: context => {
                                  return bookRepository.Find(context.GetArgument<long>("id"));
                              });
        }
    }
}