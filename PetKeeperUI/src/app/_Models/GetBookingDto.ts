import { Guid } from "guid-typescript";

export class GetBookingDto {
    constructor(
        public id:Guid,
        public businessName:string,
        public bookDate:Date,
        public appoientmentState:string,
    ) {}
}
