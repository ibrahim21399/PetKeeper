import { Guid } from "guid-typescript";

export class FilterDto {
    constructor(
        public  serviceId?:Guid,
        public  cityId?:number,
        public  areaId?:number
    ) {}
}
