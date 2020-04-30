import React from 'react';
import { Route } from 'react-router';
import { ProductsRoot } from './Products/ProductsRoot';

export const Content = () => {
    return (
        <>
            <Route path='/products' component={ProductsRoot} />
        </>
    )
}