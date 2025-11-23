import { BaseApi } from "./BaseApi";
import { ApiResponse } from "./dtos/ApiResponse";
import { Logger } from "./Logger";

export class AuthApi extends BaseApi {

    constructor(baseUrl: string, token?: string) {
        super(baseUrl, token, new Logger(AuthApi.name));
    }

    async login(email: string, password: string): Promise<ApiResponse> {
        try {
            this.logger.debug('Attempting to log in user');

            const request = this.buildRequest('/auth', 'POST', { email, password });
            const response = await fetch(request);

            if(response.status == 401) {
                this.logger.info('Login failed: Unauthorized');
                return new ApiResponse('Invalid email or password.', {}, Date.now().toString(), false);
            }

            this.logger.debug('Login request sent, processing response');

            const data = await response.json();

            return {...data, success: true } as ApiResponse;
        } catch(error: unknown) {
            this.logger.error(`Error ocurred while performing login call ${error}`);

            return {
                message: 'Login failed due to an internal error',
                data: {},
                timestamp: Date.now().toString(),
                success: false
            }
        }
    }
}