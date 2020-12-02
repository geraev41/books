import gql from 'graphql-tag';

export const BOOKS_QUERY = gql`
   query books($name: String, $order : Boolean){
        books(name: $name, order : $order){
            id
            name
            description
            price
            authorId
        }
    }
`;