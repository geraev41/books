import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import {BOOKS_QUERY} from './queries';
import {Book} from './book.interface';
import { CREATE_BOOK,DELETE_BOOK, UPDATE_BOOK} from './mutatios';


@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  private books: Book[]; 
  private book: Book; 
  private  is_editting = false;
  private name_filter = '';
  constructor(private apollo: Apollo) {
    this.filter(); 
    this.newBook(); 
   }

  ngOnInit(): void {
  }
newBook(){
  this.book = {
    id :0,
    name: '',
    description: '',
    price: 0,
    authorId: 2

  }
}

  save() {
    const variables = {
      input: { name: this.book.name, description: this.book.description, price: this.book.price, 
      authorId: this.book.authorId }
    };
    
    this.apollo.mutate({
      mutation: CREATE_BOOK,
      variables: variables
    }).subscribe(() => {
      this.filter();
      this.newBook(); 
    });
  }

  filter() {
    this.apollo.watchQuery({
      query:  BOOKS_QUERY,
      fetchPolicy: 'network-only',
      variables: {
        name: this.name_filter
      }
    }).valueChanges.subscribe(result => {
      this.books = result.data.books;
    });
  }

  delete(b: Book){
    this.apollo.mutate({
      mutation: DELETE_BOOK,
      variables: { id: b.id }
    }).subscribe(() => {
      this.is_editting = false;
      this.filter();
    });
  }

  edit(b: Book){
    this.is_editting = true;
    this.book = {...b};
    console.log(this.book); 
    const variables = {
      input: { name: this.book.name, description: this.book.description, price: this.book.price, 
      authorId: this.book.authorId }
    };
    variables['id'] = this.book.id;
    this.apollo.mutate({
      mutation: UPDATE_BOOK,
      variables: variables
    }).subscribe(() => {
      this.filter();
      this.newBook(); 
    });

  }
}
