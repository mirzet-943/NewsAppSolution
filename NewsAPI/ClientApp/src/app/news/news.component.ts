import { Component, HostListener } from '@angular/core';
import { NewsApiService } from './news-api.service';


@Component({
  selector: 'app-root',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent {

  // declare empty arrays for articles and news sources
  mArticles: Array<any>;
  mSources: Array<any>;
  haveMorePages: boolean;
  nextPage: Number;
  constructor(private newsapi: NewsApiService) {
    console.log('news component constructor called');
  }

// tslint:disable-next-line: use-life-cycle-interface
  ngOnInit() {
    // load articles
     this.newsapi.initArticles().subscribe(data => {this.mArticles = data['articles']
        this.haveMorePages = data['hasnext'];
        this.nextPage = data["currentpage"] + 1;
     });
    }

  // function to search for articles based on a news source (selected from UI mat-menu)
  searchArticles(source) {
    console.log('selected source is: ' + source);
    this.newsapi.getArticlesByID(source).subscribe(data => this.mArticles = data['articles']);
  }
  
  @HostListener("window:scroll", ["$event"])
  onWindowScroll() {
  let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
  let max = document.documentElement.scrollHeight;
  if(pos == max && this.haveMorePages)   {
           this.newsapi.getNextPage(this.nextPage.toString()).subscribe(data =>{
            this.mArticles.concat(data['articles'])
            this.haveMorePages = data['hasnext'];
            this.nextPage = data["currentpage"] + 1;
           });
    }
  }
}
