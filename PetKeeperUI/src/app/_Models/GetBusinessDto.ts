import { Guid } from "guid-typescript";

export class GetBusinessDto {
    constructor(
        public  id : Guid,
        public  businessName: string,
        public  businessDesc: string,
        public  businussPhone: string,
        public  mangerName : string,
        public  cityName : string,
        public  areaName : string,
        public businessRate : number,
        public services : string[],
        public  businessPic : string,
    ) {
        this.id = Guid.create();
    }
}
