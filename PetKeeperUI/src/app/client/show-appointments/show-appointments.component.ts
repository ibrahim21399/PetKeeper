import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-appointments',
  templateUrl: './show-appointments.component.html',
  styleUrls: ['./show-appointments.component.css']
})
export class ShowAppointmentsComponent implements OnInit {

  ClientBookings:any = null;
  imgURL:string = 'https://localhost:7293/UsersPic/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  constructor(public busServ:SharedService, public router:Router,private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.busServ.GetAllUnApprovedAppointments().subscribe(d=>{
      console.log(d.message);
      console.log(d.data);
      this.ClientBookings = d.data;
    });
  }

}
