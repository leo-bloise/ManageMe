import * as z from 'zod';
import { IMessageErrorProcessor } from './IMessageErrorProcessor';

export class ZodMessageErrorProcessor implements IMessageErrorProcessor<z.ZodError> {
    processErrorMessage(data: z.ZodError<unknown>): { [key: string]: string; } {
        const fieldErrorMessage: { [key: string]: string } = {};

        data.issues.forEach(issue => {
            console.log(issue);
            fieldErrorMessage[issue.path.join('.')] = issue.message;    
        });

        return fieldErrorMessage;
    }
}