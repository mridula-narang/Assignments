namespace ProfilePageApp.Models
{
    public class Profile
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string LinkedIn { get; set; }
        public string GitHub { get; set; }
        public string ProfilePicture { get; set; }
        public Skill[] Skills { get; set; }
        public Project[] Projects { get; set; }
    }

    public class Project
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }

    public class Skill
    {
        public string SkillName { get; set; }
        public string Image { get; set; }
    }
}
