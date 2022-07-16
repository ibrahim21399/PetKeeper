import { Guid } from "guid-typescript";

export class Schedule {
    constructor(
        public id:Guid,
        public dayOfWeek:string,
        public startTime:string,
        public endTime:string,
        public businessId:Guid,
    ){}
}
