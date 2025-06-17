import React, { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import { AuthService } from "../../../api/auth/AuthService";
import { useAuth } from "../../../hooks/useAuth";

// ----------------- TYPES -----------------
type FormValues = {
  username?: string;
  email: string;
  password: string;
};

const Login: React.FC = () => {
  const [isRegistering, setIsRegistering] = useState(false);
  const { login, user } = useAuth();

  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
  } = useForm<FormValues>({
    defaultValues: {
      username: "",
      email: "",
      password: "",
    },
  });

  const onSubmit = async (data: FormValues) => {
    try {
      if (isRegistering) {
        await AuthService.register({
          email: data.email,
          username: data.username!,
          password: data.password,
        });
        alert("Registration successful! Please log in.");
        setIsRegistering(false);
        reset();
      } else {
        const res = await AuthService.login({
          email: data.email,
          password: data.password,
        });
        login(res.data.access_token);
      }
    } catch (err: any) {
      alert(err.response?.data?.message || "Something went wrong");
    }
  };

  return (
    <div className="max-w-md mx-auto mt-12 p-6 border border-gray-200 rounded-md shadow-sm">
      <h2 className="text-2xl font-semibold mb-1 text-center">
        {isRegistering ? "Create Account" : "Hello,"}
      </h2>
      <p className="text-center text-sm mb-6 text-gray-600">
        {isRegistering
          ? "Sign up to AuraShop and start shopping!"
          : "Login to AuraShop or create an account and don't miss the deals!"}
      </p>

      <div className="mb-4 flex border-b border-gray-300">
        <button
          type="button"
          className={`flex-1 py-3 font-semibold border-b-2 focus:outline-none ${
            !isRegistering
              ? "text-orange-600 border-orange-600"
              : "text-gray-500"
          }`}
          onClick={() => setIsRegistering(false)}
        >
          Login
        </button>
        <button
          type="button"
          className={`flex-1 py-3 font-semibold border-b-2 focus:outline-none ${
            isRegistering
              ? "text-orange-600 border-orange-600"
              : "text-gray-500"
          }`}
          onClick={() => setIsRegistering(true)}
        >
          Sign Up
        </button>
      </div>

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        {isRegistering && (
          <div>
            <label className="block mb-1 font-semibold text-gray-700">
              Username
            </label>
            <Controller
              name="username"
              control={control}
              rules={{
                required: "Username is required",
                minLength: {
                  value: 3,
                  message: "Username must be at least 3 characters",
                },
              }}
              render={({ field }) => (
                <input
                  type="text"
                  {...field}
                  placeholder="Enter a username"
                  className={`w-full border rounded px-3 py-2 focus:outline-none ${
                    errors.username ? "border-red-500" : "border-gray-300"
                  }`}
                />
              )}
            />
            {errors.username && (
              <p className="text-red-500 text-sm mt-1">
                {errors.username.message}
              </p>
            )}
          </div>
        )}

        <div>
          <label className="block mb-1 font-semibold text-gray-700">
            Email
          </label>
          <Controller
            name="email"
            control={control}
            rules={{
              required: "Email is required",
              pattern: {
                value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                message: "Invalid email format",
              },
            }}
            render={({ field }) => (
              <input
                type="text"
                {...field}
                placeholder="Enter your email"
                className={`w-full border rounded px-3 py-2 focus:outline-none ${
                  errors.email ? "border-red-500" : "border-gray-300"
                }`}
              />
            )}
          />
          {errors.email && (
            <p className="text-red-500 text-sm mt-1">{errors.email.message}</p>
          )}
        </div>

        <div>
          <label className="block mb-1 font-semibold text-gray-700">
            Password
          </label>
          <Controller
            name="password"
            control={control}
            rules={{
              required: "Password is required",
              minLength: {
                value: isRegistering ? 6 : 1,
                message: isRegistering
                  ? "Password must be at least 6 characters"
                  : "Password is required",
              },
            }}
            render={({ field }) => (
              <input
                type="password"
                {...field}
                placeholder="Enter your password"
                className={`w-full border rounded px-3 py-2 focus:outline-none ${
                  errors.password ? "border-red-500" : "border-gray-300"
                }`}
              />
            )}
          />
          {!isRegistering && (
            <div className="text-right mt-1">
              <a href="#" className="text-sm text-gray-500 hover:text-gray-700">
                Forgot Password?
              </a>
            </div>
          )}
          {errors.password && (
            <p className="text-red-500 text-sm mt-1">
              {errors.password.message}
            </p>
          )}
        </div>

        <button
          type="submit"
          disabled={isSubmitting}
          className="w-full bg-orange-500 hover:bg-orange-700 transition text-white font-bold py-3 rounded-md uppercase"
        >
          {isSubmitting
            ? isRegistering
              ? "Signing up..."
              : "Logging in..."
            : isRegistering
            ? "SIGN UP"
            : "LOGIN"}
        </button>
      </form>
    </div>
  );
};

export default Login;
