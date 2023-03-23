import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { single } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GiphyService {
  private baseUrl: string;

  constructor(private http: HttpClient) { 
    this.baseUrl = environment.apiUrl;
  }
  public get() {
    return this.http.get<response>(this.baseUrl);
  }

  public getByQuery(q: string) {
    let queryUrl = this.baseUrl + q;
    return this.http.get<response>(queryUrl);
  }
}

export interface response {
  data: singleGif[]
}

export interface singleGif {
  siteUrl: string,
  title: string,
  imageUrl: string
}
