import LoginForm from "@/components/LoginForm";
import { Container } from "@/components/ui/Container";
import Link from "next/link";

export default function Home() {
  return <Container>
    <LoginForm mode="login" />
    <Link className="text-blue-600" href={"/register"}>Create your account</Link>
  </Container>;
}
