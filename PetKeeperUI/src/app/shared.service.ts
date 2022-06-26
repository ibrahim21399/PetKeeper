import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateBusinessDto } from './_Models/CreateBusinessDto';
import { RegisterDto } from './_Models/RegisterDto';
import { LoginDto } from './_Models/LoginDto';
import { GetBusinessDto } from './_Models/GetBusinessDto';
import { ServiceResponse } from './_Models/ServiceResponse';
import { GetUserDto } from './_Models/GetUserDto';
import { DropDownId } from './_Models/DropDownId';
import { DropDownGuid } from './_Models/DropDownGuid';
import { TokenDto } from './_Models/TokenDto';
import { Guid } from 'guid-typescript';
import { BehaviorSubject, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  baseurl = "https://localhost:7293/";

  constructor(public http:HttpClient) { 
    this.currentUserSubject = new BehaviorSubject<any>(JSON.stringify(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  //Business owners crud operations
  getAllBusinessOwners(){
    return this.http.get<GetUserDto[]>(this.baseurl+"api/Admin/GetAllBussinessOwner");
  }

  deleteOwnerClient(user:GetUserDto){
    return this.http.delete(this.baseurl+"api/Admin/DeleteUser?id="+user.id);
  }

  //Clients crud
  getAllClients(){
    return this.http.get<GetUserDto[]>(this.baseurl+"api/Admin/GetAllClients");
  }

  //Business Crud Methods from API
  getAllBusinesses(){
    return this.http.get<ServiceResponse<GetBusinessDto[]>>(this.baseurl+"Business/GetBusiness");
  }

  addBusiness(business:CreateBusinessDto){
    return this.http.post<ServiceResponse<number>>(this.baseurl+"Business/CreateBusiness",business);
  }

  deleteBusiness(business:GetBusinessDto){
    return this.http.delete(this.baseurl+"Business/DeleteBusiness"+business.id);
  }

  //locations
  getAllCities(){
    return this.http.get<ServiceResponse<DropDownId[]>>(this.baseurl+"Home/GetCities");
  }

  getAllAreas(cityId:number){
    return this.http.get<ServiceResponse<DropDownId[]>>(this.baseurl+"Home/GetAreasOfCity?CityId="+cityId);
  }

  getAllServices(){
    return this.http.get<ServiceResponse<DropDownGuid[]>>(this.baseurl+"Home/GetServices");
  }

  FilterBusiness(serviceId:Guid,cityId:number,areaId:number){
    return this.http.post<ServiceResponse<GetBusinessDto[]>>(this.baseurl+"Home/FilterBusiness",[{'serviceId' : serviceId, 'cityId' : cityId , 'areaId' : areaId}]);
    //?ServiceId=2e5c418c-d2d2-4d29-bb75-4689d99776b0&CityId=1&AreaId=1
  }

  //Login and register and logout
  RegisterServ(register:RegisterDto,val:number)
  {
    if(val == 0){
      const formdata = new FormData();
      formdata.append("fullName", register.fullName);
      formdata.append("email", register.email);
      formdata.append("password", register.password);
      formdata.append("confirmPassword", register.confirmPassword);
      formdata.append("phoneNumber", register.phoneNumber);
      formdata.append("userPic", register.userPic ,register.userPic.name);
      return this.http.post<ServiceResponse<number>>(this.baseurl + 'Auth/ClientRegister',formdata);
    }
    else{
      const formdata = new FormData();
      formdata.append("fullName", register.fullName);
      formdata.append("email", register.email);
      formdata.append("password", register.password);
      formdata.append("confirmPassword", register.confirmPassword);
      formdata.append("phoneNumber", register.phoneNumber);
      formdata.append("userPic", register.userPic ,register.userPic.name);
      return this.http.post<ServiceResponse<number>>(this.baseurl + 'Auth/OwnerRegister', formdata);
    }
  }

  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<TokenDto>;

  public get currentUserValue(): TokenDto {
    return this.currentUserSubject.value;
  }

  GetToken(){
    let token = JSON.parse(localStorage.getItem('currentUser')!);
    return token;
  }

  Login(login:LoginDto){
    return this.http.post<ServiceResponse<TokenDto>>(this.baseurl+"Auth/Login",login).pipe(map(user=>{
      localStorage.setItem('currentUser',JSON.stringify(user.data));
      this.currentUserSubject.next(user.data);
      return user;
    }))
  }

  Logout(){
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    return this.http.post<ServiceResponse<number>>(this.baseurl+"Auth/LogOut",[]);
  }
}
