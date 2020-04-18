﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Extrados.Controllers
{
    public class ProductoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tabla()
        {
            return View();
        }

        public JsonResult ListarProductos()
        {
            Models.dbExtradosDataContext db = new Models.dbExtradosDataContext();

            var lista = (from _producto in db.Productos
                         join _marca in db.Marcas
                         on _producto.IdMarca equals _marca.Id
                         select new
                         {
                             CODIGO = _producto.Codigo,
                             NOMBRE = _producto.Nombre,
                             MARCA = _marca.Nombre,
                             PRECIO_UNITARIO = _producto.PrecioUnitario,
                             ESTADO = _producto.Activo

                         }).ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListarMarcas()
        {
            Models.dbExtradosDataContext db = new Models.dbExtradosDataContext();

            var lista = (from _marca in db.Marcas
                         select new
                         {
                             ID = _marca.Id,
                             DESCRIPCION = _marca.Nombre
                         }).ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);

        }




        //////////////////////////////////////////////////////////////////////////////////////////
        ///

        /*

    public JsonResult DatosComboBox()
    {
        var lista = db.Rubros.Select(columna => new { ID = columna.IdRubro, DESCRIPCION = columna.Descripcion }).ToList();


        return Json(lista, JsonRequestBehavior.AllowGet);
    }

    public JsonResult DatosTabla()
    {


        var lista = db.Productos.Where(parametro => parametro.Activo.Equals(true))
            .Select(columna => new
            {
                columna.IdProducto,
                columna.Nombre,
                columna.Descripcion,
                columna.Precio,
                columna.Cantidad,
                columna.FechaVencimiento,
                columna.Categoria,
                columna.Activo



            }).ToList();

        return Json(lista, JsonRequestBehavior.AllowGet);
    }

    public ActionResult Tabla()
    {
        return View();
    }

    public JsonResult RegistroSeleccionado(int id)
    {


        var lista = db.Productos.Where(parametro => parametro.Activo.Equals(true)
        && parametro.IdProducto.Equals(id))
            .Select(columna => new
            {
                columna.IdProducto,
                columna.Nombre,
                columna.Descripcion,
                columna.Precio,
                columna.Cantidad,
                columna.FechaVencimiento,
                columna.IdRubro,
                FOTOMOSTRAR = Convert.ToBase64String(columna.Foto.ToArray()),
                columna.Categoria




            }).FirstOrDefault();

        return Json(lista, JsonRequestBehavior.AllowGet);
    }

    public int AgregarEditar(Models.Producto obj)
    {

        int nroRegistrosAfectados = 0;

        try
        {
            //si el ID es cero agregar objeto
            if (obj.IdProducto == 0)
            {

                db.Productos.InsertOnSubmit(obj);
                db.SubmitChanges();

                nroRegistrosAfectados = 1;


            }//si el ID es distinto de cero editar entidad
            else
            {

                Models.Producto objUpdate = db.Productos
                    .Where(parametro => parametro.IdProducto.Equals(obj.IdProducto)).First();

                objUpdate.Nombre = obj.Nombre;
                objUpdate.Descripcion = obj.Descripcion;
                objUpdate.Foto = obj.Foto;
                objUpdate.Precio = obj.Precio;
                objUpdate.Cantidad = obj.Cantidad;
                // objUpdate.FechaAlta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                objUpdate.FechaVencimiento = obj.FechaVencimiento;
                objUpdate.IdRubro = obj.IdRubro;
                objUpdate.Categoria = obj.Categoria;

                db.SubmitChanges();
                nroRegistrosAfectados = 1;
            }
        }
        catch (Exception ex)
        {
            nroRegistrosAfectados = 0;
            throw;
        }

        return nroRegistrosAfectados;
    }

    public int AgregarEditarConFoto(Models.Producto obj, string cadenaFoto)
    {

        int nroRegistrosAfectados = 0;

        try
        {
            //si el ID es cero agregar objeto
            if (obj.IdProducto == 0)
            {
                obj.Foto = Convert.FromBase64String(cadenaFoto);
                db.Productos.InsertOnSubmit(obj);
                db.SubmitChanges();

                nroRegistrosAfectados = 1;


            }//si el ID es distinto de cero editar entidad
            else
            {

                Models.Producto objUpdate = db.Productos
                    .Where(parametro => parametro.IdProducto.Equals(obj.IdProducto)).First();

                objUpdate.Nombre = obj.Nombre;
                objUpdate.Descripcion = obj.Descripcion;
                objUpdate.Foto = Convert.FromBase64String(cadenaFoto);
                objUpdate.Precio = obj.Precio;
                objUpdate.Cantidad = obj.Cantidad;
                // objUpdate.FechaAlta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                objUpdate.FechaVencimiento = obj.FechaVencimiento;
                objUpdate.IdRubro = obj.IdRubro;
                objUpdate.Categoria = obj.Categoria;

                db.SubmitChanges();
                nroRegistrosAfectados = 1;
            }
        }
        catch (Exception ex)
        {
            nroRegistrosAfectados = 0;
            throw;
        }

        return nroRegistrosAfectados;
    }



    public int Inactivar(Models.Producto obj)
    {

        int nroRegistrosAfectados = 0;

        try
        {
            //si el ID es cero agregar objeto


            Models.Producto objUpdate = db.Productos
                .Where(parametro => parametro.IdProducto.Equals(obj.IdProducto)).First();

            objUpdate.Activo = false;


            db.SubmitChanges();
            nroRegistrosAfectados = 1;

        }
        catch (Exception ex)
        {
            nroRegistrosAfectados = 0;
            throw;
        }

        return nroRegistrosAfectados;
    }


    public int Eliminar(Models.Producto obj)
    {

        int nroRegistrosAfectados = 0;

        try
        {

            var objEliminar = db.Productos.Where(parametro => parametro.IdProducto.Equals(obj.IdProducto)).First();

            db.Productos.DeleteOnSubmit(objEliminar);
            db.SubmitChanges();

            nroRegistrosAfectados = 1;
        }
        catch (Exception ex)
        {

            nroRegistrosAfectados = 0;
        }


        return nroRegistrosAfectados;
    }


    public int EliminarGet(int id)
    {

        int nroRegistrosAfectados = 0;

        try
        {

            var objEliminar = db.Productos.Where(parametro => parametro.IdProducto.Equals(id)).First();

            db.Productos.DeleteOnSubmit(objEliminar);
            db.SubmitChanges();

            nroRegistrosAfectados = 1;
        }
        catch (Exception ex)
        {

            nroRegistrosAfectados = 0;
        }


        return nroRegistrosAfectados;
    }

    public int InactivarGet(int id)
    {

        int nroRegistrosAfectados = 0;

        try
        {

            Models.Producto objUpdate = db.Productos
                .Where(parametro => parametro.IdProducto.Equals(id)).First();

            objUpdate.Activo = false;


            db.SubmitChanges();
            nroRegistrosAfectados = 1;

        }
        catch (Exception ex)
        {
            nroRegistrosAfectados = 0;
            throw;
        }

        return nroRegistrosAfectados;
    }



    public JsonResult ProductoClasificado()
    {

        var lista = (from prod in db.Productos
                     join rub in db.Rubros
                     on prod.IdRubro equals rub.IdRubro
                     where prod.Activo.Equals(true)
                     select new
                     {

                         PRODUCTO = prod.Nombre,
                         prod.Precio,
                         RUBRO = rub.Descripcion

                     }).ToList();

        return Json(lista, JsonRequestBehavior.AllowGet);

    }









    public JsonResult Editar(int id)
    {

        var lista = (from prod in db.Productos
                     where prod.IdProducto.Equals(id)
                     select new
                     {
                         prod.IdProducto,
                         prod.Nombre,
                         prod.Descripcion,
                         prod.Foto,
                         prod.Precio,
                         prod.Cantidad,
                         prod.FechaAlta,
                         prod.FechaVencimiento,
                         prod.IdRubro,
                         prod.Categoria,
                         prod.Activo

                     }).First();

        return Json(lista, JsonRequestBehavior.AllowGet);
    }

    */
    }
}