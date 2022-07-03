import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { GetUserDto } from 'src/app/_Models/GetUserDto';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  route:string = '';
  clients:GetUserDto[] = [];
  User:any = null;

  constructor(public Serv:SharedService,public router:Router) {}

  ngOnInit(): void {
    this.Serv.getAllClients().subscribe(d=>{
      this.clients = d;
    });

    for(let client of this.clients){
      if(client.id == this.User.id){
        this.route = '/client/edit/'+this.User.id;
      }else{
        this.route = '/businessowner/edit/'+this.User.id;
      }
    };

  };

}
