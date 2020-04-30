import { Project } from "../types/application/project";
import { getProject as getProjectDto,
         getProjects as getProjectsDto,
         deleteProject as deleteProjectDto,
         addProject as addProjectDto} from "../api/devToolsApi";

export const getProject = async (projectId: string): Promise<Project> => {
    const project: Project = await getProjectDto(projectId);
    return project;
}

export const getProjects = async (productId: string) : Promise<Project[]> => {
    const projects: Project[] = await getProjectsDto(productId);
    return projects;
}

export const deleteProject = async (projectId: string) => {
    const project: Project = await deleteProjectDto(projectId);
    return project;
}

export const addProject = async (productId: string, name: string): Promise<Project> => {
    const newProject: Project[] = await addProjectDto(productId, name);
    return newProject[0];
}