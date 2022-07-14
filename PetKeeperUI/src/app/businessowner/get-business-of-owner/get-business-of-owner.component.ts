import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { SharedService } from 'src/app/shared.service';
import { GetBusinessDto } from 'src/app/_Models/GetBusinessDto';

@Component({
  selector: 'app-get-business-of-owner',
  templateUrl: './get-business-of-owner.component.html',
  styleUrls: ['./get-business-of-owner.component.css']
})
export class GetBusinessOfOwnerComponent implements OnInit {

  Business:GetBusinessDto[] = [];
  imgURL:string = 'https://localhost:7293/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);
  constructor(public busServ:SharedService, public router:Router,private sanitizer: DomSanitizer, public _sweetalertService: SweetalertService) {
    this.busServ.getAllBusinesses().subscribe(a=>{
     this.Business=a.data;
      console.log(this.Business[0].businessPic)
    });
   }

  ngOnInit(): void {
console.log(this.thumbnail);
  }

  delete(b:Guid){
    this.busServ.deleteBusiness(b).subscribe(a=>{
      if (a.success) {
        this._sweetalertService.RunAlert(a.message, true);
        this.router.navigate(['/']).then(() => {
          window.location.reload();
        });
      } else {
        this._sweetalertService.RunAlert(a.message, false);
      }
    });
  }



}
