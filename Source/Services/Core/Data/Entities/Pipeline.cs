namespace Aurora.Core.Data.Entities
{
    public class Pipeline
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<int, string> Steps { get; private set; }
        public int ProjectId { get; private set; }
        public Project Project { get; private set; }

        public Pipeline(string name, string description, Project project)
        {
            Name = name;
            Description = description;
            Steps = new();
            Project = project;
            ProjectId = project.Id;
        }

        private Pipeline()
        {
            // Required by EF Core
            Name = null!;
            Description = null!;
            Steps = null!;
            Project = null!;
        }
    }
}
