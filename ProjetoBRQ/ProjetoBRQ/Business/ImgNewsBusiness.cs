using ProjetoBRQ.Models;
using ProjetoBRQ.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjetoBRQ.Business
{
    public class ImgNewsBusiness
    {
        public int Add(HttpPostedFileBase file,int idNews)
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
                    DataCadastro = DateTime.Now,
                    DataAlteracao = null,
                    Deletado = false,
                    FileLenght = file.ContentLength,
                    MimeType = file.ContentType,
                    FileName = file.FileName,
                    FileContent = memoryStream.ToArray()
                };

                id = new ImgNewsRepository().Add(imgNews, idNews);
            }
            return id;
        }
    }
}