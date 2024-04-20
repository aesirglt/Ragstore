import MenuLabelComponent from "../atoms/menu-atom-label.component";
import styles from './menu-li-item.module.scss'


export interface MenuItem {
    title: string
}

export default function MenuItemComponent({ title }: MenuItem){
    return (
        <li className={styles.menuLiItem}>
            <MenuLabelComponent text={title}/>
        </li>
    )
}