import { useRouter } from "next/navigation";
import { useState } from "react";

type LoginFormState = {
    errors: {
        email?: string;
        password?: string;
        geral?: string;
    }
    loading: boolean;
};

export default function useLogin() {
    const [state, setState] = useState<LoginFormState>({
        loading: false,
        errors: {}
    });
    const router = useRouter();

    const postLogin = async (formData: FormData) => {
        setState(old => ({ ...old, loading: true }));

        const payload = {
            email: formData.get("email"),
            password: formData.get("password"),
        };

        const response = await fetch("/api/auth", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(payload),
        });

        const body = await response.json();

        if (response.status != 200) {
            setState(old => ({
                ...old, loading: false, errors: {
                    ...old.errors,
                    geral: body.message
                }
            }))
        }

        router.push("/dashboard");
    }

    return {
        handleLogin: postLogin,
        loading: state.loading,
        errors: state.errors
    };
}