import * as React from 'react';
import { List, ListItem, ListItemText, ListItemIcon} from '@material-ui/core';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import { useHistory } from 'react-router';
import styled from 'styled-components';

const ListContainer = styled(List)`
    width: 200px;
    border-right: 0.1em solid lightgray;
    height: 100%;
`;

export const Menu = () => {
    let history = useHistory();
    return (
        <ListContainer>
            <ListItem button onClick={() => history.push('/products')}>
                <ListItemIcon>
                    <InboxIcon />
                </ListItemIcon>
                <ListItemText>Products</ListItemText>
            </ListItem>
        </ListContainer>
    )
}