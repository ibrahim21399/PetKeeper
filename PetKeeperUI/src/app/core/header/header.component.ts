import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { ApplicationUserDto } from 'src/app/_Models/ApplicationUserDto';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnChanges {
 
  auth:boolean = false;
  currentUser: any = null;

  constructor(public authServ:SharedService,public router:Router) { 
    console.log(this.auth);
    this.authServ.currentUser.subscribe(x => this.currentUser = x);
    this.auth = this.authServ.getauth();
    console.log(this.auth);
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.auth = this.authServ.getauth();
    console.log(this.auth);
  }

  ngOnInit(): void {
  }


  logout(){
    this.auth = false;
    console.log(this.auth);
    this.authServ.Logout();
    this.router.navigateByUrl("/");
    console.log(this.auth);
  }

}
