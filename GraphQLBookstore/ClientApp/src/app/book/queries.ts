import gql from 'graphql-tag';

export const BOOKS_QUERY = gql`
   query books($name: String, $description: String){
        books(name: $name, description : $description){
            id
            name
            description
            price
            authorId
        }
    }
`;