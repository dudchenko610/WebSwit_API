using System;

namespace WebSwIT.ViewModels.WorkSamples
{
    public class  UpdateWorkSampleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShowOnHome { get; set; }
        public Guid TechnologyId { get; set; }
        public string Link { get; set; }
    }
}
