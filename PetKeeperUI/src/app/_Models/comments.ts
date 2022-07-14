import { Guid } from "guid-typescript";

export class Comments {
    constructor(
        public commentId:Guid,
        public comment:string ,
        public rate:number ,
        public businessId:Guid ,
        public applicationUserId:Guid ,
    ) {}
}
