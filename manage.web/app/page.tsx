import LoginForm from "@/components/LoginForm";
import Link from "next/link";

export default function Home() {
  return <main className="flex flex-col items-center justify-center min-h-full w-full gap-y-4">
    <LoginForm />
    <Link className="text-blue-600" href={"/register"}>Create your account</Link>
  </main>;
}
