/**
 * @module Person
 * @description Component to show person's profile as card
 */

/** External javascript  packages */
import React from "react";
import PropTypes from "prop-types";
import { debounce } from "throttle-debounce";

Search.propTypes = {
    onSearch: PropTypes.func.isRequired
};

export function Search(props) {
    /** function to limit calls to server*/
    const debouncedSearch = debounce(300, (val) => {
        props.onSearch(val);
    });

    return (
        <div className="bp3-input-group">
            <span className="bp3-icon bp3-icon-search"></span>
            <input
                type="text"
                onChange={event => {
                    if (props.onSearch && typeof props.onSearch === "function") {
                        debouncedSearch(event.target.value);
                    }
                }}
                className="bp3-input"
                placeholder="Search for people"
            />
        </div>
    );
}
