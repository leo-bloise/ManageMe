'use client';

import { usePathname } from "next/navigation";
import useMenuItems from "./hooks/useMenuItems";

export default function SideMenu() {
    const menuItems = useMenuItems();
    const path = usePathname();

    return <aside className="bg-blue-400 h-screen max-w-20">
        <nav>
            <ul>
                {
                    menuItems.map((item) => (
                        <li key={item.path} className="flex justify-center p-4 cursor-pointer">
                            {item.iconFactory(24, path == item.path ? '#023e8a' : 'white')}
                        </li>
                    ))
                }
            </ul>
        </nav>
    </aside>
}