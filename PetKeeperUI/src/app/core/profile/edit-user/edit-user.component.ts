import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { SharedService } from 'src/app/shared.service';
import { UserDto } from 'src/app/_Models/UserDto';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  id:Guid = Guid.create();
  email:string = '';
  phoneNumber:string = '';
  fullName:string = '';
  userPic!:any ;
  password:string = '';
  oldps:string = '';

  User:UserDto = new UserDto(this.id,'','','','',this.userPic);

  src:string = 'https://localhost:7293/UsersPic/'+this.userPic;

  constructor(public Serv:SharedService,public router:Router,  public _sweetalertService: SweetalertService) { }

  Edit(){
    // this.oldps = this.User;
    this.User.email = this.email;
    this.User.phoneNumber = this.phoneNumber;
    this.User.fullName = this.fullName;
    this.User.userPic = this.userPic;
    console.log(this.User);
    this.Serv.EditUser(this.User).subscribe(d=>{
      if (d.success) {
        this._sweetalertService.RunAlert(d.message, true);
        this.router.navigate(['profile']).then(() => {
          window.location.reload();
        });
      } else {
        this._sweetalertService.RunAlert(d.message, false);
      }
    })


  }

  onChange(event:any){
    this.userPic = event.target.files[0];
  }
  
  ngOnInit(): void {
    this.Serv.getUser().subscribe(d=>{
      this.fullName = d.data.fullName,
      this.email = d.data.email,
      this.phoneNumber = d.data.phoneNumber,
      this.userPic = d.data.userPic,
      console.log(d.data);
      console.log(d.message);
    })
    console.log(this.User);
  }
}
