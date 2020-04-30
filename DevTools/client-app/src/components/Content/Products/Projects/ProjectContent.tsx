import * as React from 'react';
import { useParams } from 'react-router';
import { IconButton } from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import { AddressesTable } from './AddressesTable';
import { useState } from 'react';
import { Modal } from '../../../../controls/Modal';
import { Address } from '../../../../types/application/address';
import { NewAddress, addAddress, removeAddress } from '../../../../services/addressesService';
import { AddAddressForm } from './AddAddressFrom';
import _ from 'lodash';
import { getProject } from '../../../../services/projectsService';
import { Project } from '../../../../types/application/project';

export const ProjectContent = () => {
    const { projectId } = useParams();
    const [addresses, setAddresses] = useState<Address[]>([]);
    const [projectName, setProjectName] = useState<string>('');
    const [isOpen, setIsOpen] = useState(false);

    const loadData = async (projectId: string) => {
        const project: Project = await getProject(projectId);
        setProjectName(project.name);
        setAddresses(project.addresses);
    }

    const onAddNewAddress = async (newAddress: NewAddress) => {
        const address: Address = await addAddress(projectId as string, newAddress);
        setAddresses([...addresses, address]);
        setIsOpen(false);
    }

    const onRemoveAddress = async (address: Address) => {
        await removeAddress(projectId as string, address.id);
        setAddresses(_.filter(addresses, (x) => x.id !== address.id));
    }

    React.useEffect(() => {
        if (!projectId) {
            return;
        }

        loadData(projectId);
    }, [])

    if (!projectId) {
        return null;
    }

    return (
        <div>
            <h2>
                {projectName}
            </h2>
            <IconButton onClick={() => setIsOpen(true)}>
                <AddIcon />
            </IconButton>
            <Modal isOpen={isOpen} onClose={() => setIsOpen(false)}>
                <AddAddressForm projectId={projectId} onSubmit={onAddNewAddress} />
            </Modal>
            <AddressesTable addresses={addresses} onRemove={onRemoveAddress}/>
        </div>
    )
}