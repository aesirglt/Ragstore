import styles from './menu-atom-label.module.scss'

export interface MenuLabel {
    text: string
}
//backgroundcolor: #7f00ff
//foreground: #00FF00
export default function MenuLabelComponent({ text } : MenuLabel) {
    return (
        <a href="#" className={styles.link}>
            <span className={styles.spanText}>{text}</span>
        </a>
    )
}