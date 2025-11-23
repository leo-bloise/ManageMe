'use client';

import { SubmitButton } from "../SubmitButton";
import useLogin from "./hooks/useLogin";

export default function LoginForm() {
    const {
        errors,
        handleLogin,
        loading
    } = useLogin();

    return <form
        onSubmit={event => {
            event.preventDefault();
            handleLogin(new FormData(event.currentTarget));
        }}
        className="flex flex-col gap-y-4">
        <label className="flex flex-col indent-1 gap-y-2 cursor-pointer">
            Email:
            <input placeholder="email@email.com" className="rounded p-1 pl-2 outline-none bg-gray-200" type="email" name="email" id="email" />
        </label>
        <label className="flex flex-col indent-1 gap-y-2">
            Password:
            <input placeholder="Password" className="rounded p-1 pl-2 outline-none bg-gray-200" type="password" name="password" id="password" />
        </label>
        {errors.geral && <span className="text-red-500">{errors.geral}</span>}
        <SubmitButton message="Login" loading={loading} />
    </form>
}