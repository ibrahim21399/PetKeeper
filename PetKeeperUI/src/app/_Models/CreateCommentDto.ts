import { Guid } from "guid-typescript";

export class CreateCommentDto {
    constructor(
        public comment:string ,
        public rate:number ,
        public applicationUserId:Guid ,
    ) {}
}
