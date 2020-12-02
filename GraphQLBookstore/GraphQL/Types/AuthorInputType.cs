using GraphQL.Types;
using GraphQLBookstore.Models;

namespace  GraphQLBookstore.GraphQL.Types 
{
    class AuthorInputType : InputObjectGraphType
    {
        public AuthorInputType()
        {
            Name = "AuthorInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}