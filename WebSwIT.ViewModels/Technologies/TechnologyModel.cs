using System;

namespace WebSwIT.ViewModels.Technologies
{
    public class TechnologyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
