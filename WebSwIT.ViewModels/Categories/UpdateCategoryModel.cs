using System;

namespace WebSwIT.ViewModels.Categories
{
    public class UpdateCategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SvgImage { get; set; }
    }
}
