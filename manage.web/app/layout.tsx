import type { Metadata } from "next";
import "./globals.css";
import SideMenu from "@/components/SideMenu";

export const metadata: Metadata = {
  title: "ManageMe",
  description: "Manage your finances easily with me.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
        className={`antialiased flex min-h-screen min-w-screen`}
      >
        <SideMenu />
        {children}
      </body>
    </html>
  );
}
