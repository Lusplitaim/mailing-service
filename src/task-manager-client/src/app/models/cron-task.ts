export interface CronTask {
    id: number;
    name: string;
    description: string;
    minutes: string;
    hours: string;
    days: string;
    months: string;
    weekdays: string;
    userId: number;
    apiId: number;
    urlParamsString: string;
}