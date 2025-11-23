import LoginForm from "@/components/LoginForm";
import { Container } from "@/components/ui/Container";

export default function Page() {
    return <Container>
        <LoginForm mode="register" />
    </Container>
}