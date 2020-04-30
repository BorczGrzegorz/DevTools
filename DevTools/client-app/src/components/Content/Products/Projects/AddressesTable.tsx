import * as React from 'react';
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, IconButton } from '@material-ui/core';
import { Address } from '../../../../types/application/address';
import DeleteIcon from '@material-ui/icons/Delete';

export interface Props {
    addresses: Address[],
    onRemove: (address: Address) => void
}

export const AddressesTable = (props: Props) => {
    return (
        <>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>
                                Name
                            </TableCell>
                            <TableCell>
                                Address
                            </TableCell>
                            <TableCell>
                                Actions
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            props.addresses.map(address => (
                                <TableRow>
                                    <TableCell>{address.name}</TableCell>
                                    <TableCell>{address.path}</TableCell>
                                    <TableCell>
                                        <IconButton onClick={() => props.onRemove(address)}>
                                            <DeleteIcon/>
                                        </IconButton>
                                     </TableCell>
                                </TableRow>
                            ))
                        }
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    )
}