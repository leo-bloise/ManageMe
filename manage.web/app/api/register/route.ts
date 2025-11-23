import { AuthApi } from '@/server/AuthApi';
import { useMessageErrorProcessor } from '@/server/utils/hooks/useMessageErrorProcessor';
import * as z from 'zod';

const RegisterSchema = z.object({
    email: z.string({
        message: 'Email must be valid'
    }).email({
        message: 'Email must be valid'
    }),
    password: z.string({
        message: 'Password must be valid'
    }).min(3, {
        message: 'Password must have at least 3 chars'
    }),
    name: z.string({
        message: 'Name must be valid'
    }).min(3, {
        message: 'Name must have at least 3 chars'
    })
});

export async function POST(request: Request) {
    const messageErrorProcessor = useMessageErrorProcessor();

    try {
        const authApi = new AuthApi(process.env.MANAGE_ME_URL || '');

        const requestData = await request.json();

        const payload = RegisterSchema.parse(requestData);

        const { data, message, success } = await authApi.register(payload.name, payload.email, payload.password);

        if (!success) {
            return new Response(JSON.stringify({
                message,
                data,
                success
            }), { status: 422 });
        }

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