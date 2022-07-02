import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/auth.service';

@Component({
  selector: 'app-Nav',
  templateUrl: './Nav.component.html',
  styleUrls: ['./Nav.component.css']
})
export class NavComponent implements OnInit {
  // Model is a Var To Load "Login Form"
  model : any = { };
  constructor(private AuthService: AuthService) { }

  ngOnInit() {
  }

  //Method To Login
  login(){
    this.AuthService.login(this.model).subscribe(
      next=> {console.log('تم الدخول بنجاح')},
      error => {console.log('فشل في الدخول')}
    )
  }
  loggedIn(){
    const token = localStorage.getItem('token');
    return !! token;
  }
  loggedOut(){
    localStorage.removeItem('token');
    console.log("تـم تسجيل الخروج")
  }

}
