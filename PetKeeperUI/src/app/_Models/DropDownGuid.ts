import { Guid } from "guid-typescript";

export class DropDownGuid {
    constructor(
        public id :Guid,
        public name :string,
    ) {
        this.id = Guid.create();
    }
}
