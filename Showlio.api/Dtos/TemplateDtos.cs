namespace Showlio.api.Dtos
{
    public class TemplateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
    }

    public class AdminTemplateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateTemplateDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
    }

    public class UpdateTemplateDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
        public bool IsActive { get; set; }
    }

}
