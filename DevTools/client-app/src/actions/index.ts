import { getProducts, addProduct } from "../api/devToolsApi";
import { ProductDto } from "../api/Dto/devToolsDtos";
import { LoadProductsAction } from './../types/actions';

export const loadProducts = () => async (dispatch: any) => {
    const productsDto: ProductDto[] = await getProducts();
    const action: LoadProductsAction = {
        type: "LOAD_PRODUCTS",
        payload: productsDto
    }
    dispatch(action);
}

export const createProduct = (productName: string) => async (dispatch: any) => {
    const productDto: ProductDto = await addProduct(productName);
    dispatch({
        type: "ADD_PRODUCT",
        payload: productDto
    })
}
