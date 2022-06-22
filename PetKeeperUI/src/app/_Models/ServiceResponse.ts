export class ServiceResponse<T> {
    constructor(
        public success : Boolean,
        public message : string,
        public data : T,
    ) {}
}
