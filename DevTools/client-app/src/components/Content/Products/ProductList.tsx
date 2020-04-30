import React, { useEffect } from 'react';
import { AppState } from '../../../types/AppState';
import { useSelector, useDispatch } from 'react-redux';
import { loadProducts, createProduct } from '../../../actions';
import styled from 'styled-components';
import ListItemText from '@material-ui/core/ListItemText';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import { TextField, Divider } from '@material-ui/core';
import { ProductDictionary } from './../../../types/AppState';
import { Product } from './../../../types/application/product';
import { useHistory } from 'react-router';

const ListContainer = styled(List)`
    max-width: 200px;
    border-right: 0.1em solid lightgray;
    min-height: 100vh;
`;

export const ProductList = () => {
    const products: ProductDictionary = useSelector((state: AppState) => state.products);
    const dispatch = useDispatch(); 
    const history = useHistory();

    useEffect(() => { dispatch(loadProducts()) }, []);

    const onEnter = async (e: any) => {
        if (e.keyCode === 13) {
            dispatch(createProduct(e.target.value));
        }
    }

    const onClickItem = (product: Product) => {
        history.push(`/products/${product.id}`);
    }

    return (
        <ListContainer>
            <TextField label='New product' onKeyDown={onEnter} />
            {
                Object.keys(products).map(key => {
                    return (
                        <>
                            <ListItem button onClick={() => onClickItem(products[key])}>
                                <ListItemText primary={products[key].name} />
                            </ListItem>
                            <Divider />
                        </>
                    )
                })
            }
        </ListContainer>
    )
};