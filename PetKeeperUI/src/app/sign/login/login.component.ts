import { HttpErrorResponse, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Component, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, first, Observable, Subscription, throwError } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { LoginDto } from 'src/app/_Models/LoginDto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email:string = '';
  password:string = '';
  ad:LoginDto = new LoginDto('','');
  auth:any = null;

  constructor(public router:Router, public ar:ActivatedRoute, public adServ:SharedService) {
  }

  login(){
    this.ad.email = this.email;
    this.ad.password = this.password;
    console.log(this.ad);
    this.adServ.Login(this.ad).pipe(first()).subscribe(d=>{
      console.log(d.message);
      console.log(d.data.currentUser.fullName);
      this.router.navigate(['/home']).then(() => {
        window.location.reload();
      });
    })
  }

  ngOnInit(): void {
  }

}
