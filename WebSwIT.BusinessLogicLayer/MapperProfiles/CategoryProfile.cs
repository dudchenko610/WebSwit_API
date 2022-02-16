using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.Categories;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryModel, Category>();
            CreateMap<Category, CreateCategoryModel>();

            CreateMap<CategoryModel, Category>();
            CreateMap<Category, CategoryModel>();

            CreateMap<UpdateCategoryModel, Category>();
            CreateMap<Technology, UpdateCategoryModel>();
        }
    }
}
