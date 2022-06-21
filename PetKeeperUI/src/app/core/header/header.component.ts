import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
 
  // @Input() val:number = 0;
  auth:boolean = true;
  constructor(public authServ:SharedService) { }

  ngOnInit(): void {
    this.auth = this.authServ.getauth();
  }

}
