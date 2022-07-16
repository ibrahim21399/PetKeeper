import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
import { UserDto } from './_Models/UserDto';
import { GetUserAccountDto } from './_Models/GetUserAccountDto';
import { CreateCommentDto } from './_Models/CreateCommentDto';
import { GetBOwnerBookingDto } from './_Models/GetBOwnerBookingDto';
import { GetBookingDto } from './_Models/GetBookingDto';
import { GetAdminBusinessDetailsDto } from './_Models/GetAdminBusinessDetailsDto';
import { Schedule } from './_Models/schedule';
import { Comments } from './_Models/comments';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { formatDate } from '@angular/common';
import { EventListenerFocusTrapInertStrategy } from '@angular/cdk/a11y';


@Injectable({
  providedIn: 'root'
})
export class SharedService {
  baseurl = "https://localhost:7293/";

  constructor(public http:HttpClient,public _sw:SweetalertService) { 
    this.currentUserSubject = new BehaviorSubject<any>(JSON.stringify(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  //Business owners crud operations

  getAllBusinessOwners(){
    return this.http.get<GetUserDto[]>(this.baseurl+"api/Admin/GetAllBussinessOwner");
  }

  deleteUser(id:Guid){
    return this.http.delete<ServiceResponse<number>>(this.baseurl+"api/Admin/DeleteUser?id="+id);
  }

  //Clients crud
  getAllClients(){
    return this.http.get<GetUserDto[]>(this.baseurl+"api/Admin/GetAllClients");
  }

  getUser(){
    return this.http.get<ServiceResponse<GetUserAccountDto>>(this.baseurl+"Account");
  }

  EditUser(userDto:UserDto){
    const formdata = new FormData();
    formdata.append("fullName", userDto.fullName);
    formdata.append("email", userDto.email);
    formdata.append("phoneNumber", userDto.phoneNumber);
    formdata.append("userPic", userDto.userPic ,userDto.userPic.name);

    return this.http.put<ServiceResponse<number>>(this.baseurl+"Account/Edit",formdata);
  }

  ChangePassword(current:string,newPass:String){
    const formdata = new FormData();
    formdata.append("current", current);
    formdata.append("newPass", newPass+"");

    return this.http.post<ServiceResponse<number>>(this.baseurl+"Account/ChangePassword",formdata);
  }
  DeleteAccount(){
   var x= this.http.delete<ServiceResponse<number>>(this.baseurl+"Account/DeleteMyAccount");
    return x;
  }
  //Business Crud Methods from API
  getAllBusinesses(){
    return this.http.get<ServiceResponse<GetBusinessDto[]>>(this.baseurl+"Business/GetBusiness");
  }

  getBusinessDetails(id:Guid){
    return this.http.get<ServiceResponse<GetAdminBusinessDetailsDto>>(this.baseurl+"api/Admin/GetBusinessDetails?id="+id);
  }

  addBusiness(business:CreateBusinessDto):Observable<ServiceResponse<number>>{
    const formdata = new FormData();
      formdata.append("businessName", business.businessName);
      formdata.append("businessDesc", business.businessDesc);
      formdata.append("businussPhone", business.businussPhone);
      formdata.append("cityId", business.cityId+"");
      formdata.append("areaId", business.areaId+"");

      business.serviceId.forEach(element=>{
        formdata.append("serviceId",element+"");
      });
      formdata.append("businessPic", business.businessPic ,business.businessPic.name);
      formdata.append("licencePic", business.licencePic ,business.licencePic.name);
        business.schedules.forEach(a=>{
          console.log(a)
          var s ={
            'dayOfWeek': a.dayOfWeek,
            'startTime': a.startTime,
            'endTime': a.endTime,
            'businessId': a.businessId
          }
          console.log(s);
          formdata.append("schedules",JSON.stringify(s));
        });

      


      return this.http.post<ServiceResponse<number>>(this.baseurl+'Business/CreateBusiness',formdata);
    

  }

  deleteBusiness(id:Guid){
    return this.http.delete<ServiceResponse<number>>(this.baseurl+"Business/DeleteBusiness?Id="+id);
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
      // const formdata = new FormData();
      // formdata.append("fullName", register.fullName);
      // formdata.append("email", register.email);
      // formdata.append("password", register.password);
      // formdata.append("confirmPassword", register.confirmPassword);
      // formdata.append("phoneNumber", register.phoneNumber);
      // formdata.append("userPic", register.userPic ,register.userPic.name);
      return this.http.post<ServiceResponse<number>>(this.baseurl + 'Auth/ClientRegister',register);
    }
     else{
      // const formdata = new FormData();
      // formdata.append("fullName", register.fullName);
      // formdata.append("email", register.email);
      // formdata.append("password", register.password);
      // formdata.append("confirmPassword", register.confirmPassword);
      // formdata.append("phoneNumber", register.phoneNumber);
      // formdata.append("userPic", register.userPic ,register.userPic.name);
      return this.http.post<ServiceResponse<number>>(this.baseurl + 'Auth/OwnerRegister', register);
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
    var x=this.http.post<ServiceResponse<TokenDto>>(this.baseurl+"Auth/Login",login);
    x.subscribe(d=>{
      if(d.success==false)
      this._sw.RunAlert(d.message,false);
      else this._sw.RunAlert(d.message,true);
    })
     return x.pipe(map(user=>{
      localStorage.setItem('currentUser',JSON.stringify(user.data.token));
      localStorage.setItem('Role',JSON.stringify(user.data.role));
      this.currentUserSubject.next(user.data);
      return user;
    }))
  }

  Logout(){
    localStorage.removeItem('currentUser');
    localStorage.removeItem('Role');
    this.currentUserSubject.next(null);
    return this.http.post<ServiceResponse<number>>(this.baseurl+"Auth/LogOut",[]);
  }

  //Comments
  AddComment(busId:Guid,Comment:CreateCommentDto){
    return this.http.post<ServiceResponse<number>>(this.baseurl+"Client/CommentOnBusiness/"+busId,Comment);
  };

  DeleteComment(commentId:Guid){
    return this.http.delete<ServiceResponse<number>>(this.baseurl+"Client/DeleteComment?CommentId="+commentId);
  };

  Book(date:Date){
    return this.http.post<ServiceResponse<number>>(this.baseurl+"Client/BookAppoienment",date);
  };

  //admin approve business
  GetAllUnApprovedBusiness(){
    return this.http.get<ServiceResponse<GetBusinessDto[]>>(this.baseurl+"api/Admin/GetAllUnApprovedBusiness");
  }

  ApproveBusiness(busid:Guid){
    const formdata =new FormData();
    formdata.append("busid",busid+"");

    return this.http.post<ServiceResponse<number>>(this.baseurl+"api/Admin/ApproveBusiness",formdata);
  }

  //businessowner approve booking
  GetAllUnApprovedAppointments(){
    return this.http.get<ServiceResponse<GetBOwnerBookingDto[]>>(this.baseurl+"Business/GetAllUnApprovedAppoientments");
  }

  AcceptBooking(bookId:Guid){
    return this.http.post<ServiceResponse<number>>(this.baseurl+"Business/AcceptBooking",bookId);
  } 
  
  DeclineBooking(bookId:Guid){
    return this.http.post<ServiceResponse<number>>(this.baseurl+"Business/CancelBooking",bookId);
  }

  //client appointments
  GetAppointments(){
    return this.http.get<ServiceResponse<GetBookingDto[]>>(this.baseurl+"Client/GetAppoientments");
  }
  scroll(el: HTMLElement) {
    el.scrollIntoView();
  }

  GetAllSchedule(busId:Guid){
    return this.http.get<ServiceResponse<Schedule[]>>(this.baseurl+"Client/GetAllSchedule?busId="+busId);
  }

  GetComment(busId:Guid){
    return this.http.get<ServiceResponse<Comments[]>>(this.baseurl+"Client/GetAllcomments?busId="+busId);
  }
}
