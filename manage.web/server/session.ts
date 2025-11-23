import { cookies } from 'next/headers';

export async function createSessionTokenCookie(token: string) {
    const cookieStore = await cookies();
    const expiresAt = new Date(Date.now() + 7 * 24 * 60 * 60 * 1000);

    cookieStore.set({
        httpOnly: true,
        secure: true,
        expires: expiresAt,
        name: 'token',
        value: token,
    });
}

export async function getSessionTokenCache() {
    const cookieStore = await cookies();
    const tokenCookie = cookieStore.get('token');

    return tokenCookie?.value;
}

export async function deleteSessionTokenCache() {
    const cookieStore = await cookies();

    cookieStore.delete('token');
}