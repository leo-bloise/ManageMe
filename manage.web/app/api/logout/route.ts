import { deleteSessionTokenCache } from "@/server/session";
import { NextResponse } from "next/server";

export async function GET(request: Request) {
    await deleteSessionTokenCache();

    return NextResponse.redirect(new URL('/', request.url));
}