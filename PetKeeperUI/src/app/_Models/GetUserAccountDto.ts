import { Guid } from "guid-typescript";

export class GetUserAccountDto {
    constructor(
        public id:Guid,
        public email:string,
        public phoneNumber :string,
        public userName:string ,
        public fullName:string,
        public userPic:File,
    ) {
        this.id = Guid.create();
    }
}
