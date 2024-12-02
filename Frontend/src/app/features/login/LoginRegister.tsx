import React, { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import PageLayout from "../../layout/PageLayout";

interface LoginFormInputs {
  email: string;
  password: string;
}

interface RegisterFormInputs {
  email: string;
  password: string;
  confirmPassword: string;
}

function LoginRegister() {
  const [currentTab, setCurrentTab] = useState("login");

  const {
    control: loginControl,
    handleSubmit: handleLoginSubmit,
    formState: { errors: loginErrors },
  } = useForm<LoginFormInputs>();

  const {
    control: registerControl,
    handleSubmit: handleRegisterSubmit,
    formState: { errors: registerErrors },
  } = useForm<RegisterFormInputs>();

  const onLoginSubmit = (data: LoginFormInputs) => {
    console.log("Login data:", data);
  };

  const onRegisterSubmit = (data: RegisterFormInputs) => {
    if (data.password !== data.confirmPassword) {
      console.error("Passwords do not match!");
      return;
    }
    console.log("Register data:", data);
  };

  return (
    <PageLayout>
      <div className="flex justify-center items-center">
        <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-md">
          <div className="flex justify-around border-b mb-4">
            <button
              onClick={() => setCurrentTab("login")}
              className={`w-1/2 py-2 text-center font-medium ${
                currentTab === "login"
                  ? "border-b-2 border-blue-500 text-blue-500"
                  : ""
              }`}
            >
              Login
            </button>
            <button
              onClick={() => setCurrentTab("register")}
              className={`w-1/2 py-2 text-center font-medium ${
                currentTab === "register"
                  ? "border-b-2 border-blue-500 text-blue-500"
                  : ""
              }`}
            >
              Register
            </button>
          </div>

          {currentTab === "login" && (
            <form
              onSubmit={handleLoginSubmit(onLoginSubmit)}
              className="space-y-4"
            >
              <div>
                <label htmlFor="email" className="block text-sm font-medium">
                  Email
                </label>
                <Controller
                  name="email"
                  control={loginControl}
                  defaultValue=""
                  rules={{
                    required: "Email is required",
                    pattern: {
                      value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                      message: "Invalid email format",
                    },
                  }}
                  render={({ field }) => (
                    <input
                      id="email"
                      {...field}
                      type="email"
                      className="w-full px-3 py-2 border rounded-lg"
                    />
                  )}
                />
                {loginErrors.email && (
                  <p className="text-red-500 text-sm">
                    {loginErrors.email.message}
                  </p>
                )}
              </div>
              <div>
                <label htmlFor="password" className="block text-sm font-medium">
                  Password
                </label>
                <Controller
                  name="password"
                  control={loginControl}
                  defaultValue=""
                  rules={{ required: "Password is required" }}
                  render={({ field }) => (
                    <input
                      id="password"
                      {...field}
                      type="password"
                      className="w-full px-3 py-2 border rounded-lg"
                    />
                  )}
                />
                {loginErrors.password && (
                  <p className="text-red-500 text-sm">
                    {loginErrors.password.message}
                  </p>
                )}
              </div>
              <button
                type="submit"
                className="w-full py-2 bg-blue-500 text-white rounded-lg"
              >
                Login
              </button>
            </form>
          )}

          {currentTab === "register" && (
            <form
              onSubmit={handleRegisterSubmit(onRegisterSubmit)}
              className="space-y-4"
            >
              <div>
                <label
                  htmlFor="registerEmail"
                  className="block text-sm font-medium"
                >
                  Email
                </label>
                <Controller
                  name="email"
                  control={registerControl}
                  defaultValue=""
                  rules={{
                    required: "Email is required",
                    pattern: {
                      value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                      message: "Invalid email format",
                    },
                  }}
                  render={({ field }) => (
                    <input
                      id="registerEmail"
                      {...field}
                      type="email"
                      className="w-full px-3 py-2 border rounded-lg"
                    />
                  )}
                />
                {registerErrors.email && (
                  <p className="text-red-500 text-sm">
                    {registerErrors.email.message}
                  </p>
                )}
              </div>
              <div>
                <label
                  htmlFor="registerPassword"
                  className="block text-sm font-medium"
                >
                  Password
                </label>
                <Controller
                  name="password"
                  control={registerControl}
                  defaultValue=""
                  rules={{ required: "Password is required" }}
                  render={({ field }) => (
                    <input
                      id="registerPassword"
                      {...field}
                      type="password"
                      className="w-full px-3 py-2 border rounded-lg"
                    />
                  )}
                />
                {registerErrors.password && (
                  <p className="text-red-500 text-sm">
                    {registerErrors.password.message}
                  </p>
                )}
              </div>
              <div>
                <label
                  htmlFor="confirmPassword"
                  className="block text-sm font-medium"
                >
                  Confirm Password
                </label>
                <Controller
                  name="confirmPassword"
                  control={registerControl}
                  defaultValue=""
                  rules={{ required: "Confirm password is required" }}
                  render={({ field }) => (
                    <input
                      id="confirmPassword"
                      {...field}
                      type="password"
                      className="w-full px-3 py-2 border rounded-lg"
                    />
                  )}
                />
                {registerErrors.confirmPassword && (
                  <p className="text-red-500 text-sm">
                    {registerErrors.confirmPassword.message}
                  </p>
                )}
              </div>
              <button
                type="submit"
                className="w-full py-2 bg-blue-500 text-white rounded-lg"
              >
                Register
              </button>
            </form>
          )}
        </div>
      </div>
    </PageLayout>
  );
}

export default LoginRegister;
