import { Guid } from "guid-typescript";
import { Schedule } from "./schedule";

export class CreateBusinessDto {
    constructor(
        public id:Guid,
        public businessName:string,
        public businessDesc:string,
        public businussPhone:string,
        public cityId:number,
        public areaId:number,
        public applicationUserId:Guid,
        public isActive:boolean,
        public serviceId:Guid[],
        public schedules:Schedule[],
        public businessPic:File,
        public licencePic:File
    ){
        this.id = Guid.create();
        this.applicationUserId = Guid.create();
        // this.serviceId = new Guid[] { new Guid() };
    }
}
