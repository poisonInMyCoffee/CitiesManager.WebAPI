import { Injectable } from '@angular/core';
import { City } from "../models/city";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  cities: City[] = [];
  constructor(private httpClient: HttpClient) { }


  public getCities(): City[] {
    return this.httpClient.get("https://localhost:7221/api/v1/cities");
  }
}
