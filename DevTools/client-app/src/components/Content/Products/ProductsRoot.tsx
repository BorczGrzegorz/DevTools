import * as React from 'react';
import styled from 'styled-components';
import { ProductList } from './ProductList';
import { ProductContent } from './ProductContent';
import { Route } from 'react-router';
import { ProjectContent } from './Projects/ProjectContent';

const RootContainer = styled.div`
    display: flex;
    flex-direction: row;
`;

const ContentWrapper = styled.div`
    padding: 20px;
`;

export const ProductsRoot = () => {
    return (
        <RootContainer>
            <ProductList />
            <ContentWrapper>
                <Route exact path="/products/:id/" component={ProductContent} />
                <Route path="/products/:productId/project/:projectId" component={ProjectContent}/>
            </ContentWrapper>
        </RootContainer>
    )
}