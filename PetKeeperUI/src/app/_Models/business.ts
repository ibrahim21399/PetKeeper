export class Business {
    constructor(
        public id:string,
        public businessName:string,
        public businessDesc:string,
        public businussPhone:string,
        public cityId:number,
        public areaId:number,
        public applicationUserId:string,
        public isActive:boolean,
        public serviceId:string[],
        public businessPic:string,
        public licencePic:string
    ){}
}
