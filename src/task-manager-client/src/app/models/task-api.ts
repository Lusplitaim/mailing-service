import { UrlPath } from "./url-path";

export interface TaskApi {
    id: number;
    name: string;
    url: string;
    description: string;
    urlPaths?: UrlPath[];
}