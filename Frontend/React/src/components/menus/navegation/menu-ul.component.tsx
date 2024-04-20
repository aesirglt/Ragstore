import MenuItemComponent from "./molecules/menu-li-item.component";
import styles from './menu-ul.module.scss'

export default function MenuNavComponent(){
    return (
        <ul className={styles.ulClass}>
            <MenuItemComponent title='Home'/>
            <MenuItemComponent title='Lojas'/>
            <MenuItemComponent title='Contato'/>
        </ul>
    )
}