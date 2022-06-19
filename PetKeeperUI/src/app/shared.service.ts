import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateBusinessDto } from './_Models/CreateBusinessDto';

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
    return this.http.get<string[]>(this.baseurl+"Cities/GetCities");
  }

  getAllAreas(){
    return this.http.get<string[]>(this.baseurl+"Areas/GetAreasOfCity");
  }

  getAllServices(){
    return this.http.get<string[]>(this.baseurl+"Home/GetServices");
  }

}
