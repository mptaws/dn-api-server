import React, {useState} from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

const CreateTask = ({addTask, modal, toggle}) => {
    const [dropdownOpen, setDropdownOpen] = useState(false);
    const toggleDropDown = () => setDropdownOpen(prevState => !prevState);
    const [dropDownValue, setDropdownValue] = useState('False');

    const [taskTitle, setTaskTitle] = useState('');

    const [taskDescription, setTaskDescription] = useState('');

    const handleChange = (e) => {
        const {name, value} = e.target

        if(name === "taskTitle") {
            setTaskTitle(value)
        }

        if(name === "taskDescription") {
            setTaskDescription(value)
        }
    }

    const handleChangeDropDown = (e) => {
        setDropdownValue(e.target.textContent)
    }

    const handleSave = () => {
        let taskObj = {}
        taskObj["title"] = taskTitle
        taskObj["completed"] = dropDownValue
        taskObj["description"] = taskDescription
        addTask(taskObj)
    }

    return (
        <Modal isOpen={modal} toggle={toggle}>
            <ModalHeader toggle={toggle}>Create Task</ModalHeader>
            <ModalBody>
                <form>
                    <div className="form-group">
                        <label>Task Name</label>
                        <input type="text" className="form-control" value={taskTitle} onChange={handleChange} name="taskTitle"/>
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
                    <div className="form-group">
                        <label>Description</label>
                        <input type="text" className="form-control" value={taskDescription} onChange={handleChange} name="taskDescription"/>
                    </div>
                </form>
            </ModalBody>
            <ModalFooter>
            <Button color="primary" onClick={handleSave}>Create</Button>{' '}
            <Button color="secondary" onClick={toggle}>Cancel</Button>
            </ModalFooter>
      </Modal>
    );
};

export default CreateTask;