import { Guid } from "guid-typescript";

export class Schedule {
    constructor(
        public id:Guid,
        public created_Date:Date,
        public dayOfWeek:string,
        public startTime:string,
        public endTime:string,
        public businessId:Guid,
    ){
        this.businessId = Guid.create();
        this.id = Guid.create();
    }
}
