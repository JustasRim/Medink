import { useForm } from "react-hook-form";
import * as yup from "yup";
import YupPassword from "yup-password";
import { yupResolver } from "@hookform/resolvers/yup";
import ax from "../utilities/Axios";
import {
  Box,
  TextField,
  Button,
  FormControl,
  FormLabel,
  RadioGroup,
  FormControlLabel,
  Radio,
  Typography,
  useTheme,
} from "@mui/material";
import { Link } from "react-router-dom";

YupPassword(yup);

const schema = yup.object().shape({
  name: yup.string().required(),
  lastName: yup.string().required(),
  email: yup.string().email().required(),
  role: yup.string().required(),
  password: yup.string().password(),
  passwordConfirmation: yup
    .string()
    .oneOf([yup.ref("password"), null], "Passwords must match"),
});

type inputs = {
  name: string;
  lastName: string;
  email: string;
  password: string;
  passwordConfirmation: string;
  role: "patient" | "medic";
};

const Registration = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<inputs>({
    resolver: yupResolver(schema),
  });

  const theme = useTheme();

  const onSubmit = async (data: inputs) => {
    const response = await ax.post("/Authentication/sign-up", data);
    console.log(response);
  };

  return (
    <Box
      display="flex"
      justifyContent="center"
      alignItems="center"
      flexDirection="column"
      minHeight="100vh"
    >
      <Typography variant="h1" sx={{ mb: 3 }}>
        Register
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
          label="Name"
          variant="outlined"
          {...register("name")}
          sx={{ mb: theme.custom?.spacing?.input, width: "100%" }}
          error={!!errors.name}
        />
        <TextField
          label="Last name"
          variant="outlined"
          {...register("lastName")}
          sx={{ mb: theme.custom?.spacing?.input, width: "100%" }}
          error={!!errors.lastName}
        />

        <TextField
          label="Password"
          type="password"
          variant="outlined"
          {...register("password")}
          sx={{ mb: theme.custom?.spacing?.input, width: "100%" }}
          error={!!errors.password}
        />

        <TextField
          label="Confirm password"
          type="password"
          variant="outlined"
          sx={{ mb: theme.custom?.spacing?.input, width: "100%" }}
          {...register("passwordConfirmation")}
          error={!!errors.passwordConfirmation}
        />

        <FormControl error={!!errors.role}>
          <FormLabel id="radio-role-group">Register as:</FormLabel>
          <RadioGroup
            aria-labelledby="radio-role-group"
            name="radio-role-group"
          >
            <FormControlLabel
              value="patient"
              {...register("role")}
              control={<Radio />}
              label="Patient"
            />
            <FormControlLabel
              value="medic"
              {...register("role")}
              control={<Radio />}
              label="Medic"
            />
          </RadioGroup>
        </FormControl>

        <Box display="flex" justifyContent="flex-end">
          <Button type="submit" variant="contained" sx={{ mb: 2 }}>
            Submit
          </Button>
        </Box>
        <Link to="/login">Already have an account?</Link>
      </Box>
    </Box>
  );
};

export default Registration;
