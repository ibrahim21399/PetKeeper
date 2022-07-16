import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-approve',
  templateUrl: './approve.component.html',
  styleUrls: ['./approve.component.css']
})
export class ApproveComponent implements OnInit {

  UnApprovedBusinesses:any = null;
  imgURL:string = 'https://localhost:7293/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);
  
  constructor(public busServ:SharedService, public router:Router,private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.busServ.GetAllUnApprovedBusiness().subscribe(d=>{
      console.log(d.message);
      console.log(d.data);
      this.UnApprovedBusinesses = d.data;
    });

    this.busServ.getAllServices().subscribe(d=>{
      console.log(d.data.find(d=> d.id == this.UnApprovedBusinesses[0].id));
    })
  }

  Approve(id:Guid){
    console.log(id);
    this.busServ.ApproveBusiness(id).subscribe(d=>{
      console.log(d.message);
      console.log(d.data);
      this.router.navigate(['/admin']);
    });
  }

  Decline(id:Guid){
    this.busServ.deleteBusiness(id).subscribe(d=>{
      console.log(d.data);
      console.log(d.message);
    })
  }
}
