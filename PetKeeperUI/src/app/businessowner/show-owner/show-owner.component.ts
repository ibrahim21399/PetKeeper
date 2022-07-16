import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SharedService } from 'src/app/shared.service';
import { GetUserDto } from 'src/app/_Models/GetUserDto';

@Component({
  selector: 'app-show-owner',
  templateUrl: './show-owner.component.html',
  styleUrls: ['./show-owner.component.css']
})
export class ShowOwnerComponent implements OnInit {

  BusinessOwners:GetUserDto[] = [];
  // imgURL:string = 'https://localhost:7293/';
  // thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  constructor(public busServ:SharedService, public router:Router,private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.busServ.getAllBusinessOwners().subscribe(a=>{this.BusinessOwners = a 
    });
    console.log("inside business owners list");
    console.log(this.BusinessOwners);
    console.log(this.BusinessOwners[1]);
  }

  delete(id:Guid){
    this.busServ.deleteUser(id).subscribe(a=>{
      console.log("deleted");
      this.router.navigate(['/businessowner']);
    })
  }
}
