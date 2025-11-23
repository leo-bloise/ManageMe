export interface IMessageErrorProcessor<T> {
    processErrorMessage(data: T): {
        [key: string]: string
    };
}