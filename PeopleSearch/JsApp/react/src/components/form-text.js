/**
 * @module FormText
 * @description Component to capture text input
 */

/** External javascript  packages */
import React from "react";
import PropTypes from "prop-types";
import { FormGroup, InputGroup } from "@blueprintjs/core";

FormText.propTypes = {
    maxLength: PropTypes.number.isRequired,
    placeHolder: PropTypes.string,
    isRequired: PropTypes.bool,
    helperText: PropTypes.string,
    onVlaueChanged: PropTypes.func
};
export function FormText(props) {
    const requiredLabel = props.isRequired ? "(required)" : "";
    return (
        <FormGroup
            helperText={props.helperText}
            label={props.placeHolder}
            labelFor="text-input"
            labelInfo={requiredLabel}
        >
            <InputGroup
                required
                maxLength={props.maxLength}
                id="text-input"
                placeholder={props.placeHolder}
                onChange={({ target: { value } }) => {
                    props.onVlaueChanged(value);
                }}
            />
        </FormGroup>
    );
}
