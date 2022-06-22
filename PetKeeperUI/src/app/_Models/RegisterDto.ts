export class RegisterDto {
    constructor(
        public fullName:string,
        public email:string,
        public password:string,
        public confirmPassword:string,
        public phoneNumber:string,
        public userPic:File
    ) {}
}
