'use client';

import { useRouter } from "next/navigation";

export function LogoutButton() {
    const router = useRouter();

    const handleLogout = async () => {
        const response = await fetch('/api/logout', {
            method: 'GET',
        });

        if(response.status !== 200) {
            console.error('Logout failed');
            return;
        }

        router.push(response.url)
    };

    return <button onClick={e => {
        e.preventDefault()
        handleLogout();
    }}>
        Logout
    </button>
}