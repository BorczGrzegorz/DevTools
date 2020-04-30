import * as React from 'react'; 
import { Modal as MaterialUiModal, makeStyles, Theme, createStyles} from '@material-ui/core';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        modal: {
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
        },
        paper: {
            backgroundColor: theme.palette.background.paper,
            boxShadow: theme.shadows[5],
            padding: theme.spacing(2, 4, 3),
        },
    }),
);

export interface Props {
    isOpen: boolean,
    onClose: () => void,
    children: React.ReactElement;
}

export const Modal = (props : Props) => {
    const classes = useStyles();
    return (
        <MaterialUiModal className={classes.modal} open={props.isOpen} onClose={props.onClose}>
            <div className={classes.paper}>
                {props.children}
            </div>
        </MaterialUiModal>
    );
}