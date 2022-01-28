using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCoreEmpleadosSession.Extensions;
using MvcCoreEmpleadosSession.Models;
using MvcCoreEmpleadosSession.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaEmpleados()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult SessionSalarios(int? salario)
        {
            if(salario != null)
            {
                int sumasalarial = 0;
                if (HttpContext.Session.GetString("SUMASALARIAL")!= null)
                {
                    sumasalarial = int.Parse(HttpContext.Session.GetString("SUMASALARIAL"));
                }
                sumasalarial += salario.Value;
                HttpContext.Session.SetString("SUMASALARIAL", sumasalarial.ToString());
                ViewData["MENSAJE"] = "Salario Almacenado: " + salario.Value;
            }
            return View(this.repo.GetEmpleados());
        }
        public IActionResult SumarSalarios()
        {
            return View();
        }

        public IActionResult SessionEmpleados(int? idEmpleado)
        {
            if(idEmpleado != null)
            {
                Empleado emp = this.repo.FindEmpleado(idEmpleado.Value);
                List<Empleado> empleadossession;
                if(HttpContext.Session.GetObject<List<Empleado>>("EMPLEADOS") != null)
                {
                    empleadossession = HttpContext.Session.GetObject<List<Empleado>>("EMPLEADOS");
                }
                else
                {
                    empleadossession = new List<Empleado>();
                }
                empleadossession.Add(emp);
                HttpContext.Session.SetObject("EMPLEADOS", empleadossession);
                ViewData["MENSAJE"] = "Empleado " +
                    emp.EmpNo + ", " + emp.Apellido + " almacenado en Session";
            }
            return View(this.repo.GetEmpleados());
        }

        public IActionResult EmpleadosAlmacenados()
        {
            return View();
        }


        public IActionResult SessionEmpleadosCorrecto(int? idEmpleado)
        {
            if (idEmpleado != null)
            {
                List<int> listaids;
                if (HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS") == null)
                {
                    listaids = new List<int>();
                }
                else
                {
                    listaids = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
                }
                listaids.Add(idEmpleado.Value);
                HttpContext.Session.SetObject("IDSEMPLEADOS", listaids);
                ViewData["MENSAJE"] = "Empleados Almacenados: " + listaids.Count();
            }
            return View(this.repo.GetEmpleados());
        }

        public IActionResult EmpleadosAlmacenadosCorrecto()
        {
            if (HttpContext.Session.GetString("IDSEMPLEADOS") == null)
            {
                ViewData["MENSAJE"] = "No Existen empleados almacenados";
                return View();
            }
            else
            {
                List<int> listIdEmpleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
                List<Empleado> empleados = this.repo.GetEmpleadosSession(listIdEmpleados);
                return View(empleados);
            }
        }
    }
}
