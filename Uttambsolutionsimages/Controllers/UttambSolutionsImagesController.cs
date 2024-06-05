using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uttambsolutionsimages.Models;

namespace Uttambsolutionsimages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UttambSolutionsImagesController : ControllerBase
    {
        [HttpPost("Uploaduttambsolutionsimage")]
        public List<string> UploadImage([FromForm] Uttambsolutions model)
        {
            List<string> uploadedFiles = new List<string>();
            string path = Path.Combine("wwwroot", model.Foldername);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (model.OldFileImagePath==null)
            {
                foreach (IFormFile postedFile in model.Fileimages)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + model.Foldername + "/", fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(new UriBuilder { Scheme = Request.Scheme, Host = Request.Host.Host, Port = Request.Host.Port ?? -1, Path = "/" + model.Foldername + "/" + fileName }.ToString());
                    }
                }
            }
            else
            {
                var uri = new Uri(model.OldFileImagePath);
                var ImagePath = new PathString(uri.PathAndQuery);
                var segments = ImagePath.ToString().Split('/');
                string lastPart = segments.Last();
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + model.Foldername + "/", lastPart)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + model.Foldername + "/", lastPart));
                }
                foreach (IFormFile postedFile in model.Fileimages)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + model.Foldername + "/", fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(new UriBuilder { Scheme = Request.Scheme, Host = Request.Host.Host, Port = Request.Host.Port ?? -1, Path = "/" + model.Foldername + "/" + fileName }.ToString());
                    }
                }
            }
            return uploadedFiles;
        }

        [HttpPost("Deleteuttambsolutionsimage")]
        public string DeleteImage([FromForm] Uttambsolutions model)
        {
            if (model.OldFileImagePaths.Count()>0)
            {
                foreach (var item in model.OldFileImagePaths)
                {
                    var uri = new Uri(item);
                    var ImagePath = new PathString(uri.PathAndQuery);
                    var segments = ImagePath.ToString().Split('/');
                    string lastPart = segments.Last();
                    if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + model.Foldername + "/", lastPart)))
                    {
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + model.Foldername + "/", lastPart));
                    }
                }
            }
            if (model.OldFileImagePath != null)
            {
                var uri = new Uri(model.OldFileImagePath);
                var ImagePath = new PathString(uri.PathAndQuery);
                var segments = ImagePath.ToString().Split('/');
                string lastPart = segments.Last();
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + model.Foldername + "/", lastPart)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + model.Foldername + "/", lastPart));
                }
            }
            return "All is Well";
        }
    }
}
