import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { GetBusinessDto } from 'src/app/_Models/GetBusinessDto';

@Component({
  selector: 'app-show-buisness',
  templateUrl: './show-buisness.component.html',
  styleUrls: ['./show-buisness.component.css']
})
export class ShowBuisnessComponent implements OnInit {

  Businesses:GetBusinessDto[] = [];
  imgURL:string = 'https://localhost:7293/UsersPic/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  constructor(public busServ:SharedService, public router:Router,private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.busServ.getAllBusinesses().subscribe(a=>{this.Businesses = a.data });
    console.log("inside business list");
    console.log(this.Businesses);
    console.log(this.Businesses[1]);
  }

  delete(b:GetBusinessDto){
    this.busServ.deleteBusiness(b).subscribe(a=>{
      console.log("deleted");
      this.router.navigate(['/business']);
    })
  }

}
