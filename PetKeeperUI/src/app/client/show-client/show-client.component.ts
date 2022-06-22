import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { GetUserDto } from 'src/app/_Models/GetUserDto';

@Component({
  selector: 'app-show-client',
  templateUrl: './show-client.component.html',
  styleUrls: ['./show-client.component.css']
})
export class ShowClientComponent implements OnInit {

  Clients:GetUserDto[] = [];
  
  constructor(public busServ:SharedService, public router:Router) { }

  ngOnInit(): void {
    this.busServ.getAllClients().subscribe(a=>{this.Clients = a });
    console.log("inside Clients list");
    console.log(this.Clients);
    console.log(this.Clients[1]);
  }

  delete(b:GetUserDto){
    this.busServ.deleteOwnerClient(b).subscribe(a=>{
      console.log("deleted");
      this.router.navigate(['/client']);
    })
  }

}
