import { NextRequest, NextResponse } from "next/server";
import { getSessionTokenCache } from "./server/session";
import { Logger } from "./server/Logger";

const anonymousUrls = ['/', '/register'];

const extractPath = (url: string) => {
    const parsed = new URL(url);
    return parsed.pathname;
};

const logger = new Logger('proxy-middleware');

export async function proxy(request: NextRequest) {
    const path = extractPath(request.url);
    
    logger.info(`Proxy middleware invoked for path: ${path}`);

    const token = await getSessionTokenCache();

    const isAnonymousRoute = anonymousUrls.includes(path);

    if(isAnonymousRoute && token) {
        return NextResponse.redirect(new URL('/dashboard', request.url));
    }

    if (!isAnonymousRoute && !token) {
        return NextResponse.redirect(new URL('/', request.url));
    }

    return NextResponse.next();
}

export const config = {
    matcher: [
        // exclude: '/', api, next static, images, png
        '/((?!api|_next/static|_next/image|.*\\.png$).*)',
    ]
};
