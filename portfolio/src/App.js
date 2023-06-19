import Intro from './components/intro'
import Experience from './components/experience'
import Skill from './components/skill'
import Project from './components/project'
import Header from './components/header'
import NavBar from './components/navbar'

import './css/app.css'

function App() {
  return (
    <div className='app'>
      <div className='intro-box'>
        <Header />
      </div>
      <div className='info-container'>
        <div className='nav-bar'>
            <NavBar />
        </div>
        <div className='content'>
            <Intro />
            <Experience />
            <Skill />
            <Project />
        </div>
      </div>
    </div>
  );
}

export default App;
