using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using ProjetoBRQ.Repository;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjetoBRQ.Business
{
    public class ImgNewsBusiness : IDisposable
    {
        public async Task<int> UpdateAsync(HttpPostedFileBase File, int Id)
        {
            int error = -1;
            using (Stream inputStream = File.InputStream)
            {
                var memoryStream = inputStream as MemoryStream;
                if(memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }

                using(var db = new DbBRQ())
                {
                    var imgId = db.ImgNews.Where(x => x.Id == Id).FirstOrDefault();

                    if(imgId == null)
                    {
                        return error;
                    }
                }

                var imgNews = new ImgNews()
                {
                    Id = Id,
                    //DataCadastro = DateTime.Now,
                    //DataAlteracao = null,
                    //Deletado = false,
                    FileLenght = File.ContentLength,
                    MimeType = File.ContentType,
                    FileName = File.FileName,
                    FileContent = memoryStream.ToArray()
                };

                await new ImgNewsRepository().UpdateAsync(imgNews);
            }
            
            return 0;
        }

        public async Task<int> AddAsync(HttpPostedFileBase file,int idNews)
        {
            int id = -1;

            using (Stream inputStream = file.InputStream)
            {
                var memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }

                var imgNews = new ImgNews()
                {
                    //DataCadastro = DateTime.Now,
                    //DataAlteracao = null,
                    //Deletado = false,
                    FileLenght = file.ContentLength,
                    MimeType = file.ContentType,
                    FileName = file.FileName,
                    FileContent = memoryStream.ToArray()
                };

                id = await new ImgNewsRepository().AddAsync(imgNews, idNews);
            }
            return id;
        }

        public void Dispose()
        {
            
        }
    }
}