using System;

namespace WebSwIT.ViewModels.Technologies
{
    public class CreateTechnologyModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
