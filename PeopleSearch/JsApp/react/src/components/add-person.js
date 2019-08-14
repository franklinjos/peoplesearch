/* eslint-disable no-console */
/**
 * @module AddPerson
 * @description Component to Add person to db
 */

/** External javascript  packages */
import React, { useState } from "react";
import styled from "styled-components";
import PropTypes from "prop-types";
import { FileInput, FormGroup, Button, Classes, Dialog, Intent } from "@blueprintjs/core";
import { DateInput } from "@blueprintjs/datetime";

/** Internal */
import { addPerson } from "../common/data-service";
import { toBase64, personModel, requiredAddressKeys, requiredPersonKeys } from "../common/utils";
import { FormText } from "./";

const Container = styled.div`
    display: grid;
    grid-template-columns: 200px 240px;
    grid-gap: 15px;
`;

const jsDateFormatter = {
    formatDate: date => date.toLocaleDateString(),
    parseDate: str => new Date(str),
    placeholder: "M/D/YYYY"
};

AddPerson.propTypes = {
    onAdd: PropTypes.func
};

export function AddPerson(props) {
    const [isOpen, setIsOpen] = useState(false);
    const [isValidInput, setIsValidInput] = useState(false);
    const [fileInput, setFileInput] = useState("Choose file...");
    const [isSaving, setIsSaving] = useState(false);
    const [person, setPerson] = useState(JSON.parse(JSON.stringify(personModel)));

    function updatePersonValue(key, val) {
        let newPerson = JSON.parse(JSON.stringify(person));
        newPerson[key] = val;
        setPerson(newPerson);
        validate(newPerson);
    }

    function updateAddressValue(key, val) {
        let newPerson = JSON.parse(JSON.stringify(person));
        newPerson.Address[key] = val;
        setPerson(newPerson);
        validate(newPerson);
    }

    function validate(newPerson) {
        let count = 0;
        requiredPersonKeys.forEach(key => {
            if (!newPerson[key].trim()) {
                count++;
            }
        });
        requiredAddressKeys.forEach(key => {
            if (!newPerson.Address[key].trim()) {
                count++;
            }
        });
        if (count == 0) setIsValidInput(true);
        else setIsValidInput(false);
    }
    return (
        <div>
            <Button
                onClick={() => {
                    setIsOpen(true);
                    setFileInput("");
                    setPerson(JSON.parse(JSON.stringify(personModel)));
                    setIsValidInput(false);
                }}
            >
                Add Person
            </Button>
            <Dialog isOpen={isOpen} icon="add" onClose={() => setIsOpen(false)} title="Add Person">
                <Container className={Classes.DIALOG_BODY}>
                    <FormText
                        placeHolder="First Name"
                        isRequired={true}
                        maxLength={50}
                        onVlaueChanged={val => updatePersonValue("FirstName", val)}
                    />
                    <FormText
                        placeHolder="Last Name"
                        isRequired={true}
                        maxLength={50}
                        onVlaueChanged={val => updatePersonValue("LastName", val)}
                    />
                    <FormGroup helperText="" label="Date of birth" labelFor="text-input" labelInfo="(required)">
                        <DateInput
                            maxDate={new Date()}
                            required
                            {...jsDateFormatter}
                            onChange={newDate => updatePersonValue("DateOfBirth", newDate.toISOString().split("T")[0])}
                        />
                    </FormGroup>
                    <FormText
                        placeHolder="Address"
                        isRequired={true}
                        maxLength={100}
                        onVlaueChanged={val => updateAddressValue("AddressLine1", val)}
                    />
                    <FormText
                        placeHolder="City"
                        isRequired={true}
                        maxLength={50}
                        onVlaueChanged={val => updateAddressValue("City", val)}
                    />
                    <FormText
                        placeHolder="State"
                        isRequired={true}
                        maxLength={50}
                        onVlaueChanged={val => updateAddressValue("State", val)}
                    />
                    <FormText
                        placeHolder="Zip Code"
                        isRequired={true}
                        helperText="Should be 5 digit number"
                        maxLength={5}
                        onVlaueChanged={val => {
                            let parsed = parseInt(val, 10);
                            if (!isNaN(parsed) && parsed > 10000) {
                                updateAddressValue("ZipCode", val);
                            } else {
                                updateAddressValue("ZipCode", "");
                            }
                        }}
                    />
                    <FormText
                        placeHolder="Country"
                        isRequired={true}
                        maxLength={50}
                        onVlaueChanged={val => updateAddressValue("Country", val)}
                    />
                    <FormText
                        helperText="Should be comma delimited list"
                        placeHolder="Interests"
                        isRequired={true}
                        maxLength={100}
                        onVlaueChanged={val => {
                            if (val.trim()) {
                                updatePersonValue("Interests", JSON.stringify(val.split(",")));
                            } else {
                                updatePersonValue("Interests", val);
                            }
                        }}
                    />

                    <FormGroup
                        helperText="Shoud be jpeg and not exceed 1 mb"
                        label="Profile picture"
                        labelFor="text-input"
                        labelInfo="(required)"
                    >
                        <FileInput
                            disabled={false}
                            required
                            text={fileInput}
                            onInputChange={async ({ target: { files } }) => {
                                const file = files[0];
                                if (file.size < 1024000 && file.type === "image/jpeg") {
                                    setFileInput(file.name);
                                    try {
                                        let base64 = await toBase64(file);
                                        base64 = base64.substring(base64.indexOf(",") + 1);
                                        updatePersonValue("Picture", base64);
                                    } catch (err) {
                                        console.error(err);
                                    }
                                }
                            }}
                        />
                    </FormGroup>
                </Container>
                <div className={Classes.DIALOG_FOOTER}>
                    <div className={Classes.DIALOG_FOOTER_ACTIONS}>
                        <Button onClick={() => setIsOpen(false)}>Close</Button>
                        <Button
                            intent={Intent.PRIMARY}
                            onClick={async () => {
                                if (isValidInput) {
                                    setIsSaving(true);
                                    try {
                                        await addPerson(person);
                                    } catch (ex) {
                                        // eslint-disable-next-line no-console
                                        console.error(ex);
                                        setIsSaving(false);
                                    }

                                    props.onAdd();
                                    setIsSaving(false);
                                    setIsOpen(false);
                                }
                            }}
                            disabled={!isValidInput}
                            loading={isSaving}
                        >
                            Save
                        </Button>
                    </div>
                </div>
            </Dialog>
        </div>
    );
}
