import { createTheme } from '@mui/material/styles';

declare module '@mui/material/styles' {
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
  palette: {
    primary: {
      main: '#fa8072',
    },
    secondary: {
      main: '#72ecfa',
    },
  },
  typography: {
    fontSize: 12,
    h1: {
      fontSize: '3rem',
    },
    h2: {
      fontSize: '2rem',
    },
    h3: {
      fontSize: '1rem',
    },
  },
  custom: {
    spacing: {
      input: '1.5rem',
    },
  },
});

export default main;
