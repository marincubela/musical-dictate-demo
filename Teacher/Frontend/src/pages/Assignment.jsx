import { useEffect } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom"
import AssignmentsService from "../api/services/Assignments";
import { SmallTitle, Title } from "../components/Title";
import { useState } from "react";
import StudentSolutionsService from "../api/services/StudentSolution";
import { SolutionCard } from "../components/SolutionCard";
import "../styles/modal.css";
import { UseSelect } from "../components/Select";

export function Assignment() {
    const params = useParams();
    const location = useLocation();
    const navigate = useNavigate();

    const [assignment, setAssignment] = useState();
    const [solutions, setSolutions] = useState([])

    useEffect(() => {
        (async () => {
            const assignmentResponse = await AssignmentsService.getAssignment(params.assignmentId);
            const solutionsResponse = await StudentSolutionsService.getStudentSolutionsByAssignment(params.assignmentId)
            setAssignment(assignmentResponse);
            setSolutions(solutionsResponse)
        })()
    }, [location])

    const chooseSolution = (solutionId) => {
        navigate(`/solutions/${solutionId}`)
    }

    return (<>
        <div className="group-container">
            <Title title={assignment !== undefined ? assignment.exercise.title : ""} />
            <div className="card-container">
                {solutions.length !== 0
                    ? solutions.map(solution => {
                        return <>
                            <SolutionCard
                                onClick={() => chooseSolution(solution.id)}
                                jmbag={solution !== undefined ? solution.student.jmbag : ""}
                                firstName={solution !== undefined ? solution.student.firstName : ""}
                                lastName={solution !== undefined ? solution.student.lastName : ""}
                                date={solution !== undefined ? new Date(solution.createdUtc).toLocaleDateString() : ""}
                                isGraded={solution?.result !== null}
                            />
                        </>
                    })
                    : <SmallTitle title="Nema predanih rješenja za ovaj zadatak" />}
            </div>
        </div>
    </>)
}

export function CreateAssignmentModal({ exercise, addAssignment, setIsOpen }) {
    const [graderType, setGraderType, graderTypeSelect] = UseSelect({
        label: "Odaberite vrstu ocjenjivača",
        options: [{ value: "simple", label: "simple" }, { value: "teacher", label: "teacher" }]
    });

    useEffect(() => {
        setGraderType("teacher")
    }, [])

    return (<>
        <div className="darkBG" onClick={() => setIsOpen(false)} />
        <div className="centered">
            <div className="modal">
                <div className="modalContent">
                    <h1>{exercise?.title}</h1>
                    {graderTypeSelect}
                    <button className="button" onClick={() => addAssignment({exercise, graderType})}>Dodaj</button>
                    <button className="button" onClick={() => setIsOpen(false)}>Odustani</button>
                </div>
            </div>
        </div>
    </>)
}