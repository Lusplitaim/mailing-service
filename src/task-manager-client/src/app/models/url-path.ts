import { UrlParam } from "./url-param";

export interface UrlPath {
    id: number;
    apiId: number;
    name: string;
    urlParams?: UrlParam[];
}