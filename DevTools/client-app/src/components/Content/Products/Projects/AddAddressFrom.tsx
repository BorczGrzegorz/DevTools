import * as React from 'react';
import { useFormik } from 'formik';
import styled from 'styled-components';
import { Button, TextField, FormControlLabel, Checkbox } from '@material-ui/core';
import { NewAddress } from '../../../../services/addressesService';

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
    projectId: string,
    onSubmit: (newAddress: NewAddress) => void
}

export const AddAddressForm = (props: Props) => {
    const formik = useFormik({
        initialValues: {
            name: '',
            path: '',
            isSingleUrl: false
        },
        onSubmit: values => props.onSubmit(values)
    })

    return (
        <form onSubmit={formik.handleSubmit}>
            <Container>
                <Input id='name' onChange={formik.handleChange} value={formik.values.name} label='Name' />
                <Input id='path' onChange={formik.handleChange} value={formik.values.path} label='Path' />
                <FormControlLabel
                    control={
                        <Checkbox id='isSingleUrl' 
                          name='isSingleUrl'
                          onChange={formik.handleChange} 
                          checked={formik.values.isSingleUrl}
                          onBlur={formik.handleBlur}
                          value={formik.values.isSingleUrl} />
                    }
                    label="Is single path?"
                />
                <SubmitButton type='submit' variant="outlined">Submit</SubmitButton>
            </Container>
        </form>
    )
}