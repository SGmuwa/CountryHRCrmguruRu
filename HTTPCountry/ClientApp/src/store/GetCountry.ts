import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface GetCountryState {
    isLoading: boolean;
    inputName: string;
    forecasts: CountryInfo[];
}

export interface CountryInfo {
    name: string;
    code: string;
    capital: string;
    area: number;
    population: number;
    region: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestCountryInfo {
    type: 'REQUEST_COUNTRY_INFO';
    inputName: string;
}

interface ReceiveCountryInfo {
    type: 'RECEIVE_COUNTRY_INFO';
    forecasts: CountryInfo[];
}

interface SetInputName {
    type: 'SET_INPUT_NAME';
    inputName: string;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCountryInfo | ReceiveCountryInfo | SetInputName;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestGetCountry: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.getCountry) {
            fetch(`getcountry`)
                .then(response => response.json() as Promise<CountryInfo[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COUNTRY_INFO', forecasts: data });
                });

            dispatch({ type: 'REQUEST_COUNTRY_INFO', inputName: appState.getCountry.inputName });
        }
    },
    setInput: (v: string) => ({ type: 'SET_INPUT_NAME', inputName: v } as SetInputName),
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: GetCountryState = { forecasts: [], isLoading: false, inputName: 'Ukraine' };

export const reducer: Reducer<GetCountryState> = (state: GetCountryState | undefined, incomingAction: Action): GetCountryState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_COUNTRY_INFO':
            return {
                forecasts: state.forecasts,
                isLoading: true,
                inputName: state.inputName
            };
        case 'RECEIVE_COUNTRY_INFO':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            return {
                forecasts: action.forecasts,
                isLoading: false,
                inputName: state.inputName
            };
        case 'SET_INPUT_NAME':
            return {
                forecasts: state.forecasts,
                isLoading: state.isLoading,
                inputName: action.inputName
            }
        default:
            return state;
    }
};
