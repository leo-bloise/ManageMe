import { Logger } from "./Logger";

export abstract class BaseApi {
    constructor(protected baseUrl: string, protected token?: string, protected logger: Logger = new Logger(BaseApi.name)) {}

    protected buildRequest(path: string, method: string, body?: any): Request {
        const headers: HeadersInit = {
            'Content-Type': 'application/json',
            'Authorization': this.token ? `Bearer ${this.token}` : '',
        };

        return new Request(`${this.baseUrl}${path}`, {
            method,
            headers,
            body: body ? JSON.stringify(body) : undefined,
        });
    }
}