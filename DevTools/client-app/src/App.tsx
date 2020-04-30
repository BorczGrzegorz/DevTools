import React from 'react';
import { createStore, applyMiddleware, compose } from 'redux';
import reduxThunk from 'redux-thunk';
import { Provider } from 'react-redux';
import { Main } from './components/Main';
import { rootReducer } from './reducers';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import styled from 'styled-components';

declare global {
  interface Window {
    __REDUX_DEVTOOLS_EXTENSION_COMPOSE__?: typeof compose;
  }
}

const createAppStore = () => {
  const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
  const store = createStore(rootReducer, composeEnhancers(applyMiddleware(reduxThunk)));
  return store;
}

const OptionsContainer = styled.div`
    display: flex;
    align-items: center;
    justify-content: center;
    background: lightgray;
    margin-top: -8px;
    margin-left: -8px;
`;

const OptionsContent = styled.div`
  display: flex;
  width: 220px;
  max-width: 220px;
  height: 100vh;
  background-color: white;
  box-shadow: 10px 10px 5px grey;
  padding: 10px;
`;

const App: React.FC = () => {
  return (
    <Provider store={createAppStore()}>
      <BrowserRouter>
        <Switch>
          <Route path='/' component={Main} />
        </Switch>
      </BrowserRouter>
    </Provider>
  );
}

export default App;
