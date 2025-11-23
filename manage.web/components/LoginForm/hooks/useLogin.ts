import { useRouter } from "next/navigation";
import { useState } from "react";
import { email } from "zod";

type LoginFormState = {
    errors: {
        email?: string;
        password?: string;
        geral?: string;
        name?: string;
    }
    loading: boolean;
    success: boolean;
};

const registerStrategy = async (formData: FormData): Promise<LoginFormState> => {
    const payload = {
        email: formData.get("email"),
        password: formData.get("password"),
        name: formData.get("name"),
    };

    const response = await fetch("/api/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
    });

    const body = await response.json();

    if(response.status != 200) {
        return {
            loading: false,
            errors: {
                ...body.data
            },
            success: false
        }
    }

    return {
        errors: {},
        loading: false,
        success: true
    }
}

const loginStrategy = async (formData: FormData): Promise<LoginFormState> => {
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
        return {
            loading: false,
            errors: {
                geral: body.message
            },
            success: false
        }
    }

    return {
        errors: {},
        loading: false,
        success: true
    }
}

export default function useLogin(mode: 'login' | 'register' = 'login') {
    const [state, setState] = useState<LoginFormState>({
        loading: false,
        errors: {},
        success: true
    });
    const router = useRouter();

    const postLogin = async (formData: FormData) => {
        setState(old => ({ ...old, loading: true }));

        switch(mode) {
            case 'login':
                const result = await loginStrategy(formData);
                if(result.success) {
                    router.push("/dashboard");
                    return;
                }
                
                setState(result);
                break;
            case 'register':
                const resultRegister = await registerStrategy(formData);
                if(!resultRegister.success) {
                    setState(resultRegister);
                    return;
                }
                await loginStrategy(formData);
                router.push("/dashboard");
                break;
            default:
                throw new Error(`Invalid mode ${mode}`);
        }
    }

    return {
        handleLogin: postLogin,
        loading: state.loading,
        errors: state.errors
    };
}