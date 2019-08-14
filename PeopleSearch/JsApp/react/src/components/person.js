/**
 * @module Person
 * @description Component to show person's profile as card
 */

/** External javascript  packages */
import React from "react";
import styled from "styled-components";
import PropTypes from "prop-types";
import { Tag, Text, Card, Elevation, Button } from "@blueprintjs/core";

/** Internal */
import { formatData } from "../common/utils";
import { deletePerson } from "../common/data-service";

const StyledCard = styled(Card)`
    display: grid;
    grid-template-columns: [image]auto [details]1fr [del]auto;
    grid-template-rows: 1fr auto;
    justify-content: left;
    align-content: center;
    grid-gap: 10px;
    min-width: 300px;
`;

const StyledTag = styled(Tag)`
    margin-right: 5px;
    margin-bottom: 5px;
    padding: 2px;
`;

const StyledText = styled(Text)`
    margin-bottom: 5px;
`;

const Image = styled.img`
    min-width: 100px;
`;

const Interests = styled.div`
    grid-column: 1/3;
`;

const DelButton = styled(Button)`
    justify-self: right;
    grid-column: 4/-1;
`;

Person.propTypes = {
    person: PropTypes.shape({
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
    }),
    isLoading: PropTypes.bool,
    onDelete: PropTypes.func
};
export function Person(props) {
    const formattedName = props.person.LastName + ", " + props.person.FirstName;
    const interests = props.person.Interests ? JSON.parse(props.person.Interests) : [];
    const imgSrc = "data:image/jpeg;base64," + props.person.Picture;
    const skeletonLoad = props.isLoading ? "bp3-skeleton" : "";
    const formattedDobAge =
        formatData(props.person.DateOfBirth, "date") + " (" + formatData(props.person.DateOfBirth, "age") + ")";

    return (
        <StyledCard elevation={Elevation.TWO}>
            {!props.isLoading && <Image src={imgSrc} width="150px" alt=""></Image>}
            {props.isLoading && <Image className={skeletonLoad} src="" width="150px" alt=""></Image>}
            <div>
                <StyledText className={skeletonLoad}>{formattedName}</StyledText>
                <StyledText className={skeletonLoad}>{formattedDobAge}</StyledText>
                <StyledText className={skeletonLoad}>{props.person.Address.AddressLine1}</StyledText>
                <StyledText className={skeletonLoad}>
                    {props.person.Address.City +
                        ", " +
                        props.person.Address.State +
                        ", " +
                        props.person.Address.ZipCode}
                </StyledText>
                <StyledText className={skeletonLoad}>{props.person.Address.Country}</StyledText>
            </div>

            <Interests>
                {interests.map((interest, index) => (
                    <StyledTag className={skeletonLoad} key={props.person.PersonId + index} round={true} large={false}>
                        {interest}
                    </StyledTag>
                ))}
            </Interests>
            <DelButton
                minimal={true}
                icon="delete"
                onClick={async () => {
                    await deletePerson(props.person.PersonId);
                    props.onDelete();
                }}
            ></DelButton>
        </StyledCard>
    );
}
