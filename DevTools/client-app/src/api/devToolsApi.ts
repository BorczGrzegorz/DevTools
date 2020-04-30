import axios from 'axios';
import {
    MachineDto,
    ProductDto,
    NewMachineDto,
    ProjectDto,
    AddressDto,
    NewAddressDto,
    UserDto,
    UsersDaysSummaryDto,
    DaySummaryDto
} from './Dto/devToolsDtos';

export const getProducts = (): Promise<ProductDto[]> => axios.get("/product").then(x => x.data);

export const addProduct = async (productName: string): Promise<ProductDto> => {
    const response = await axios.post(`/product/name/${productName}`);
    return response.data;
}

export const addMachine = async (productId: string, newMachineDto: NewMachineDto): Promise<MachineDto[]> => {
    const models: NewMachineDto[] = [newMachineDto];
    const response = await axios.put(`/machines/product/${productId}`, models);
    return response.data;
}

export const deleteMachine = async (productId: string, machineId: string): Promise<MachineDto> => {
    const response = await axios.delete(`/machines/product/${productId}/machine/${machineId}`);
    return response.data;
}

export const getMachines = async (productId: string): Promise<MachineDto[]> => {
    const response = await axios.get(`/machines/product/${productId}`);
    return response.data;
}

export const addProject = async (productId: string, name: string): Promise<ProjectDto[]> => {
    const model: string[] = [name];
    const response = await axios.put(`/product/${productId}/projects/`, model);
    return response.data;
}

export const getProject = async (projectId: string): Promise<ProjectDto> => {
    const response = await axios.get<ProjectDto>(`/projects/${projectId}`);
    return response.data;
}

export const getProjects = async (productdId: string): Promise<ProjectDto[]> => {
    const response = await axios.get<ProjectDto[]>(`/product/${productdId}/projects`);
    return response.data
}

export const deleteProject = async (projectId: string): Promise<ProjectDto> => {
    const response = await axios.delete<ProjectDto>(`/projects/${projectId}`);
    return response.data;
}

export const addAddress = async (projectId: string, model: NewAddressDto): Promise<AddressDto> => {
    const models: NewAddressDto[] = [model];
    const response = await axios.put(`/addresses/project/${projectId}`, models);
    return response.data[0];
}

export const deleteAddress = async (projectId: string, addressId: string): Promise<AddressDto> => {
    const response = await axios.delete<AddressDto>(`/addresses/project/${projectId}/address/${addressId}`);
    return response.data;
}

export const getAddresses = async(projectId: string): Promise<AddressDto[]> => {
    const response = await axios.get<AddressDto[]>(`/addresses/project/${projectId}`);
    return response.data;
}

export const getUser = async () : Promise<UserDto> => {
    const response = await axios.get(`http://localhost:5000/user`);
    return response.data;
}

export const getDatesSummary = async (userName: string) : Promise<DaySummaryDto> => {
    const response = await axios.get(`http://localhost:5000/issues/dates?userName=${userName}`);
    const usersDaysSummary = response.data as UsersDaysSummaryDto;
    return usersDaysSummary[Object.keys(usersDaysSummary)[0]];
}