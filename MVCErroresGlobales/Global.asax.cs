using MVCErroresGlobales.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCErroresGlobales
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //EN ESTE METODO SE CAPTURAN LAS PETICIONES DE ERROR
        //INCONTROLABLES POR FILTER O POR EXCEPTION
        protected void Application_Error()
        {
            //debemos crear la excepcion que esta llamando al metodo actualmente
            Exception ex = Server.GetLastError();
            //estamos en entorno http las excepciones que queremos capturar tienen codigos http
            //debemos convertir nuestro exception general a un tipo de excepcion http
            HttpException httpexception = ex as HttpException;
            //el objeto httpexception contiene los codigos de error http
            //almacenamos la accion donde deseamos enviar dependiendo de los errores http
            String accion = "";
            switch (httpexception.ErrorCode)
            {
                case 404:
                    accion = "PaginaNoEncontrada";
                    break;
                case 403: //forbidden
                    accion = "ErrorGeneral";
                    break;
                default:
                    accion = "ErrorGeneral";
                    break;

            }
            //al capturar las excepciones globales debemos limpiar el contexto del error
            Context.ClearError();
            //redireccionamos pero no lo vamos a hacer con routing
            //vamos a enviar la peticion request directamente al controlador error
            //necesitamos ROUTEdata para indicar donde vamos a ir
            RouteData rutaerror = new RouteData();
            //añadimos a la ruta el controlador y el action
            rutaerror.Values.Add("controller", "Error");
            rutaerror.Values.Add("action", accion);
            //para poder ejecutar la peticion a un controlador 
            //con una ruta, necesitamos la clase icontroller que permite ejecutar otra peticion
            IController controlador = new ErrorController();
            //mediante el metodo execute, redirigimos el request, enviando el contexto(Context) y RouteData
            controlador.Execute(
                new RequestContext(new HttpContextWrapper(Context), rutaerror));


        }
    }
}
