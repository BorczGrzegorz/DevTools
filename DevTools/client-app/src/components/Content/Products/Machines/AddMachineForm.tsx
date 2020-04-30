import * as React from 'react';
import { useFormik } from 'formik';
import { TextField, Button } from '@material-ui/core';
import styled from 'styled-components';
import { NewMachine } from '../../../../services/machinesService';

const Container = styled.div`
    display: flex;
    flex-direction: column;
`;

const SubmitButton = styled(Button)`
    && {
        margin-top: 20px
        };
`;

const Input = styled((props: any) => <TextField {...props} />)`
    && {
        margin-top: 30px;
        min-width: 300px;
    }
`
export interface Props {
    productId: string,
    onSubmit: (newMachine: NewMachine) => void
}

export const AddMachineForm = (props: Props) => {
    const formik = useFormik({
        initialValues: {
            name: '',
            address: '',
            description: ''
        },
        onSubmit: values => props.onSubmit(values)
    });

    return (
        <form onSubmit={formik.handleSubmit}>
            <Container>
                <Input id='name' onChange={formik.handleChange} value={formik.values.name} label='Name' />
                <Input id='address' onChange={formik.handleChange} value={formik.values.address} label='Address' />
                <Input id='description' onChange={formik.handleChange} value={formik.values.description} label='Description' />
                <SubmitButton type='submit' variant="outlined">Submit</SubmitButton>
            </Container>
        </form>
    )
}