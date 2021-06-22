import React, {useState} from 'react';
import CreateTask from '../modals/CreateTask'
import TodoCard from './TodoCard'
const TodoList = ({itemList}) => {
    const [modal, setModal] = useState(false);
    const [taskList, setTaskList] = useState(itemList);

    const baseURL = 'http://localhost:5000/api/todos';

    const POST = (task) => {
        const isCompleted = task['completed'] === 'True' ? true : false
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                title: task["title"],
                completed: isCompleted,
                description: task["description"],

            })
        };
        fetch(`${baseURL}/`, requestOptions)
            .then(() => window.location.reload())
    }

    const PUT = (title, id, taskCompleted, description) => {
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                title: title,
                completed: taskCompleted,
                description: description,
            })
        };
        fetch(`${baseURL}/${id}`, requestOptions)
            .then((result) => window.location.reload())
    }

    const DELETE = (id) => {
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
        };
        fetch(`${baseURL}/${id}`, requestOptions)
            .then((result) => window.location.reload())
    }

    const completeTask = (item) => {
        let title = item.title
        let id = item.id
        item.completed = !item.completed
        let completed = item.completed
        let description = item.description
        PUT(title, id, completed, description)
      }

    
    const deleteTask = (id) => {
        DELETE(id);
    }

    const toggle = () => {
        setModal(!modal);
    }

    const addTask = (taskObj) => {
        let tempList = taskList
        tempList.push(taskObj)
        POST(taskObj)
        setTaskList(tempList)
        setModal(false)
    }

    const updateTask = (item) => {
        let id = item.id
        let title = item.title
        let completed = item.completed
        let description = item.description
        {console.log(id, title, completed, description)}
        PUT(title, id, completed, description)
    }



    return (
        <>
            <div className="header text-center">
                <h3>Todo List</h3>
                <button className="btn btn-primary mt-2" onClick= {() => setModal(true)}>Create Task</button>
            </div>
            <div className="task-container">
                {taskList.map((item) => (
                    <div key={item.id}>
                        <TodoCard updateTask={updateTask} addTask={addTask} completeTask={completeTask} deleteTask={deleteTask} item={item} />
                    </div>
                ))}
            </div>
            <CreateTask addTask={addTask} toggle = {toggle} modal = {modal}/> 
        </>

    );
};

export default TodoList;