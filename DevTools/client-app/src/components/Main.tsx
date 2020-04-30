import React from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import styled from 'styled-components';
import { Menu } from './Menu/Menu';
import { Content } from './Content/Content';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            display: 'flex'
        },
        appBar: {
            zIndex: theme.zIndex.drawer + 1,
        }
    }),
);

const MainContainer = styled.div`
    display: flex;
    flex-direction: row;
    height: 100vh;
    align-items: stretch;
    justify-items: stretch;
    width: 100%;
    padding-top:66px;
`;

const ContentContainter = styled.div`
    flex: 1 1 auto;
    overflow: auto;
    &::-webkit-scrollbar{
        display: none;
    }
`;

export const Main = () => {
    const classes = useStyles();

    return (
        <>
            <div className={classes.root}>
                <CssBaseline />
                <AppBar
                    position="fixed"
                    className={classes.appBar}>
                    <Toolbar>
                        <Typography variant="h6" noWrap>
                            DevTools Administration Panel
                    </Typography>
                    </Toolbar>
                </AppBar>
                <MainContainer>
                    <Menu />
                    <ContentContainter>
                        <Content />
                    </ContentContainter>
                </MainContainer>
            </div>
        </>
    );
}