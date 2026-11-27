
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaInventario
{
    class Program
    {
        static List<Producto> productos = new List<Producto>();
        static int siguienteId = 1;

        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("    SISTEMA DE INVENTARIO");
                Console.WriteLine("================================");
                Console.WriteLine("1. Registrar producto");
                Console.WriteLine("2. Mostrar productos");
                Console.WriteLine("3. Editar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Buscar producto");
                Console.WriteLine("0. Salir");
                Console.WriteLine("================================");

                Console.Write("Seleccione una opcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Ingrese una opcion valida.");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        RegistrarProducto();
                        break;

                    case 2:
                        MostrarProductos();
                        break;

                    case 3:
                        EditarProducto();
                        break;

                    case 4:
                        EliminarProducto();
                        break;

                    case 5:
                        BuscarProducto();
                        break;

                    case 0:
                        Console.WriteLine("Gracias por utilizar el sistema.");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

        // CREATE: Registrar producto
        static void RegistrarProducto()
        {
            Console.Clear();
            Console.WriteLine("REGISTRAR PRODUCTO");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre es obligatorio.");
                return;
            }

            if (productos.Any(p => p.Nombre.Equals(
                nombre.Trim(),
                StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Ya existe un producto con ese nombre.");
                return;
            }


            Console.Write("Precio: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal precio)
                || precio <= 0)
            {
                Console.WriteLine("El precio debe ser mayor que cero.");
                return;
            }

            Console.Write("Cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad)
                || cantidad < 0)
            {
                Console.WriteLine("La cantidad no puede ser negativa.");
                return;
            }

            Producto nuevoProducto = new Producto
            {
                Id = siguienteId++,
                Nombre = nombre.Trim(),
                Precio = precio,
                Cantidad = cantidad
            };

            productos.Add(nuevoProducto);

            Console.WriteLine("\nProducto registrado correctamente.");
        }

        // READ: Mostrar productos
        static void MostrarProductos()
{
    Console.Clear();
    Console.WriteLine("==================================");
    Console.WriteLine("       LISTA DE PRODUCTOS");
    Console.WriteLine("==================================");

    if (productos.Count == 0)
    {
        Console.WriteLine("No hay productos registrados.");
    }
    else
    {
        Console.WriteLine(
            "{0,-8} {1,-20} {2,12} {3,12}",
            "ID", "NOMBRE", "PRECIO", "CANTIDAD");

        Console.WriteLine(
            "--------------------------------------------------------");

        foreach (Producto producto in productos)
        {
            Console.WriteLine(
                "{0,-8} {1,-20} {2,12:C} {3,12}",
                producto.Id,
                producto.Nombre,
                producto.Precio,
                producto.Cantidad);
        }

        Console.WriteLine(
            "--------------------------------------------------------");

        Console.WriteLine($"Total de productos: {productos.Count}");
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}
        static void EditarProducto()
        {
            Console.Clear();
            Console.WriteLine("EDITAR PRODUCTO");

            Console.Write("Ingrese el ID del producto: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID no valido.");
                return;
            }

            Producto producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            Console.Write("Nuevo nombre: ");
            string nombre = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre es obligatorio.");
                return;
            }

            Console.Write("Nuevo precio: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal precio)
                || precio <= 0)
            {
                Console.WriteLine("Precio no valido.");
                return;
            }

            Console.Write("Nueva cantidad: ");

            if (!int.TryParse(Console.ReadLine(), out int cantidad)
                || cantidad < 0)
            {
                Console.WriteLine("Cantidad no valida.");
                return;
            }

            


        producto.Nombre = nombre.Trim();
            producto.Precio = precio;
            producto.Cantidad = cantidad;

            Console.WriteLine("Producto actualizado correctamente.");
        }

        // DELETE: Eliminar producto

        // DELETE: Eliminar producto
        static void EliminarProducto()
        {
            Console.Clear();
            Console.WriteLine("ELIMINAR PRODUCTO");

            Console.Write("Ingrese el ID del producto: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID no válido.");
                return;
            }

            Producto producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            Console.Write($"¿Está seguro de eliminar el producto {producto.Nombre}? (S/N): ");
            string respuesta = Console.ReadLine() ?? "";

            if (respuesta.ToUpper() != "S")
            {
                Console.WriteLine("Eliminación cancelada.");
                return;
            }

            productos.Remove(producto);

            Console.WriteLine("Producto eliminado correctamente.");
        }


        // SEARCH: Buscar producto

        // SEARCH: Buscar producto
        static void BuscarProducto()
        {
            Console.Clear();
            Console.WriteLine("BUSCAR PRODUCTO");

            Console.Write("Ingrese el nombre o ID del producto: ");
            string nombre = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Debe escribir un nombre o ID.");
                return;
            }

            List<Producto> encontrados = productos
                .Where(p => p.Nombre.Contains(
                    nombre.Trim(),
                    StringComparison.OrdinalIgnoreCase)
                    || p.Id.ToString() == nombre.Trim())
                .ToList();

            if (encontrados.Count == 0)
            {
                Console.WriteLine("No se encontraron productos.");
                return;
            }

            foreach (Producto producto in encontrados)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"ID: {producto.Id}");
                Console.WriteLine($"Nombre: {producto.Nombre}");
                Console.WriteLine($"Precio: {producto.Precio:C}");
                Console.WriteLine($"Cantidad: {producto.Cantidad}");
            }

            Console.WriteLine($"Productos encontrados: {encontrados.Count}");
        }

    }

    class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
