import { Guid } from "guid-typescript";

export class GetUserDto {
    constructor(
        public id : Guid,
        public fullName : String,
        public email : String,
        public phoneNumber : String,
    ) {
        this.id = Guid.create();
    }
}
