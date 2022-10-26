import axios from "axios";

const ax = axios.create({
  baseURL: "https://localhost:7222/api/v1",
});

export default ax;
