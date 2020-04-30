import * as React from 'react';
import { useParams } from 'react-router';
import { AppState } from '../../../types/AppState';
import { useSelector } from 'react-redux';
import { ProductDictionary } from './../../../types/AppState';
import { MachineContent } from './Machines/MachineContent';
import { ProjectList } from './Projects/ProjectList';

export const ProductContent = () => {
    let { id }: { id?: string } = useParams();
    let products: ProductDictionary = useSelector((state: AppState) => state.products);

    if (!id || !products[id]) {
        return null;
    }

    return (
        <div>
            <MachineContent productId={id} />
            <ProjectList productId={id} />
        </div>
    )
}