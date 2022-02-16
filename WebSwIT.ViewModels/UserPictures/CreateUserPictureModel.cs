using Microsoft.AspNetCore.Http;
using System;

namespace WebSwIT.ViewModels.UserPictures
{
    public class CreateUserPictureModel
    {
        public IFormFile File { get; set; }
    }
}
