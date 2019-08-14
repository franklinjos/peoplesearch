import { css } from "styled-components";
const sizes = {
    xsmall: 360,
    small: 768,
    medium: 992,
    large: 1440
};

// Iterate through the sizes and create a media template
const media = Object.keys(sizes).reduce((acc, label) => {
    acc[label] = (...args) => css`
        @media (min-width: ${sizes[label] / 16}em) {
            ${css(...args)}
        }
    `;

    return acc;
}, {});

export { media };
