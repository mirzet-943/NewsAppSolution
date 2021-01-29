import { Injectable } from '@angular/core';
import { HttpClient  } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NewsApiService {

  // Add your API key here
  api_key = 'YOUR API KEY';

  constructor(private http: HttpClient) { }

  // function to get list of all news sources
  initSources() {
     return this.http.get(`${environment.apiUrl}/articles?pageNumber=1`);
  }


  // function to get a list of headlines
  initArticles() {
   return this.http.get('https://newsapi.org/v2/top-headlines?sources=techcrunch&apiKey=' + this.api_key);
  }

  // function to get a list of headlines for a user-selected source
  getArticlesByID(source: String) {
   return this.http.get(`${environment.apiUrl}/articles/${source}`);
  }
  
   getNextPage(page: String) {
    return this.http.get(`${environment.apiUrl}/articles?pageNumber=1${page}`);
   }
 
}
