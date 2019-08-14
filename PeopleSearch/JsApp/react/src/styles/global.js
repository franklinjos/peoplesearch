import { createGlobalStyle } from "styled-components";
import styledNormalize from "styled-normalize";

export const GlobalStyle = createGlobalStyle`
    @import url('https://fonts.googleapis.com/css?family=Lato');
    ${styledNormalize}
    :root{
        font-size: 16px;
        font-family: Lato;
    }

`;
