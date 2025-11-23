import { NextRequest, NextResponse } from "next/server";
import { getSessionTokenCache } from "./server/session";

export async function proxy(request: NextRequest) {
    console.log('Middleware: proxying request', request.url);

    const token = await getSessionTokenCache();

    if (!token) {
        return NextResponse.redirect(new URL('/', request.url));
    }

    return NextResponse.next();
}

export const config = {
    matcher: [
        // exclude: '/', api, next static, images, png
        '/((?!$|api|_next/static|_next/image|.*\\.png$).*)',
    ]
};
