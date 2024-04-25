import MenuNavComponent from "./navegation/menu-ul.component";
import styles from './menu-bar.module.scss';

const MenuBarComponent = () => {
    return (
        <div className={styles.mainDiv}>
            <div className="ml-56">
                LOGO
            </div>
            <MenuNavComponent/>
            <div className="mr-56">
                <button className="bg-slate-400 hover:bg-slate-700 text-white font-bold py-2 px-4 rounded"> profile </button>
            </div >
        </div>
    )
}

export default  MenuBarComponent;