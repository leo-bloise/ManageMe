import { ReactNode } from "react"

type Props = {
    children: ReactNode;
}

export function Container({ children }: Props) {
    return <main className="flex flex-col items-center justify-center min-h-full w-full gap-y-4">
        {children}
    </main>
}