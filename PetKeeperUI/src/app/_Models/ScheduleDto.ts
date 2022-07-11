import { Guid } from "guid-typescript";

export class ScheduleDto {
    constructor(
        public dayOfWeek:string,
        public startTime:string,
        public endTime:string,
        public businessId:string,
    ){
    }
}

