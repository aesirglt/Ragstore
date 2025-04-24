export interface PageResult<T> {
    items: T[];
    nextLink?: string;
    count?: number;
} 