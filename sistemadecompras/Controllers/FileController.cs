using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Mvc;

using Firebase.Storage;

namespace sistemadecompras.Controllers
{
    //public class FileController : Controller
    //{
    //    public ActionResult Index()
    //    {
    //        // Recuperar la lista de nombres de imágenes subidas desde la sesión del usuario
    //        List<string> imagenesSubidas = Session["ImagenesSubidas"] as List<string>;

    //        // Pasar la lista de imágenes a la vista
    //        return View(imagenesSubidas);
    //    }

    //    public ActionResult Crear()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //   // public async Task<ActionResult> Crear(FileDM oImage, HttpPostedFileBase Imagen)
    //    {
    //        // Guardar la imagen en Firebase Storage
    //        Stream image = Imagen.InputStream;
    //        string urlimagen = await SubirStorage(image, Imagen.FileName);

    //        // Recuperar la lista de nombres de imágenes subidas de la sesión del usuario
    //        List<string> imagenesSubidas = Session["ImagenesSubidas"] as List<string>;

    //        // Si la lista aún no existe, crea una nueva lista
    //        if (imagenesSubidas == null)
    //        {
    //            imagenesSubidas = new List<string>();
    //        }

    //        // Agregar el nombre de la imagen recién subida a la lista
    //        imagenesSubidas.Add(Imagen.FileName);

    //        // Guardar la lista actualizada en la sesión del usuario
    //        Session["ImagenesSubidas"] = imagenesSubidas;

    //        // Redireccionar a la página Index para mostrar la lista de imágenes subidas
    //        return RedirectToAction("Index");
    //    }


    //    public async Task<string> SubirStorage(Stream archivo, string nombre)
    //    {
    //        // Mantenido tus credenciales, asegúrate de que estén actualizadas
    //        string email = "22300257@uttt.edu.mx";
    //        string clave = "andrik123";
    //        string ruta = "storage-536a7.appspot.com";
    //        string api_key = "AIzaSyCWTwV48Vl7v3q3-Rvd6lh4duooQthJTRY";

    //        // Utiliza el método SignInWithEmailAndPasswordAsync directamente
    //       // var authProvider = new FirebaseAuthProvider(new FirebaseConfig(api_key));
    //        //var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, clave);

    //        var cancellation = new CancellationTokenSource();

    //        // Utiliza la clase FirebaseStorage directamente
    //        var storage = new FirebaseStorage(ruta, new FirebaseStorageOptions
    //        {
    //            AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
    //            ThrowOnCancel = true // Puedes ajustar esto según tus necesidades
    //        });

    //        // Cambia el formato de la ruta
    //        var task = storage
    //            .Child("Fotos_Cursos")
    //            .Child(nombre)
    //            .PutAsync(archivo, cancellation.Token);

    //        var downloadURL = await task;

    //        return downloadURL;
    //    }

    //}
}
