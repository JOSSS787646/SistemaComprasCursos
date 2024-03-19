using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace sistemadecompras.Controllers
{
    public class ImagenController : Controller
    {
        private FirestoreDb _db;
        private StorageClient _storage;

        public ImagenController()
        {
            string projectId = "storage-536a7";
            string pathToKeyFile = HostingEnvironment.MapPath(@"~/Json/sistemacompras.json");

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", pathToKeyFile);
            _db = FirestoreDb.Create(projectId);
            _storage = StorageClient.Create();
        }

        [HttpGet]
        public ActionResult Subir()
        {
            return View("_Subir");
        }

        [HttpPost]
        public async Task<ActionResult> SubirFoto(HttpPostedFileBase foto, string nuevoNombre)
        {
            if (foto != null && foto.ContentLength > 0)
            {
                CollectionReference collection = _db.Collection("Fotos_Cursos");
                await collection.Document("check").SetAsync(new { });
                DocumentReference document = await collection.AddAsync(new { });
                string documentId = document.Id;
                string fileName = !string.IsNullOrEmpty(nuevoNombre) ? nuevoNombre + Path.GetExtension(foto.FileName) : foto.FileName;
                string filePath = $"{fileName}";
                using (Stream fileStream = foto.InputStream)
                {
                    await _storage.UploadObjectAsync("sistemacompras-4cd72.appspot.com", filePath, null, fileStream);
                }
                string url = $"https://firebasestorage.googleapis.com/u/0/{_db.ProjectId}.appspot.com/o/{HttpUtility.UrlEncode(filePath)}";

                await document.SetAsync(new { Url = url });

                // Devuelve la URL a la vista
                return RedirectToAction("Subir", new { imageUrl = url });
            }
            return RedirectToAction("Subir");
        }
    }
}
