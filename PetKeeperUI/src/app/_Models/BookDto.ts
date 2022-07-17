import { Guid } from "guid-typescript";

export class BookDto {
    constructor(
        public bookDate:Date,
        public busId:Guid ,
        public scheduleId :Guid ,
    ) {}
}
