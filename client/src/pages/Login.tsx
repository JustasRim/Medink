import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import ax from "../utilities/Axios";
import { Box, TextField, Button, Typography, useTheme } from "@mui/material";
import { Link } from "react-router-dom";

const schema = yup.object().shape({
  email: yup.string().email().required(),
  password: yup.string(),
});

type inputs = {
  email: string;
  password: string;
};

const Login = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<inputs>({
    resolver: yupResolver(schema),
  });

  const theme = useTheme();

  const onSubmit = async (data: inputs) => {
    const response = await ax.post("/Authentication/login", data);
    console.log(response);
  };
  return (
    <Box
      display="flex"
      justifyContent="center"
      alignItems="center"
      flexDirection="column"
      marginTop={10}
    >
      <Typography variant="h1" sx={{ mb: 3 }}>
        Log in
      </Typography>
      <Box component="form" maxWidth="300px" onSubmit={handleSubmit(onSubmit)}>
        <TextField
          label="Email"
          variant="outlined"
          {...register("email")}
          sx={{ mb: theme.custom?.spacing?.input, width: "100%" }}
          error={!!errors.email}
        />

        <TextField
          label="Password"
          type="password"
          variant="outlined"
          {...register("password")}
          sx={{ mb: theme.custom?.spacing?.input, width: "100%" }}
          error={!!errors.password}
        />

        <Box display="flex" justifyContent="flex-end">
          <Button type="submit" variant="contained" sx={{ mb: 2 }}>
            Submit
          </Button>
        </Box>
        <Link to="/sign-up">Don't have an account?</Link>
      </Box>
    </Box>
  );
};

export default Login;
