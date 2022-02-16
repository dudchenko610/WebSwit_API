using System;
using System.Collections.Generic;
using WebSwIT.ViewModels.Technologies;

namespace WebSwIT.ViewModels.Categories
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SvgImage { get; set; }
        public List<TechnologyModel> Technologies { get; set; }
    }
}
