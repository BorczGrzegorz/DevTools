export interface NewAddressDto {
    name: string,
    path: string,
    isSingleUrl: boolean
}

export interface AddressDto extends NewAddressDto {
    id: string
}

export interface ProjectDto {
    id: string,
    name: string,
    addresses: AddressDto[]
}

export interface ProductDto {
    id: string,
    name: string,
    description: string;
    machines: MachineDto[],
    projects: ProjectDto[]
}

export interface NewMachineDto {
    address: string;
    description: string | null;
    name: string;
}

export interface MachineDto extends NewMachineDto {
    id: string
}

export interface AvatarsUrls {
    url48x48: string,
    url32x32: string,
    url24x24: string,
    url16x16: string
}

export interface UserDto {
    key: string,
    name: string,
    emailAddress: string,
    displayName: string,
    avatarsUrls: AvatarsUrls
}

export interface ShortIssueDto {
    id: string,
    key: string,
    summary: string,
    timeSpentSeconds: number
}

export interface DaySummaryDto{
    [date: string] : ShortIssueDto[]
}

export interface UsersDaysSummaryDto{
    [userName: string] : DaySummaryDto
}
