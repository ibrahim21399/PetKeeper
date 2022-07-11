import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-approve-appointment',
  templateUrl: './approve-appointment.component.html',
  styleUrls: ['./approve-appointment.component.css']
})
export class ApproveAppointmentComponent implements OnInit {

  UnApprovedAppointments:any = null;
  imgURL:string = 'https://localhost:7293/UsersPic/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  constructor(public busServ:SharedService, public router:Router,private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.busServ.GetAllUnApprovedAppointments().subscribe(d=>{
      console.log(d.message);
      console.log(d.data);
      this.UnApprovedAppointments = d.data;
    });

  }

  Approve(id:Guid){
    this.busServ.AcceptBooking(id).subscribe(d=>{
      console.log(d.data);
      console.log(d.message);
      this.router.navigate(["/businessowner/approve"]);
    })
  }

  Decline(id:Guid){
    this.busServ.DeclineBooking(id).subscribe(d=>{
      console.log(d.data);
      console.log(d.message);
      this.router.navigate(["/businessowner/approve"]);
    })
  }
}
