import { Guid } from "guid-typescript";

export class GetBOwnerBookingDto {
    constructor(
        public id:Guid,
        public businessName:string,
        public bookDate:Date,
    ) {}
}
