import MenuNavComponent from "./navegation/menu-ul.component";
import styles from './menu-bar.module.scss';

const MenuBarComponent = () => {
    return (
        <div className={styles.mainDiv}>
            <div >
                LOGO
            </div>
            <MenuNavComponent/>
            <div >
                <button className="bg-slate-400 hover:bg-slate-700 text-white font-bold py-2 px-4 rounded"> profile </button>
            </div>
        </div>
    )
}

export default  MenuBarComponent;