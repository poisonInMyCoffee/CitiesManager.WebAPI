import { Component } from '@angular/core';
import { City } from '../models/city';
import { CitiesService } from '../services/cities.service';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent {
  cities: City[] = [];

  constructor(private citiesService: CitiesService) {
  }
  //seeding data from asp.net to angular
  ngOnInit() {
   this.citiesService.getCities().subscribe(
      (response: City[]) => {
        this.cities = response;
      },
     (error: any) => {
        console.log(error)     },
      () => { }

    );
  }
}
