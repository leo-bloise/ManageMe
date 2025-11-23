export abstract class ApiResponse {
    constructor(
        public message: string,
        public data: { [key: string]: string },
        public timestamp: string,
        public success: boolean = false
    ) {}
}