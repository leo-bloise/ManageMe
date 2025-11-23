import { AuthApi } from '@/server/AuthApi';
import { createSessionTokenCookie } from '@/server/session';
import { useMessageErrorProcessor } from '@/server/utils/hooks/useMessageErrorProcessor';
import * as z from 'zod';

const LoginDataSchema = z.object({
    email: z.string({
        message: 'Email must be provided'
    }).email({
        message: 'Email must be valid'
    }),
    password: z.string({
        message: 'Password must be provided'
    }).min(3, {
        message: 'Password must have, at least, 3 chars'
    }),
});

export async function POST(request: Request) {
    const messageErrorProcessor = useMessageErrorProcessor();

    try {
        const authApi = new AuthApi(process.env.MANAGE_ME_URL || '');

        const requestData = await request.json();

        const payload = LoginDataSchema.parse(requestData);

        const { data, message, success } = await authApi.login(payload.email, payload.password);

        if (!success) {
            return new Response(JSON.stringify({
                message,
                data,
                success
            }), { status: 401 });
        }

        if(!data?.token) {
            return new Response(JSON.stringify({
                message: 'Login failed. Please, talk with the administrator.',
                success: false
            }), { status: 500 });
        }

        await createSessionTokenCookie(data.token);

        return new Response(JSON.stringify({
            message,
            success
        }));
    } catch(error: unknown) {
        console.error('Error in /api/auth route:', error);

        if(error instanceof z.ZodError) {
            const fieldErrorMessage: {
                [key: string]: string
            } = messageErrorProcessor.processErrorMessage(error);

            return new Response(JSON.stringify({
                message: 'Invalid request data',
                success: false,
                data: fieldErrorMessage
            }), { status: 422 });
        }

        return new Response(JSON.stringify({
            message: 'Internal server error',
            success: false
        }), { status: 500 });
    }
}