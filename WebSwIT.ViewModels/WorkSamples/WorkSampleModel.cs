using System;
using System.Collections.Generic;
using WebSwIT.ViewModels.WorkSamplePictures;

namespace WebSwIT.ViewModels.WorkSamples
{
    public class WorkSampleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShowOnHome { get; set; }
        public Guid TechnologyId { get; set; }
        public Guid MainPictureId { get; set; }
        public string Link { get; set; }
        public List<WorkSamplePictureModel> Pictures { get; set; }
    }
}
