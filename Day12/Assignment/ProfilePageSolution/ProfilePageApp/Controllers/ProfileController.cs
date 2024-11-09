using Microsoft.AspNetCore.Mvc;
using ProfilePageApp.Models;

namespace ProfilePageApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var profile = new Profile
            {
                Name = "Mridula Narang",
                Title = "Inventive Developer Blending Creativity with Emerging Tech",
                Bio = "With a passion for technology that knows no bounds, I’m a versatile developer with expertise spanning AI, mobile apps, and beyond. My journey includes crafting dynamic mobile experiences that captivate users, designing intuitive software solutions, and diving into diverse tech projects that drive innovation. Whether it’s developing robust applications, exploring new frameworks, or tackling complex challenges with creative flair, I bring a curious mind and a knack for problem-solving to every endeavor. From AI to full-stack development, my goal is to blend creativity with emerging tech to build solutions that inspire and make a difference.",
                Email = "narangmg@gmail.com",
                LinkedIn = "https://www.linkedin.com/in/mridulagnarang/",
                GitHub = "https://github.com/mridula-narang",
                ProfilePicture = "/images/profile.jpg",
                Skills = new Skill[]
                {
                    new Skill{SkillName = "Bootstrap", Image = "/images/Bootstrap.png"},
                    new Skill{SkillName = "CSharp", Image = "/images/CSharp.png"},
                    new Skill{SkillName = "CSS", Image = "/images/CSS.png"},
                    new Skill{SkillName = "Django", Image = "/images/Django.png"},
                    new Skill{SkillName = "ExpressJS", Image = "/images/ExpressJS.png"},
                    new Skill{SkillName = "Flutter", Image = "/images/Flutter.png"},
                    new Skill{SkillName = "Git", Image = "/images/Git.png"},
                    new Skill{SkillName = "HTML", Image = "/images/HTML.png"},
                    new Skill{SkillName = "Javascript", Image = "/images/Javascript.png"},
                    new Skill{SkillName = "MongoDB", Image = "/images/MongoDB.png"},
                    new Skill{SkillName = "NodeJS", Image = "/images/NodeJS.png"},
                    new Skill{SkillName = "Python", Image = "/images/Python.png"},
                    new Skill{SkillName = "SQL", Image = "/images/SQL.png"},
                    new Skill{SkillName = "TensorFlow", Image = "/images/TensorFlow.png"}
                },
                Projects = new Project[]
                {
                    new Project { Title = "Zen Mentor: A Yoga Monitoring and Correction Application", Description = "An innovative mobile application that monitors and corrects yoga poses using AI-based pose estimation.", Image = "/images/yoga.png"},
                    new Project { Title = "Sangeeta Sarangha: Unveiling the Essence of Carnatic Ragas", Description = "A app that identifies Carnatic ragas based on real-time audio analysis and machine learning.", Image = "/images/music.png" },
                    new Project { Title = "Harnessing Machine Learning to Decode Chest X-Ray Patterns", Description = "A project utilizing convolutional neural networks to classify chest x-rays into various diagnostic categories.", Image = "/images/chest.png" },
                    new Project { Title = "Bringing Brain MRI Data to Life: Advanced Visualization with Python", Description = "Visualization of brain MRI scans using Python to assist in neurological research and analysis.", Image = "/images/brain.png" }
                }
            };
            return View(profile);
        }
    }
}
