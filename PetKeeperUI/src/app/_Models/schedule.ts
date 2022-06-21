export class Schedule {
    constructor(
        public id:string,
        public created_Date:string,
        public dayOfWeek:string,
        public startTime:string,
        public endTime:string,
        public businessId:string,
    ){}
}
