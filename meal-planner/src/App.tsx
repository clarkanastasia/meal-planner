import './App.css'
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom'
import RecipeList from './RecipeList'
import AddRecipe from './AddRecipe'

const App = () => {

  return (
    <Router>
      <Routes>
        <Route path = '/' element={<RecipeList />}/>
        <Route path = '/addrecipe' element={<AddRecipe />} />
      </Routes>
    </Router>
  )
}

export default App
