import { Guid } from "guid-typescript";
import {  Schedule } from "./schedule";
import { ScheduleDto } from "./ScheduleDto";

export class CreateBusinessDto {
    constructor(
        public id:string,
        public businessName:string,
        public businessDesc:string,
        public businussPhone:string,
        public cityId:number,
        public areaId:number,
        public applicationUserId:Guid,
        public isActive:boolean,
        public serviceId:Guid[],
        public schedules:ScheduleDto[],
        public businessPic:File,
        public licencePic:File
    ){
        this.id = Guid.create().toString();
        // this.applicationUserId = Guid.create();
        // this.serviceId = new Guid[] { new Guid() };
    }
}
