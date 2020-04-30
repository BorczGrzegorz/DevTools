import { Address } from "./address";

export interface Project {
    id: string,
    name: string,
    addresses: Address[]
}