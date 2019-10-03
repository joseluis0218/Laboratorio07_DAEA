using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana07Linq
{
    class Program
    {
        public static DataClasses2DataContext context = new DataClasses2DataContext();


        static void Main(string[] args)
        {
            var Pedidos = context.Pedidos.ToList();

            var Clientes = context.clientes.ToList();

            var Empleados = context.Empleados.ToList();

            var Proveedores = context.proveedores.ToList();

            var DetallePedidos = context.detallesdepedidos.ToList();



            CiudadesPersonas(Clientes, Empleados, Proveedores);

            //EmpleadosGanenMas1000(Pedidos,Empleados);
            Console.Read();
        }
        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = from num in numbers
                           where (num % 2) == 0
                           select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }

        static void DataSource()
        {
            var queryAllCustomer = from cust in context.clientes
                                   select cust;

            foreach (var item in queryAllCustomer)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Filtering()
        {
            var queryLondonCustomer = from cust in context.clientes
                                      where cust.Ciudad == "Londres"
                                      select cust;

            foreach (var item in queryLondonCustomer)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers3 = from cust in context.clientes
                                        where cust.Ciudad == "Londres"
                                        orderby cust.NombreCompañia ascending
                                        select cust;
            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("{0}",customer.NombreCompañia);

                }
            }
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);

            }
        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistribuitorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);

            }
        }


       //Funciones Lambda

        static void IntroToLINQLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = numbers.Where(x => (x % 2) == 0).ToList();

            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }

        static void DataSourceLambda()
        {
            var queryAllCustomer = context.clientes.Select(x => x);

            foreach (var item in queryAllCustomer)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void FilteringLambda()
        {
            var queryLondonCustomer = context.clientes.Where(x => x.Ciudad == "Londres");

            foreach (var item in queryLondonCustomer)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void OrderingLambda()
        {
            var queryLondonCustomers3 = context.clientes.Where(x => x.Ciudad == "Londres").OrderBy(x => x.NombreCompañia);
            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(x => x.Ciudad);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("{0}", customer.NombreCompañia);

                }
            }
        }
        static void Grouping2Lambda()
        {
            var custQuery = context.clientes.GroupBy(cust => cust.Ciudad).
                Where(custGroup => custGroup.Count() > 2).OrderBy(custGroup => custGroup.Key);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);

            }
        }

        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes.Join(context.Pedidos, cust => cust.idCliente, dist => dist.IdCliente,
                                                        (cust, dist) =>
                                                        new { CustomerName = cust.NombreCompañia, DistribuitorName = dist.PaisDestinatario }
                                                        );

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);

            }
        }


        //Ejercicios extras 


        static void PedidosUltimos5Años(List<Pedidos> pedidos)
        {
            var queryOrders = pedidos.Where(ord => ord.FechaPedido >= DateTime.Now.AddYears(-24));

            foreach (var item in queryOrders)
            {
                Console.WriteLine(item.FechaPedido);

            }
        }
            
        static void ClientesMas2Pedidos(List<clientes> clientes)
        {
            var queryCustomers = clientes.Where(cust => cust.Pedidos.Count() > 2);

            foreach (var item in queryCustomers)
            {
                Console.WriteLine(item.NombreCompañia);

            }
        }
        static void ImporteMayor200(List<Pedidos> pedidos)
        {
            var queryOrders = pedidos.Join(context.detallesdepedidos, order => order.IdPedido, detail => detail.idpedido,
                                                   (order, detail) =>
                                                   new { DistribuitorName = order.PaisDestinatario, PrecioUnidad = detail.preciounidad, Cantidad = detail.cantidad }
                                                ).Where(orderDetail => orderDetail.PrecioUnidad * orderDetail.Cantidad > 200);

            foreach (var item in queryOrders)
            {
                Console.WriteLine(item.DistribuitorName);

            }
        }

        static void ProveedoresMas2Productos(List<proveedores> proveedores)
        {
            var queryProviders = proveedores.Where(prov => prov.productos.Count() > 2);
            foreach (var item in queryProviders)
            {
                Console.WriteLine(item.nombreCompañia);

            }

        }

        static void ProductoMas3Pedidos(List<detallesdepedidos> detallesdepedidos)
        {
            var queryDetailOrders= detallesdepedidos.GroupBy(dp => dp.idproducto).Select(group => new { IdProducto = group.Key, CantidadPedidos = group.Count()})
                                                  .Where(group => group.CantidadPedidos > 3);

            foreach (var item in queryDetailOrders)
            {
                Console.WriteLine(item.IdProducto.ToString() +' '+ item.CantidadPedidos.ToString());

            }
        }

        static void EmpleadosCodPostal(List<Empleados> empleados,List<clientes> clientes)
        {
            var queryEmpleados = empleados.Join(clientes, emp => emp.codPostal, cli => cli.CodPostal,
                                   (emp,cli) =>
                                   new {EmpCodPostal = emp.codPostal, CliCodPostal = cli.CodPostal }
                                );
            foreach (var item in queryEmpleados)
            {
                Console.WriteLine(item.EmpCodPostal , item.CliCodPostal);

            }
        }

        static void CiudadesPersonas(List<clientes> clientes,List<Empleados> empleados,List<proveedores> proveedores)
        {

            var queryCiudades = clientes.Join(empleados, cli => cli.Ciudad, emp => emp.ciudad,
                                                    (cli, emp) => new { CiudadCli = cli.Ciudad,NombreCli = cli.NombreCompañia, NombreEmp = emp.Apellidos,CiudadEmp = emp.ciudad }).
                                                    Join(proveedores, per => per.CiudadCli,
                                                    prov => prov.ciudad, (per, prov) => new { Ciudad = prov.ciudad, NombreProv = prov.nombreCompañia }).GroupBy(res => res.Ciudad);

            foreach (var item in queryCiudades)
            {
                Console.WriteLine(item.Key);

            }

        }

        static void EmpleadosGanenMas1000(List<Pedidos> pedidos,List<Empleados> empleados)

        {


            var queryEmpleados = context.compañiasdeenvios.Join(pedidos, comp => comp.idCompañiaEnvios, ord => ord.FormaEnvio,
                                                                (comp, ord) => new { IdComp = comp.idCompañiaEnvios, NombreComp = comp.nombreCompañia, IdEmpleado = ord.IdEmpleado })
                                                                .Join(empleados, ord => ord.IdEmpleado, emp => emp.IdEmpleado,
                                                                (ord, emp) => new { IdCompañia = ord.IdComp, NombreCompañia = ord.NombreComp, SueldoBasico = emp.sueldoBasico })
                                                                .GroupBy(res => new { Id = res.IdCompañia, Compañia = res.NombreCompañia });

            foreach (var item in queryEmpleados)
            {
                Console.WriteLine(item.Key);

            }
        }
    }
}
