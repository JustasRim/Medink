import { createTheme } from "@mui/material/styles";

declare module "@mui/material/styles" {
  interface CustomTheme {
    custom?: {
      spacing?: {
        input?: string;
      };
    };
  }

  interface Theme extends CustomTheme {}
  interface ThemeOptions extends CustomTheme {}
}

const main = createTheme({
  typography: {
    fontSize: 12,
    h1: {
      fontSize: "3rem",
    },
  },
  custom: {
    spacing: {
      input: "1.5rem",
    },
  },
});

export default main;