import { Machine } from "./machine";
import { Project } from "./project";

export interface Product {
    id: string;
    name: string;
    description: string;
    machines: Machine[];
    projects: Project[]
}