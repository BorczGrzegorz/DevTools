import { Machine } from "../types/application/machine";
import { getMachines as getMachinesDto,
         addMachine as addMachineDto,
         deleteMachine as deleteMachineDto} from "../api/devToolsApi";

export interface NewMachine{
    name: string,
    address: string,
    description: string
}

export const getMachines = async (productId: string): Promise<Machine[]> => {
    const machines = await getMachinesDto(productId);
    return machines;
}

export const addMachine = async (productId: string, newMachine: NewMachine): Promise<Machine> => {
    const machine = await addMachineDto(productId, newMachine);
    return machine[0];
}

export const deleteMachine = async (productId: string, machineId: string): Promise<Machine> => {
    const machine: Machine = await deleteMachineDto(productId, machineId);
    return machine;
}