import { Action } from "redux";
import { Product } from "./application/product";
import { Machine } from "./application/machine";
import { Project } from "./application/project";
import { Address } from './application/address';

export interface LoadProductsAction extends Action {
    payload: Product[];
}

export interface AddProdcutAction extends Action {
    payload : Product;
}

export interface AddMachineAction extends Action {
    payload: Machine,
    productId: string
}

export interface AddProjectAction extends Action {
    payload: Project,
    productId: string
}

export interface AddAddressAction extends Action {
    payload: Address,
    projectId: string,
    productId: string
}