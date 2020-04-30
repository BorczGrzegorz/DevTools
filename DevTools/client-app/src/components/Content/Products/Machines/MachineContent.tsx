import * as React from 'react';
import AddIcon from '@material-ui/icons/Add';
import { MachineTable } from './MachineTable';
import { Machine } from '../../../../types/application/machine';
import { IconButton } from '@material-ui/core';
import { useState } from 'react';
import { Modal } from '../../../../controls/Modal';
import { AddMachineForm } from './AddMachineForm';
import { getMachines, NewMachine, addMachine, deleteMachine } from '../../../../services/machinesService';

export interface Props {
    productId: string
}

export const MachineContent = ({ productId }: Props) => {
    const [isOpen, setIsOpen] = useState(false);
    const [machines, setMachines] = useState<Machine[]>([]);

    const loadData = async () => {
        const machines: Machine[] = await getMachines(productId);
        setMachines(machines);
    }

    const onAddNewMachine = async (newMachine: NewMachine) => {
        const machine = await addMachine(productId, newMachine);
        setMachines([...machines, machine]);
        setIsOpen(false);
    }

    const onRemove = async (machine: Machine) => {
        await deleteMachine(productId, machine.id);
        setMachines(machines.filter(x => x.id !== machine.id));
    }

    React.useEffect(() => {
        loadData();
    }, [productId])

    return (
        <div>
            <h2>
                Machines
            </h2>
            <IconButton onClick={() => setIsOpen(true)}>
                <AddIcon />
            </IconButton>
            <Modal isOpen={isOpen} onClose={() => setIsOpen(false)}>
                <AddMachineForm productId={productId} 
                                onSubmit={onAddNewMachine}/>
            </Modal>
            <MachineTable machines={machines} onRemove={onRemove}/>
        </div>
    )
}