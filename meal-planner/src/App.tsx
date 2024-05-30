import './App.css'
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom'
import RecipeList from './RecipeList'

const App = () => {

  return (
    <Router>
      <Routes>
        <Route path = '/' element={<RecipeList />}/>
      </Routes>
    </Router>
  )
}

export default App
