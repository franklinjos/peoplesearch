/**
 * @module App
 * @description App component
 */

/** External javascript  packages */
import React, { useState, useEffect } from "react";
import styled from "styled-components";

/** External CSS  packages */
import "@blueprintjs/core/lib/css/blueprint.css";
import "@blueprintjs/icons/lib/css/blueprint-icons.css";
import "@blueprintjs/datetime/lib/css/blueprint-datetime.css";

/** Internal */
import { GlobalStyle } from "./styles/global";
import { colorPalette } from "./themes/color-palette";
import { People, AddPerson, Search } from "./components";
import { search } from "./common/data-service";
// Data to show skeleton loading
import skeleton from "./common/skeleton-data";

const AppContainer = styled.div`
    display: grid;
    grid-template-columns: 1fr;
    margin-left: 20px;
    margin-right: 20px;
`;

const Header = styled.header`
    background-color: ${colorPalette.bluish};
    color: ${colorPalette.wildsand};
    margin-left: -20px;
    margin-right: -20px;
    height: 40px;
    
    padding: 5px;
    padding-left: 20px;
    font-size: 1.5rem;
`;

const SearchAddContainer = styled.div`
    display: grid;
    grid-template-columns: 1fr 1fr;
    grid-gap: 40px;
    margin-top: 20px;
    margin-bottom: 20px;
`;

function App() {
    /** React hooks for managing state of app */
    const [people, setPeople] = useState([...skeleton]);
    const [isLoading, setIsLoading] = useState(true);
    const [isError, setIsError] = useState(false);

    function getData(input) {
        search(input)
            .then(response => {
                setPeople([...response.data]);
                setIsLoading(false);
            })
            .catch(err => {
                //eslint-disable-next-line no-console
                console.error(err);
                setIsError(true);
            });
    }

    /** Adding one second delay to mock search being slow */
    function getDataWithDelay(searchInput) {
        setIsLoading(true);
        setPeople([...skeleton]);
        setTimeout(() => {
            getData(searchInput);
        }, 1000);
    }

    useEffect(() => {
        // Laod all people first time
        getData();
    }, []);

    return (
        <React.Fragment>
            <GlobalStyle />
            <AppContainer>
                <Header>Person search app</Header>
                <SearchAddContainer>
                    <Search onSearch={val => getDataWithDelay(val)} />
                    <AddPerson onAdd={() => getData()} />
                </SearchAddContainer>
                <People isError={isError} isLoading={isLoading} people={people}></People>
            </AppContainer>
        </React.Fragment>
    );
}

export default App;
