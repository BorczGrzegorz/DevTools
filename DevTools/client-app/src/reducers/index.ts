import { combineReducers } from "redux";
import { productReducer } from "./productReducers";
import { AppState } from "../types/AppState";

export const rootReducer = combineReducers<AppState>({
    products: productReducer
});