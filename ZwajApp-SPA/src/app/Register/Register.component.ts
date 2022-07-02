import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../_Services/auth.service';

@Component({
  selector: 'app-Register',
  templateUrl: './Register.component.html',
  styleUrls: ['./Register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() dataEvent = new EventEmitter();
  model: any = {};
  constructor(private authService : AuthService) { }

  ngOnInit() {
  }
  register() {
    this.authService.register(this.model).subscribe(
      ()=>{console.log('تم الاشتراك بنجاح')},
      error => {console.log(error)}
    )
  }
  cancel() {
    console.log('ليس الأن');
    this.dataEvent.emit(false);


  }

}
