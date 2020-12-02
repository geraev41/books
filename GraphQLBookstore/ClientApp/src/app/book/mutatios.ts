import gql from 'graphql-tag';

export const CREATE_BOOK = gql`
    mutation createBook ($input: BookInput!){
        createBook(input: $input){
            id
            name
            price
        }
    }
`;
export const UPDATE_BOOK = gql`
    mutation updateBook($id: ID!, $input: BookInput!) {
        updateBook(id: $id, input: $input) {
            id
            name
        }
    }
`;

export const DELETE_BOOK = gql`
    mutation($id: ID!) {
        deleteBook(id: $id) {
            id
        }
    }
`;