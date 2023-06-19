import '../css/intro.css'

const Intro = () => {
    return (
        <div id="about">
            <h2>About Me</h2>
            <p>
                My name is Helen. I am a software developer and I graduated Bachelors of Science in Computer Science in Seattle University. 
            </p>
            <p>
                I am a mom of two attention seeking orange tabbies. I love kayaking and going to farms to pick produces.
            </p>
            <h3>Education</h3>
            <div className="box-container">
                <div className="box">
                    <p>Bachelors of Science in Computer Science</p>
                    <p>Seattle University</p>
                    <p>09/2020 - 06/2022</p>
                    <p>GPA - 3.97 / Summa Cum Laude</p>     
                </div>
                <div className="box">
                    <p>Associate of Science in Computer Science</p>
                    <p>Seattle Central College</p>
                    <p>09/2018 - 06/2020</p>
                    <p>GPA - 3.8</p>     
                </div>
            </div>
        </div>
    )
}

export default Intro