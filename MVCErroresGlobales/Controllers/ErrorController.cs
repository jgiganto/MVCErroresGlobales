using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
 

namespace MVCErroresGlobales.Controllers
{
    public class ErrorController : Controller
    {
        //Este controlador solo sera para la parte grafica , 
        //donde llevaremos las peticiones en caso de error
        // GET: Error
        public ActionResult PaginaNoEncontrada()
        {
            //Cuando tenemos Errores HTTP (httpexception)
            //debemos indicar la respuesta del estado al servidor
            //la clase HttpStatusCode contiene los codigos de error para las respuestas al servidor
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            ViewBag.Mensaje = "Pagina no encontrada en el servidor";
            return View();
        }
        public ActionResult ErrorGeneral()
        {
            ViewBag.Mensaje = "Error general en la app";
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}