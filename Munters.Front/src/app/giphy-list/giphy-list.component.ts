import { Component, OnInit } from '@angular/core';
import { GiphyService, singleGif } from '../giphy.service';

@Component({
  selector: 'app-giphy-list',
  templateUrl: './giphy-list.component.html',
  styleUrls: ['./giphy-list.component.css']
})
export class GiphyListComponent implements OnInit {
  giphyList: singleGif[] = [];
  constructor(private giphyService: GiphyService) { }

  ngOnInit(): void {
    this.getGiphies();
  }

  private getGiphies() {
    this.giphyService.get()
      .subscribe(g => {
        this.giphyList = g.data;
        // console.log(g.data);
    });
  }
  onKeyDown($event) {
    console.log($event.target.value);
    this.giphyService.getByQuery($event.target.value)
      .subscribe(g => {
        this.giphyList = g.data;
      });
  }
}
