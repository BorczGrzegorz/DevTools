import * as React from 'react';
import styled from 'styled-components';
import { CircularProgress } from '@material-ui/core';

const Container = styled.div`
    display: flex;
    align-items: center;
    justify-content: center;
    flex-grow: 1;
`;

export const LoadingPage = () => {
    return(
        <Container>
            <CircularProgress/>
        </Container>
    )
}