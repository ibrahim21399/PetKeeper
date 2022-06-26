import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { RegisterDto } from 'src/app/_Models/RegisterDto';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  val:number= 0;
  file:any = [];
  regist:RegisterDto = new RegisterDto('','','','','',this.file);
  fullName:string = "";
  email:string = "";
  password:string =  "";
  confirmPassword:string =  "";
  phoneNumber:string = "";
  pb:BlobPart[] = [];
  userPic:any=  '';
  

  constructor(public regServ:SharedService,public router:Router) { }

  Register(){
    this.regist.fullName = this.fullName;
    this.regist.email = this.email;
    this.regist.password = this.password;
    this.regist.confirmPassword = this.confirmPassword;
    this.regist.phoneNumber = this.phoneNumber;
    this.regist.userPic = this.userPic;
    console.log(this.regist);
    console.log(this.userPic);
    this.regServ.RegisterServ(this.regist,this.val).subscribe(d=>{
      console.log(d.message);
      this.router.navigate(['/home']).then(() => {
        window.location.reload();
      });
    })
  }

  onChange(event:any) {
    this.userPic = event.target.files[0];
  }

  ngOnInit(): void {
  }

}
