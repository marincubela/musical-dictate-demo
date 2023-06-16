import "../styles/card.css"

export function AssignmentCard(props) {
    return (<>
        <div className="card" onClick={props.onClick}>
            <div className="exercisecard">
                <div>
                    <div className="exercisecard-group">
                        {props.group}
                    </div>
                    <div className="exercisecard-title">
                        {props.exerciseTitle}
                    </div>
                </div>
                <div className="exercisecard-bottom">
                    <div>
                        Ocjenjivač:
                    </div>
                    <div>
                        {props.graderType}
                    </div>
                </div>
                <div className="exercisecard-bottom">
                    <div>
                        {props.teacher}
                    </div>
                    <div>{props.date}</div>
                </div>
            </div>
        </div>
    </>)
}