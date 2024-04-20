export interface ResponseApi<T> {
    items: T
}
export interface ResponseApiPaged<T> {
    items: T [],
    count: number,
    nextPageLink: string
}