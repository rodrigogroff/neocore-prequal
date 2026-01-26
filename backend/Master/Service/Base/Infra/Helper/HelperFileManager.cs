using Microsoft.AspNetCore.Http;
using System.IO;

namespace Master.Service.Base.Infra.Helper
{
    public class HelperFileManager
    {
        public string currentFileOrFolder { get; set; }

        public void AddFileOrFolder(string dir)
        {
#if RELEASE
            currentFileOrFolder += "/" + dir;
#else
            currentFileOrFolder += "\\" + dir;
#endif
        }

        public void CreateDirIfNotExists()
        {
            if (!Directory.Exists(currentFileOrFolder))
            {
                Directory.CreateDirectory(currentFileOrFolder);
            }
        }

        public string BuildFilePath(string filesDir, string mType, long fkCompany, long id)
        {
            currentFileOrFolder = filesDir;

            AddFileOrFolder(fkCompany.ToString());
            CreateDirIfNotExists();
            AddFileOrFolder(mType);
            CreateDirIfNotExists();
            AddFileOrFolder(id.ToString());
            CreateDirIfNotExists();

            return currentFileOrFolder;
        }

        public bool SaveFile(string filesDir, string tag_image, long fkCompany, long id, IFormFile postedFile)
        {
            BuildFilePath(filesDir, tag_image, fkCompany, id);
            AddFileOrFolder(postedFile.FileName);

            using (Stream fileStream = new FileStream(currentFileOrFolder, FileMode.Create))
            {
                postedFile.CopyTo(fileStream);
            }

            return true;
        }
    }
}
