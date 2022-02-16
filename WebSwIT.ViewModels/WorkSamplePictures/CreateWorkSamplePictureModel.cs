using Microsoft.AspNetCore.Http;
using System;

namespace WebSwIT.ViewModels.WorkSamplePictures
{
    public class CreateWorkSamplePictureModel
    {
        public Guid WorkSampleId { get; set; }
        public IFormFile File { get; set; }
    }
}
