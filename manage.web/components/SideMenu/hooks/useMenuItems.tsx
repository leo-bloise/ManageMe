import { ReactNode } from "react";
import { BsFillHouseFill } from "react-icons/bs";

type IconFactory = (size: number, color: string) => ReactNode;

type TMenuItem = {
    iconFactory: IconFactory;
    label: string;
    path: string;
};

export default function useMenuItems(): TMenuItem[] {
    const menuItems: TMenuItem[] = [
        {
            iconFactory: (size, color) => <BsFillHouseFill size={size} color={color} />,
            label: 'Home',
            path: '/'
        }
    ];

    return menuItems;
}