import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { ErrorMessage } from "@hookform/error-message";
import ax from "../utilities/Axios";

const schema = yup.object().shape({
  name: yup.string().required(),
  lastName: yup.string().required(),
  email: yup.string().email().required(),
});

type inputs = {
  name: string;
  lastName: string;
  email: string;
};

const Registration = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<inputs>({
    resolver: yupResolver(schema),
  });

  const onSubmit = async (data: inputs) => {
    const response = await ax.post("/Authentication/sign-up", data);
    debugger;
  };

  return (
    <div>
      <form onSubmit={handleSubmit(onSubmit)}>
        <input {...register("name")} />
        <ErrorMessage
          errors={errors}
          name="name"
          render={({ message }) => <p>{message}</p>}
        />
        <input {...register("lastName")} />
        <input {...register("email")} />
        <input type="submit" />
      </form>
    </div>
  );
};

export default Registration;
