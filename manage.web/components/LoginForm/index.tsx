'use client';

import { SubmitButton } from "../ui/SubmitButton";
import useLogin from "./hooks/useLogin";

type Props = {
    mode: 'login' | 'register';
}

const renderNameInput = (errors: { [key: string]: string }) => {
    return <>
        <label className="flex flex-col indent-1 gap-y-2">
            Name:
            <input placeholder="Name" className="rounded p-1 pl-2 outline-none bg-gray-200" type="text" name="name" id="name" />
        </label>
        {errors.name && <span className="text-red-500">{errors.name}</span>}
    </>
}

export default function LoginForm({ mode }: Props) {
    const {
        errors,
        handleLogin,
        loading
    } = useLogin(mode);

    return <form
        onSubmit={event => {
            event.preventDefault();
            handleLogin(new FormData(event.currentTarget));
        }}
        className="flex flex-col gap-y-4">
        {
            mode === 'register' && renderNameInput(errors)
        }
        <label className="flex flex-col indent-1 gap-y-2 cursor-pointer">
            Email:
            <input placeholder="email@email.com" className="rounded p-1 pl-2 outline-none bg-gray-200" type="email" name="email" id="email" />
        </label>
        {errors.email && <span className="text-red-500">{errors.email}</span>}
        <label className="flex flex-col indent-1 gap-y-2">
            Password:
            <input placeholder="Password" className="rounded p-1 pl-2 outline-none bg-gray-200" type="password" name="password" id="password" />
        </label>
        {errors.password && <span className="text-red-500">{errors.password}</span>}
        {errors.geral && <span className="text-red-500">{errors.geral}</span>}
        <SubmitButton message={mode == 'login' ? 'Login' : 'Register'} loading={loading} />
    </form>
}