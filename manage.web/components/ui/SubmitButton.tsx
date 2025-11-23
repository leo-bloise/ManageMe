import { LoadingSpinner } from "./LoadingSpinner";

type Props = {
    message: string;
    loading?: boolean;
}

export function SubmitButton({ message, loading }: Props) {
    const inputCssBase = "border-none font-bold p-1 rounded-md bg-blue-200 text-black";
    const inputCss = loading ? inputCssBase + " opacity-50 cursor-not-allowed flex items-center justify-center" : inputCssBase + ' cursor-pointer';

    return <button disabled={loading} type="submit" className={`${inputCss}`}>
        {!loading && message}
        {loading && <LoadingSpinner />}
    </button>
}