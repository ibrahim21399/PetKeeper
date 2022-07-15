import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  currentpass:string = '';
  newpass:string = '';




  constructor(public Serv:SharedService,public router:Router,  public _sweetalertService: SweetalertService) { }

  Edit(){
    console.log(this.currentpass)
    console.log(this.newpass)

    this.Serv.ChangePassword(this.currentpass,this.newpass).subscribe(d=>{
      if (d.success) {
        this._sweetalertService.RunAlert(d.message, true);
      } 
      else {
this._sweetalertService.RunAlert(d.message,false);
      }
    })


  }

  
  ngOnInit(): void {

  }
}