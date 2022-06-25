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
  
  shortLink: string = "";
  loading: boolean = false;
  
  constructor(public regServ:SharedService,public router:Router) { }

  Register(){
    this.regist.fullName = this.fullName;
    this.regist.email = this.email;
    this.regist.password = this.password;
    this.regist.confirmPassword = this.confirmPassword;
    this.regist.phoneNumber = this.phoneNumber;
    this.regist.userPic = this.userPic;
    console.log(this.regist);
    this.loading = !this.loading;
    console.log(this.userPic);
    this.regServ.RegisterServ(this.regist,this.val).subscribe(a=>{
      console.log("inside regist component"+a.data+a.message);
      (event: any) => {
        if (typeof (event) === 'object') {
            this.shortLink = event.link;
            this.loading = false;
            console.log(this.userPic);
        }
    }
    });
  }

  onChange(event:any) {
    this.userPic = event.target.files[0];
  }

  ngOnInit(): void {
  }

}







// import { HttpClient } from '@angular/common/http';
// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { SharedService } from 'src/app/shared.service';
// import { RegisterDto } from 'src/app/_Models/RegisterDto';

// @Component({
//   selector: 'app-register',
//   templateUrl: './register.component.html',
//   styleUrls: ['./register.component.css']
// })
// export class RegisterComponent implements OnInit {

//   constructor(public regServ:SharedService,public router:Router,httpClient: HttpClient) { }

//   ngOnInit(): void {
//   }

//   val:number= 0;

//   regist:RegisterDto = new RegisterDto('','','','','','');

//   fullName:string = "";
//   email:string = "";
//   password:string =  "";
//   confirmPassword:string =  "";
//   phoneNumber:string = "";
//   userPic:string =  "";

//   selectedFile: File|null = null;
//   submissionResult: StudentFormSubmissionResult;

//   chooseFile(files: FileList) {
//     // ...
//     this.selectedFile = files[0];
//   }

//   upload() {
//     // validations not shown
//     const formData = new FormData();
//     formData.append('userPic', this.selectedFile);
//     formData.append('formId', this.formId.toString());
//     this.courses.forEach((c) => {
//       formData.append('courses', c);
//     });

//     const req = new HttpRequest(
//       'POST',
//       `api/students/${this.studentId}/forms`,
//       formData,
//       {
//         reportProgress: true,
//       }
//     );

//     this.uploading = true;
//     this.httpClient
//       .request<StudentFormSubmissionResult>(req)
//       .pipe(
//         finalize(() => {
//           this.uploading = false;
//           this.selectedFile = null;
//         })
//       )
//       .subscribe(
//         (event) => {
//           if (event.type === HttpEventType.UploadProgress) {
//             this.uploadProgress = Math.round(
//               (100 * event.loaded) / event.total
//             );
//           } else if (event instanceof HttpResponse) {
//             this.submissionResult = event.body;
//           }
//         },
//         (error) => {
//           // Here, you can either customize the way you want to catch the errors
//           throw error; // or rethrow the error if you have a global error handler
//         }

//   // Register(){
//   //   this.regist.fullName = this.fullName;
//   //   this.regist.email = this.email;
//   //   this.regist.password = this.password;
//   //   this.regist.confirmPassword = this.confirmPassword;
//   //   this.regist.phoneNumber = this.phoneNumber;
//   //   this.regist.userPic = this.userPic;
//   //   console.log(this.regist);
//   //   this.regServ.RegisterServ(this.regist,this.val).subscribe(a=>{console.log("inside regist component"+a)});
//   // }

// }
