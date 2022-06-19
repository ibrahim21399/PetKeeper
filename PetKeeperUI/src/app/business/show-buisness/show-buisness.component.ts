import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { CreateBusinessDto } from '../../_Models/CreateBusinessDto';

@Component({
  selector: 'app-show-buisness',
  templateUrl: './show-buisness.component.html',
  styleUrls: ['./show-buisness.component.css']
})
export class ShowBuisnessComponent implements OnInit {

  Businesses:CreateBusinessDto[] = [];

  constructor(public busServ:SharedService, public router:Router) { }

  ngOnInit(): void {
    this.busServ.getAllBusinesses().subscribe(a=>{this.Businesses = a });
    console.log("inside business list");
    console.log(this.Businesses);
  }

  delete(b:CreateBusinessDto){
    this.busServ.deleteBusiness(b).subscribe(a=>{
      console.log("deleted");
      // this.router.navigate(['/Business/GetBusiness']);
    })
  }

}
