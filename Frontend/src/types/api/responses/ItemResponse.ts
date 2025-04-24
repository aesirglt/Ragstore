import { ItemViewModel } from "../viewmodels/ItemViewModel";
import { PageResult } from "./PageResult";

export interface ItemResponse extends PageResult<ItemViewModel> {
    '@odata.count'?: number;
    value?: ItemViewModel[];
    data?: ItemViewModel;
}