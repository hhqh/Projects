import { Link } from "react-scroll";
import '../css/navbar.css';
import Home from '../images/home.png'

const NavBar = () => {
    return (
    <div className="sticky">   
        <button class="image-button">
            <img src={Home} alt="home"/>
        </button>      
        <ul className="list-inline">
            <li>
                <Link activeClass="active" smooth spy to="about"> 
                    ABOUT 
                </Link>
            </li>
            <li>
                <Link activeClass="active" smooth spy to="exp"> 
                    EXPERIENCE
                </Link>
            </li>
            <li>
                <Link activeClass="active" smooth spy to="skills"> 
                    SKILLS
                </Link>
            </li>
            <li>
                <Link activeClass="active" smooth spy to="projects">
                    PROJECTS
                </Link>
            </li>
        </ul>
    </div>
    )
}

export default NavBar;