using System;

namespace WebSwIT.Entities.Entities
{
    public class WorkSamplePicture : BaseEntity
    {
        public string Name { get; set; }
        public Guid WorkSampleId { get; set; }
        public WorkSample WorkSample { get; set; }
    }
}
