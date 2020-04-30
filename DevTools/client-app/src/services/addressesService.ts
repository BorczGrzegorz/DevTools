import { Address } from "../types/application/address";
import { getAddresses as getAddressesDtos,
         addAddress as addAddressDto, 
         deleteAddress} from "../api/devToolsApi";
import { AddressDto } from "../api/Dto/devToolsDtos";

export interface NewAddress{
    name: string,
    path: string,
    isSingleUrl: boolean
}

export const getAddresses = async (projectId: string): Promise<Address[]> => {
    const addresses: AddressDto[] = await getAddressesDtos(projectId);
    return addresses;
}

export const addAddress = async (projectId: string, newAddress: NewAddress): Promise<Address> => {
    const newAddressesDto = await addAddressDto(projectId, newAddress);
    return newAddressesDto;
}

export const removeAddress = async (projectId: string, addressId: string): Promise<Address> => {
    const deletedAddress = await deleteAddress(projectId, addressId);
    return deletedAddress;
}