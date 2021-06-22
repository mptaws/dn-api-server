import React, { useState , useEffect} from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

const EditTask = ({modal, toggle, updateTask, taskObj}) => {
    const [taskTitle, setTaskTitle] = useState('');
    const [taskCompleted, setTaskCompleted] = useState('');
    const [taskDescription, setTaskDescription] = useState('');


    const [dropdownOpen, setDropdownOpen] = useState(false);
    const toggleDropDown = () => setDropdownOpen(prevState => !prevState);
    const [dropDownValue, setDropdownValue] = useState('False');


    const handleChangeDropDown = (e) => {
        setDropdownValue(e.target.textContent)
    }

    const handleChange = (e) => {
        
        const {name, value} = e.target

        if(name === "taskTitle"){
            setTaskTitle(value)
        } else if (name === "taskDescription") {
            setTaskDescription(value)
        }else{
            setTaskCompleted(value)
        }


    }

    useEffect(() => {
        setTaskTitle(taskObj.taskTitle)
        setTaskCompleted(taskObj.completed)
        setTaskDescription(taskObj.taskDescription)
    },[])

    const handleUpdate = (e) => {
        e.preventDefault();
        let tempObj = {}
        tempObj['id'] = taskObj.id
        tempObj['title'] = taskTitle
        tempObj['completed'] = taskCompleted
        tempObj['description'] = taskDescription
        updateTask(tempObj)
    }

    return (
        <Modal isOpen={modal} toggle={toggle}>
            <ModalHeader toggle={toggle}>Update Task</ModalHeader>
            <ModalBody>
            
                    <div className = "form-group">
                        <label>Task Name</label>
                        <input type="text" className = "form-control" placeholder= {taskObj.title} value={taskTitle} onChange = {handleChange} name = "taskTitle"/>
                    </div>
                    <div className="form-group form-inline">
                        <label style={{marginRight: '10px'}}>Completed</label>
                        <Dropdown isOpen={dropdownOpen} toggle={toggleDropDown} size="sm">
                        <DropdownToggle caret>
                            {dropDownValue}
                        </DropdownToggle>
                        <DropdownMenu right>
                            <DropdownItem>
                                <div onClick={handleChangeDropDown}>False</div>
                            </DropdownItem>
                            <DropdownItem>
                                <div onClick={handleChangeDropDown}>True</div>
                            </DropdownItem>
                        </DropdownMenu>
                        </Dropdown>
                    </div>
                    <div className = "form-group">
                        <label>Task Description</label>
                        <input type="text" className = "form-control" placeholder= {taskObj.description} value={taskDescription} onChange = {handleChange} name = "taskDescription"/>
                    </div>
                
            </ModalBody>
            <ModalFooter>
            <Button color="primary" onClick={handleUpdate}>Update</Button>{' '}
            <Button color="secondary" onClick={toggle}>Cancel</Button>
            </ModalFooter>
      </Modal>
    );
};

export default EditTask;