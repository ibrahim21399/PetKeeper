import { Guid } from "guid-typescript";

export class Schedule {
    constructor(
        public dayOfWeek:string,
        public startTime:string,
        public endTime:string,
        public businessId:Guid,
    ){
        // this.businessId = Guid.create();
    }
}
