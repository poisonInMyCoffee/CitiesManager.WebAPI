import { Injectable } from '@angular/core';
import { City } from "../models/city";

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  cities: City[] = [];
  constructor() {
    this.cities = [
      new City("101","Delhi"),
      new City("102","Noida"),
      new City("103","Jaipur"),
      new City("104","Lucknow"),
    ];
  }
}
