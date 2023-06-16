import { Box, Tab, Tabs } from "@mui/material";
import { SmallTitle, Title } from "../components/Title";
import { useInput } from "../components/Input";
import { StudentListHeader, StudentListItem } from "../components/StudentListItem";
import { ExerciseCard } from "../components/ExerciseCard";
import { TabPanel } from "../components/TabPanel";
import { useNavigate, useParams } from "react-router-dom";
import { useState } from "react";
import { useEffect } from "react";
import GroupsService from "../api/services/Groups";
import StudentsService from "../api/services/Students";
import ExercisesService from "../api/services/Exercises";
import "../styles/groups.css";
import "../styles/input.css";
import "../styles/main.css";
import "../styles/card.css";
import "../styles/common.css";
import "../styles/studentList.css";
import { AssignmentCard } from "../components/AssignmentCard";
import { CreateAssignmentModal } from "./Assignment";

function a11yProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

export function Group(props) {
    const navigate = useNavigate();
    const params = useParams();

    const [value, setValue] = useState(0);
    const [group, setGroup] = useState();
    const [students, setStudents] = useState([]);
    const [assignments, setAssignments] = useState([]);
    const [allStudents, setAllStudents] = useState([]);
    const [allExercises, setAllExercises] = useState([]);
    const [isOpenAssignment, setIsOpenAssignment] = useState(false);
    const [exercise, setExercise] = useState(null)

    const [groupName, setGroupName, groupNameInput] = useInput({ type: "text", label: "Ime grupe:" })

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    useEffect(() => {
        (async () => {
            const group = await GroupsService.getGroup(params.groupId);
            setGroup(group);
            setGroupName(group.name);
            setStudents(group.students)
            setAssignments(group.assignments)

            var allStudents = await StudentsService.getStudents();
            setAllStudents(allStudents.filter(x => !group.students.map(s => s.id).includes(x.id)));

            var exercises = await ExercisesService.getExercises();
            setAllExercises(exercises);
        })()
    }, [])

    const deleteStudent = (student) => {
        setStudents(students.filter(s => s.id !== student.id))
        setAllStudents(oldArray => [...oldArray, student])
    }

    const addStudent = (student) => {
        setAllStudents(allStudents.filter(s => s.id !== student.id))
        setStudents(oldArray => [...oldArray, student])
    }

    const deleteAssignment = (assignment) => {
        setAssignments(assignments.filter(s => !(s.exercise.id === assignment.exercise.id && s.graderType === assignment.graderType)))
    }

    const addAssignment = (assignment) => {
        setAssignments(oldArray => [...oldArray, { exercise: exercise, exerciseId: assignment.exercise.id, graderType: assignment.graderType }])
    }

    const updateGroup = async () => {
        await GroupsService.updateGroup({
            id: group.id,
            name: groupName,
            assignments: assignments,
            studentIds: students.map(s => s.id)
        })
        navigate("/groups");
    }

    const deleteGroup = async () => {
        await GroupsService.deleteGroup(group.id)
    }

    return (<>
        <div className="group-container">
            <Title title={groupName}></Title>
            <Box sx={{ width: '100%' }}>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <Tabs value={value} onChange={handleChange} aria-label="basic tabs example" centered variant="fullWidth">
                        <Tab label="Informacije" {...a11yProps(0)} />
                        <Tab label="Studenti" {...a11yProps(1)} />
                        <Tab label="Zadaci" {...a11yProps(2)} />
                    </Tabs>
                </Box>
                <TabPanel value={value} index={0}>
                    <div className="box-container">
                        <SmallTitle title="Informacije" />
                        <div className="inputs-container">
                            {groupNameInput}
                        </div>
                    </div>
                </TabPanel>
                <TabPanel value={value} index={1}>
                    <div className="box-container">
                        <SmallTitle title="Studenti u grupi" />
                        <div className="studentList-container">
                            <div className="studentListItem-withDelete">
                                <StudentListHeader jmbag="Jmbag" firstName="Ime" lastName="Prezime" nameClass="Razred" />
                                <div className="studentListItem-delete-box"></div>
                            </div>
                            {
                                students.map(student => <>
                                    <div className="studentListItem-withDelete" key={student.id}>
                                        <StudentListItem jmbag={student.jmbag} firstName={student.firstName} lastName={student.lastName} nameClass={student.nameClass} />
                                        <button className="studentListItem-delete-box studentListItem-delete-button" onClick={() => deleteStudent(student)}></button>
                                    </div>
                                </>
                                )
                            }
                        </div>
                        <SmallTitle title="Svi studenti" />
                        <div className="studentList-container">
                            <div className="studentListItem-withDelete">
                                <StudentListHeader jmbag="Jmbag" firstName="Ime" lastName="Prezime" nameClass="Razred" />
                                <div className="studentListItem-delete-box"></div>
                            </div>
                            {
                                allStudents.map(student => <>
                                    <div className="studentListItem-withDelete" key={student.id}>
                                        <StudentListItem jmbag={student.jmbag} firstName={student.firstName} lastName={student.lastName} nameClass={student.nameClass} />
                                        <button className="studentListItem-delete-box studentListItem-add-button" onClick={() => addStudent(student)}></button>
                                    </div>
                                </>
                                )
                            }
                        </div>
                    </div>
                </TabPanel>
                <TabPanel value={value} index={2}>
                    <div className="box-container">
                        <SmallTitle title="Zadaci u grupi" />
                        <div className="card-container">
                            {assignments.map(assignment => {
                                return <>
                                    <AssignmentCard
                                        onClick={() => deleteAssignment(assignment)}
                                        group={groupName}
                                        exerciseTitle={assignment.exercise.title}
                                        graderType={assignment.graderType}
                                        teacher={assignment.exercise.teacher.firstName + " " + assignment.exercise.teacher.lastName}
                                        date={new Date(assignment.exercise.createdUtc).toLocaleDateString()}
                                    />
                                </>
                            })}
                        </div>
                        <SmallTitle title="Svi zadaci" />
                        <div className="card-container">
                            {allExercises.map(exercise => {
                                return <>
                                    <ExerciseCard
                                        onClick={() => {
                                            setExercise(exercise)
                                            setIsOpenAssignment(true)
                                        }}
                                        group={groupName}
                                        exerciseTitle={exercise.title}
                                        teacher={exercise.teacher.firstName + " " + exercise.teacher.lastName}
                                        date={new Date(exercise.createdUtc).toLocaleDateString()}
                                    />
                                </>
                            })}
                        </div>
                    </div>
                    {isOpenAssignment ? <CreateAssignmentModal exercise={exercise} addAssignment={addAssignment} setIsOpen={setIsOpenAssignment} /> : ""}
                </TabPanel>
            </Box>
            <div className="button-container">
                <button className="button" onClick={() => updateGroup()}>Spremi promjene</button>
                <button className="button" onClick={() => deleteGroup()}>Obriši grupu</button>
                <button className="button" onClick={() => navigate(-1)}>Odustani</button>
            </div>
        </div>
    </>)
}