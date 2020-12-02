using GraphQL.Types;
using GraphQLBookstore.Models;

namespace  GraphQLBookstore.GraphQL.Types 
{
    class BookInputType : InputObjectGraphType
    {
        public BookInputType()
        {
            Name = "BookInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<NonNullGraphType<FloatGraphType>>("price");
            Field<NonNullGraphType<IntGraphType>>("authorId");
        }
    }
}