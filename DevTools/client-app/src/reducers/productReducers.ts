
import { Action } from 'redux';
import { LoadProductsAction, AddProdcutAction, AddMachineAction, AddProjectAction, AddAddressAction } from './../types/actions';
import { ProductDictionary } from '../types/AppState';

export const productReducer = (state: ProductDictionary = {}, action: Action): ProductDictionary => {
    if (action.type === 'LOAD_PRODUCTS') {
        const products = (action as LoadProductsAction).payload;
        const newProductCollection: ProductDictionary = {};
        products.map(x => newProductCollection[x.id] = x)
        return {
            ...state,
            ...newProductCollection
        }
    }

    if (action.type === 'ADD_PRODUCT') {
        const product = (action as AddProdcutAction).payload;
        return { ...state, [product.id]: product }
    }

    if (action.type === 'ADD_MACHINE') {
        const addMachineAction: AddMachineAction = action as AddMachineAction;
        const updatedProduct = state[addMachineAction.productId];
        updatedProduct.machines.push(addMachineAction.payload);
        return { ...state, [addMachineAction.productId]: updatedProduct }
    }

    if (action.type === 'ADD_PROJECT') {
        const addProjectAction: AddProjectAction = action as AddProjectAction;
        const updatedProduct = state[addProjectAction.productId];
        updatedProduct.projects.push(addProjectAction.payload);
        return { ...state, [addProjectAction.productId]: updatedProduct }
    }

    if(action.type === 'ADD_ADDRESS'){
        const addAddressAction: AddAddressAction = action as AddAddressAction;
        let updatedProduct = state[addAddressAction.productId];
        const products = updatedProduct.projects.map(x => {
            if(x.id === addAddressAction.projectId){
                x.addresses.push(addAddressAction.payload);
                return {...x}
            }

            return x;
        })
        updatedProduct.projects = products;
        updatedProduct = {...updatedProduct}
        return { ...state, [addAddressAction.productId]: updatedProduct }
    }

    return state;
}