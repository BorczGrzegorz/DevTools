import * as React from 'react';
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, IconButton } from '@material-ui/core';
import { Machine } from '../../../../types/application/machine';
import DeleteIcon from '@material-ui/icons/Delete';
export interface Props {
    machines: Machine[],
    onRemove: (machine: Machine) => void
}

export const MachineTable = (props: Props) => {
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
                                Description
                            </TableCell>
                            <TableCell>
                                Actions
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            props.machines.map((singleMachine: Machine) => {
                                return (
                                    <TableRow>
                                        <TableCell>{singleMachine.name}</TableCell>
                                        <TableCell>{singleMachine.address}</TableCell>
                                        <TableCell>{singleMachine.description}</TableCell>
                                        <TableCell>
                                            <IconButton onClick={() => props.onRemove(singleMachine)}>
                                                <DeleteIcon/>
                                            </IconButton>
                                         </TableCell>
                                    </TableRow>
                                )
                            })
                        }
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    )
}