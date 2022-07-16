import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { SharedService } from 'src/app/shared.service';
import { GetBusinessDto } from 'src/app/_Models/GetBusinessDto';

@Component({
  selector: 'app-show-buisness',
  templateUrl: './show-buisness.component.html',
  styleUrls: ['./show-buisness.component.css']
})
export class ShowBuisnessComponent implements OnInit {

  Businesses:GetBusinessDto[] = [];
  role:any = "";

  imgURL:string = 'https://localhost:7293/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  constructor(public busServ:SharedService, public router:Router,private sanitizer: DomSanitizer, public _sweetalertService: SweetalertService) { }

  ngOnInit(): void {
    this.busServ.getAllBusinesses().subscribe(a=>{this.Businesses = a.data });
    console.log("inside business list");
    console.log(this.Businesses);
    console.log(this.Businesses[1]);
    this.role = localStorage.getItem('Role');
    this.role = this.role.replace(/['"]+/g, '');
  }

  delete(id:Guid){
    this.busServ.deleteBusiness(id).subscribe(a=>{
      if (a.success) {
        this._sweetalertService.RunAlert(a.message, true);
        this.router.navigate(['/business/show']);

      } else {
        this._sweetalertService.RunAlert(a.message, false);
      }
    })
  }

}
