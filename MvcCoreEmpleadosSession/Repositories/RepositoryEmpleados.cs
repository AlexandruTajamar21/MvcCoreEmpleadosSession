using MvcCoreEmpleadosSession.Data;
using MvcCoreEmpleadosSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Repositories
{
    public class RepositoryEmpleados
    {
        EmpleadosContext context;
        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            List<Empleado> empleados = this.context.Empleados.ToList();
            return empleados;
        }

        public Empleado FindEmpleado(int idEmpleado)
        {
            return this.context.Empleados.SingleOrDefault(x => x.EmpNo == idEmpleado);
        }

        public List<Empleado> GetEmpleadosSession(List<int> idsEmpleados)
        {
            var consulta = from datos in this.context.Empleados
                           where idsEmpleados.Contains(datos.EmpNo)
                           select datos;
            return consulta.ToList();
        }
    }
}
