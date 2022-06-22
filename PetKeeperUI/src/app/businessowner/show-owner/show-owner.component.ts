import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { GetUserDto } from 'src/app/_Models/GetUserDto';

@Component({
  selector: 'app-show-owner',
  templateUrl: './show-owner.component.html',
  styleUrls: ['./show-owner.component.css']
})
export class ShowOwnerComponent implements OnInit {

  BusinessOwners:GetUserDto[] = [];

  constructor(public busServ:SharedService, public router:Router) { }

  ngOnInit(): void {
    this.busServ.getAllBusinessOwners().subscribe(a=>{this.BusinessOwners = a });
    console.log("inside business owners list");
    console.log(this.BusinessOwners);
    console.log(this.BusinessOwners[1]);
  }

  delete(b:GetUserDto){
    this.busServ.deleteOwnerClient(b).subscribe(a=>{
      console.log("deleted");
      this.router.navigate(['/businessowner']);
    })
  }
}
