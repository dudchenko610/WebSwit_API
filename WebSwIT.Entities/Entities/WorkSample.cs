using System;
using System.Collections.Generic;

namespace WebSwIT.Entities.Entities
{
    public class WorkSample : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShowOnHome { get; set; }
        public Guid TechnologyId { get; set; }
        public Technology Technology { get; set; }
        public Guid MainPictureId { get; set; }
        public string Link { get; set; }
        public List<WorkSamplePicture> WorkSamplePictures { get; set; }
    }
}
