import { Product } from "./application/product";
import { Machine } from "./application/machine";
import { Project } from "./application/project";

export interface ProductDictionary {
    [productId: string]: Product;
}

export interface AppState {
    products: ProductDictionary;
}