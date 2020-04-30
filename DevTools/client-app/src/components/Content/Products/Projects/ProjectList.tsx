import * as React from 'react';
import { Project } from '../../../../types/application/project';
import { Paper, TextField, List, ListItem, ListItemText, Divider, ListItemSecondaryAction, IconButton } from '@material-ui/core';
import styled from 'styled-components';
import { useHistory, useRouteMatch } from 'react-router';
import DeleteIcon from '@material-ui/icons/Delete';
import { getProjects, deleteProject, addProject } from '../../../../services/projectsService';

export interface Props {
    productId: string
}

const Container = styled((props: any) => <Paper {...props} />)`
    && {
        padding: 20px;
    }
`;

export const ProjectList = ({productId}: Props) => {

    const [projects, setProjects] = React.useState<Project[]>([]);
    const history = useHistory();
    const match = useRouteMatch();

    const onKeyDown = async (e: any) => {
        if (e.keyCode === 13) {
            const newProject = await addProject(productId, e.target.value);
            setProjects([...projects, newProject]);
        }
    }

    const loadData = async () => {
        const projects = await getProjects(productId);
        setProjects(projects);
    }

    React.useEffect(() => {
        loadData();
    },[productId])

    const onDelete = async (project: Project) => {
        await deleteProject(project.id);
        setProjects(projects.filter(x => x.id !== project.id));
    }

    return (
        <>
            <h2>
                Projects
            </h2>
            <Container>
                <TextField label='New project' onKeyDown={onKeyDown} />
                <List>
                    {projects.map((project: Project) => {
                        return (
                            <>
                                <ListItem button onClick={() => history.push(`${match.url}/project/${project.id}`)}>
                                    <ListItemText primary={project.name} />
                                    <ListItemSecondaryAction>
                                        <IconButton edge="end" aria-label="delete" onClick={() => onDelete(project)}>
                                            <DeleteIcon />
                                        </IconButton>
                                    </ListItemSecondaryAction>
                                </ListItem>
                                <Divider />
                            </>
                        )
                    })}
                </List>
            </Container>
        </>
    )
} 