import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateBusinessDto } from './_Models/CreateBusinessDto';
import { RegisterDto } from './_Models/RegisterDto';
import { LoginDto } from './_Models/LoginDto';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  baseurl = "https://localhost:7293/";

  constructor(public http:HttpClient) { }

  //Business Crud Methods from API
  getAllBusinesses(){
    return this.http.get<CreateBusinessDto[]>(this.baseurl+"Business/GetBusiness");
  }

  addBusiness(business:CreateBusinessDto){
    return this.http.post<CreateBusinessDto>(this.baseurl+"Business/CreateBusiness",business);
  }

  deleteBusiness(business:CreateBusinessDto){
    return this.http.delete(this.baseurl+"Business/DeleteBusiness"+business.id);
  }

  //locations
  getAllCities(){
    return this.http.get<[]>(this.baseurl+"Home/GetCities");
  }

  getAllAreas(id:number){
    return this.http.get<[]>(this.baseurl+"Home/GetAreasOfCity/"+id);
  }

  getAllServices(){
    return this.http.get<string[]>(this.baseurl+"Home/GetServices");
  }

  //Login and register
  isAuthenticated:boolean = true;
  RegisterServ(register:RegisterDto,val:number)
  {
    if(val == 0){
      this.isAuthenticated = true;
      return this.http.post<RegisterDto[]>(this.baseurl + 'Auth/ClientRegister', register);
    }
    else{
      this.isAuthenticated = true;
      return this.http.post<RegisterDto[]>(this.baseurl + 'Auth/OwnerRegister', register);
    }
  }

  getauth(){
    return this.isAuthenticated;
  }

  ad:LoginDto = new LoginDto('','');
  
  Login(email:string){
    this.http.get<LoginDto>(this.baseurl+email).subscribe(d=>
      this.ad = d)
    return this.ad;
  }

}
