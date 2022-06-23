import { Component, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { LoginDto } from 'src/app/_Models/LoginDto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public router:Router, public ar:ActivatedRoute, public adServ:SharedService) { }

  email:string = '';
  password:string = '';

  ad:LoginDto = new LoginDto('','');

  sub:Subscription|null = null;
  sub2:Subscription|null = null;

  login(){
    this.adServ.Login(this.ad).subscribe(a=>{
      console.log(a.message);
    })
    // this.router.navigateByUrl("/home");
  }
  ngOnInit(): void {
  }

}
