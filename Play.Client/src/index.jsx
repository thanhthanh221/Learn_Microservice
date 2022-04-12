import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

import {useState} from "react";
function component()  {
  const [state, setState] = useState(initState);
}
ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

//Hook => Dùng cho function component

/*
useCallback: ƒ useCallback(callback, deps)
useContext: ƒ useContext(Context)
useDebugValue: ƒ useDebugValue(value, formatterFn)
useDeferredValue: ƒ useDeferredValue(value)
useEffect: ƒ useEffect(create, deps)
useId: ƒ useId()
useImperativeHandle: ƒ useImperativeHandle(ref, create, deps)
useInsertionEffect: ƒ useInsertionEffect(create, deps)
useLayoutEffect: ƒ useLayoutEffect(create, deps)
useMemo: ƒ useMemo(create, deps)
useReducer: ƒ useReducer(reducer, initialArg, init)
useRef: ƒ useRef(initialValue)
useState: ƒ useState(initialState)
useSyncExternalStore: ƒ useSyncExternalStore(subscribe, getSnapshot, getServerSnapshot)
useTransition: ƒ useTransition()
 */
