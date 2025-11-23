import { createSessionTokenCookie } from '@/server/session';
import { NextApiRequest } from 'next';
import { cookies } from 'next/headers';
import * as z from 'zod';

type LoginData = {
    email: string;
    password: string;
}

type SubmitFunction = (payload: LoginData) => Promise<{
    success: boolean;
    message: string;
    data: null | { [key: string]: string }
}>;

const LoginDataSchema = z.object({
    email: z.string().email(),
    password: z.string().min(3),
});

const submitLogin: SubmitFunction = async (payload: LoginData) => {
    try {
        const response = await fetch(`${process.env.MANAGE_ME_URL}/auth`, {
            method: 'POST',
            body: JSON.stringify(
                LoginDataSchema.parse(payload)
            ),
            headers: {
                'Content-Type': 'application/json'
            }
        });
        
        if(response.status == 401) return {
            success: false,
            message: 'Invalid email or password',
            data: null
        }

        const responseBody = await response.json();

        return {
            success: true,
            message: 'Login successful',
            data: {
                token: responseBody.data.token as string
            }
        }

    } catch(error) {
        let errorMap: { [key: string]: string } = {};
        console.error('Error submitting login:', error);

        if(error instanceof z.ZodError) { 
            error.issues.forEach(issue => {
                errorMap[issue.path.join('.')] = issue.message;
            });

            return {
                success: false,
                message: 'Validation errors occurred',
                data: errorMap
            }
        }

        return {
            success: false,
            message: 'An unexpected error occurred',
            data: null
        }
    }
}

export async function POST(request: Request) {
    const requestData = await request.json();

    const {
        data,
        message,
        success
    } = await submitLogin(requestData);    

    console.debug('Login attempt result:', { success, message, data });
    
    if(!success) {
        return new Response(JSON.stringify({
            message,
            data,
            success
        }), { status: 401 });
    }

    await createSessionTokenCookie(data!.token);

    return new Response(JSON.stringify({
        message,
        success
    }));
}