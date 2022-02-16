using System.Collections.Generic;

namespace WebSwIT.Entities.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SvgImage { get; set; }

        public List<Technology> Technologies { get; set; }

        public Category()
        {
            Technologies = new List<Technology>();
        }
    }
}
