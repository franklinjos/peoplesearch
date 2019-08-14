/**
 * @module People
 * @description Component to show collection of Person profile cards
 */

/** External javascript  packages */
import React from "react";
import styled from "styled-components";
import PropTypes from "prop-types";
import { Card, Elevation } from "@blueprintjs/core";

/** Internal */
import { Person } from "./";
import { media } from "../styles/media";

const CardsContainer = styled.div`
    display: grid;
    grid-template-columns: repeat(1, 1fr);
    grid-gap: 20px;
    /* Adjust cards in one row based on screen break point. Defaulted to xsmal */
    ${media.small`grid-template-columns: repeat(2, 1fr);`}
    ${media.medium`grid-template-columns: repeat(3, 1fr);`}
    ${media.large`grid-template-columns: repeat(4, 1fr);`}
`;

const StyledEmptyOrErrorCard = styled(Card)`
    grid-column: 1/-1;
`;

People.propTypes = {
    people: PropTypes.arrayOf(
        PropTypes.shape({
            PersonId: PropTypes.number.isRequired,
            FirstName: PropTypes.string.isRequired,
            LastName: PropTypes.string.isRequired,
            MiddleName: PropTypes.string,
            DateOfBirth: PropTypes.string,
            Picture: PropTypes.any,
            Interests: PropTypes.string,
            Address: PropTypes.shape({
                PersonAddressId: PropTypes.number.isRequired,
                AddressLine1: PropTypes.string.isRequired,
                AddressLine2: PropTypes.string,
                City: PropTypes.string.isRequired,
                State: PropTypes.string.isRequired,
                ZipCode: PropTypes.string.isRequired,
                Country: PropTypes.string.isRequired
            })
        })
    ),
    isLoading: PropTypes.bool,
    isError: PropTypes.bool,
    onDelete: PropTypes.func
};

export function People(props) {
    return (
        <CardsContainer>
            {!props.isError &&
                props.people.map(person => (
                    <Person
                        isLoading={props.isLoading}
                        key={person.PersonId}
                        person={person}
                        onDelete={() => {
                            props.onDelete();
                        }}
                    />
                ))}
            {props.isError && (
                <StyledEmptyOrErrorCard elevation={Elevation.TWO}>Sorry failed to laod data.</StyledEmptyOrErrorCard>
            )}
            {!props.isError && !props.people.length && (
                <StyledEmptyOrErrorCard elevation={Elevation.TWO}>No matching person found.</StyledEmptyOrErrorCard>
            )}
        </CardsContainer>
    );
}
