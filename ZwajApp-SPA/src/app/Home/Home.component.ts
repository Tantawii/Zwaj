import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-Home',
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode : boolean = false;
  constructor(private http : HttpClient) { }

  ngOnInit() {
  }
  registerToggle(){
    this.registerMode = ! this.registerMode
  }
  dataRecive(recive : boolean){
    this.registerMode = recive
  }

}
