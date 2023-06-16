import { MenuCard } from "../components/MenuCard";
import { Title } from "../components/Title";
import students from "../images/students.png"
import assignment from "../images/assignment.png"
import settings from "../images/settings.png"
import exercise from "../images/exercise.png"
import "../styles/main.css"

export function StudentMain() {
    return (<>
        <div className="main-container">
            <Title title="Dobro došao nazad!" />
            <div className="menucard-container">
                <MenuCard title="Moje grupe" route="/student/groups" image={students}></MenuCard>
                <MenuCard title="Rezultati" route="/student/main" image={exercise}></MenuCard>
                <MenuCard title="Postavke" route="/student/main" image={settings}></MenuCard>
            </div>
        </div>
    </>)
}