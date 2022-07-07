import { ApplicationUserDto } from "./ApplicationUserDto";

export class TokenDto {
    constructor(
        public token :string,
        public expiration :Date,
        public currentUser : ApplicationUserDto,
        public role :string
    ) {}
}
