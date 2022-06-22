// type GUID = string & { isGuid: true};
// function guid(guid: string) : GUID {
//     return  guid as GUID; // maybe add validation that the parameter is an actual guid ?
// }

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
