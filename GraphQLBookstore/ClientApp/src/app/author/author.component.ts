import { Component } from '@angular/core';
import { AUTHORS_QUERY } from './queries';
import { CREATE_AUTHOR, UPDATE_AUTHOR, DELETE_AUTHOR } from './mutations';
import { Author } from './author.inteface';
import { Apollo } from 'apollo-angular';
import {Router} from '@angular/router';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.css']
})
export class AuthorComponent {

  authors: Author[];
  name_filter = '';
  is_editting = false;
  currentAuthor: Author;
  constructor(private apollo: Apollo , private router: Router) {
    this.currentAuthor = { id: -1, name: '' };
    this.filter();
  }

  edit(author: Author){
    this.currentAuthor = {...author};
    this.is_editting = true;
  }

  delete(author: Author){
    this.apollo.mutate({
      mutation: DELETE_AUTHOR,
      variables: { id: author.id }
    }).subscribe(() => {
      this.currentAuthor = { id: -1, name: '' };
      this.is_editting = false;
      this.filter();
    });
  }

  save() {
    let mutation = CREATE_AUTHOR;
    const variables = {
      input: { name: this.currentAuthor.name }
    };
    if(this.currentAuthor.id > 0){
      variables['id'] = this.currentAuthor.id;
      mutation = UPDATE_AUTHOR;
    }
    this.apollo.mutate({
      mutation: mutation,
      variables: variables
    }).subscribe(() => {
      this.currentAuthor = { id: -1, name: '' };
      this.is_editting = false;
      this.filter();
    });
  }

  filter() {
    this.apollo.watchQuery({
      query: AUTHORS_QUERY,
      fetchPolicy: 'network-only',
      variables: {
        name: this.name_filter
      }
    }).valueChanges.subscribe(result => {
      this.authors = result.data.authors;
    });
  }

  addBook(Author : Author){
    var url = 'https://localhost:5001/books/'+Author.id; 
    window.open(url); 
   
  }

}
