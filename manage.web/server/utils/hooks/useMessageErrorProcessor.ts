import { IMessageErrorProcessor } from "../errorMessageProcessor/IMessageErrorProcessor";
import { ZodMessageErrorProcessor } from "../errorMessageProcessor/ZodMessageErrorProcessor";

export function useMessageErrorProcessor(): IMessageErrorProcessor<unknown> {
    return new ZodMessageErrorProcessor();
}