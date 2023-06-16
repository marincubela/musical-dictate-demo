import "../styles/notifications.css"

export function Notifications({ messages }) {

    return (
        <div className="notifications-container">
            {messages.map(message => {
                return <div className="notification-list-item">Učenik {message} predao je rješenje.</div>
            })}
        </div>
    )
}