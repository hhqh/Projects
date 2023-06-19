import "../css/header.css"
import Profile from '../images/profile.png';
import Location from '../images/location.png';
import Mail from '../images/mail.png';
import LinkedIn from '../images/linkedin.png';
import GitHub from '../images/github.png';
import Resume from '../images/HelenHuangResume.pdf';

const Header = () => {
    return (
        <div className="intro-container">
            <div className="profile-container">
                <img className="profile-image" src={Profile} alt="profile"/>
                <p>HELEN HUANG</p>
                <p>Full Stack Developer</p>
                <p>
                    <img className="icon" src={Location} alt="location"/>
                    Seattle, Washington
                </p>
                <br/>
                <p>
                    <a href="mailto:helenqinhuang@gmail.com">
                        <button className="contact-button" onclick="https://www.scaler.com/topics/how-to-make-a-button-link-to-another-page-in-html/">
                            <img src={Mail} alt="mail"/>
                        </button>  
                    </a>
                    <a href="https://www.linkedin.com/in/helenqinhuang/">
                        <button className="contact-button">
                            <img src={LinkedIn} alt="linkedin"/>
                        </button>
                    </a>  
                    <a href="https://github.com/hhqh/Projects/">
                        <button className="contact-button">
                            <img src={GitHub} alt="github"/>
                        </button>  
                    </a>
                </p>
            </div>
            <div className="cv-container">
                <a href={Resume} download="HelenHResume.pdf">
                    <button>
                        Download CV
                    </button>
                </a>
            </div>
        </div>
    )
}

export default Header