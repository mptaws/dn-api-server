import './App.css';
import TodoList from './components/TodoList'
import 'bootstrap/dist/css/bootstrap.min.css'
import React from 'react';

class App extends React.Component {
  constructor() {
    super();
    this.state = {
      items: [],
      isLoaded: false,
    };
  }

  componentDidMount() {
    fetch('http://localhost:5000/api/todos')
      .then(res => res.json())
      .then(json => {
        this.setState({
          isLoaded: true,
          items: json,
        })
      });
  }

  render() {

    var { isLoaded, items } = this.state;

    if (!isLoaded) {
      return <></>
    } else{
        return (
        <div className="App">
          <TodoList itemList={items} />
        </div>
      );
    }
  }
}

export default App;