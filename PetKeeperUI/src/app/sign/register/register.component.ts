import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { SharedService } from 'src/app/shared.service';
import { RegisterDto } from 'src/app/_Models/RegisterDto';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  RegisterForm!: FormGroup;
  val:number= 2;
  

  constructor(public regServ:SharedService,public router:Router,public formBuilder: FormBuilder,
    public _sweetalertService: SweetalertService) { 
    this.RegisterForm = this.formBuilder.group({
      fullName: ['', [Validators.required,Validators.minLength(10)]],
      email: ['',[Validators.required,Validators.email]],
      password: ['',[Validators.required,Validators.pattern(/^(?=\D*\d)(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z]).{8,30}$/)]],
      confirmPassword: [''],
      phoneNumber:['',[Validators.required,Validators.pattern('^01[0125][0-9]{8}$')]]},
     {validators: this.MustMatch('password', 'confirmPassword')}
    );

  }

  onSubmit() {
    console.log(this.RegisterForm.value);

    if (this.RegisterForm.invalid) {
      this._sweetalertService.RunAlert(
        'form not valid sholud check all inputs',
        false
      );
      return;
    }
    this.regServ.RegisterServ(this.RegisterForm.value,this.val).subscribe((res) => {
      if (res.success) {
        this._sweetalertService.RunAlert(res.message, true);
        this.router.navigate(['/login']).then(() => {
          window.location.reload();
        });
      } else {
        this._sweetalertService.RunAlert(res.message, false);
      }
    });
  }



  // onChange(event:any) {
  //   this.userPic = event.target.files[0];
  // }

  ngOnInit(): void {
  }

  change(value:any){
    this.val=value;
  }

  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
        // return if another validator has already found an error on the matchingControl
        return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
  get form() {
    return this.RegisterForm.controls;
  }

}
