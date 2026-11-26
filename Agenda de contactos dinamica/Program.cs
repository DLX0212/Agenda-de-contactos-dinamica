using System;

namespace AgendaListaEnlazada
{
    
    // 1. CLASE CONTACTO: Modelo de datos de cada registro
    class Contacto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        public Contacto(string nombre, string telefono, string correo)
        {
            Nombre = nombre;
            Telefono = telefono;
            Correo = correo;
        }

        // Formato limpio para mostrar los datos del contacto en consola
        public override string ToString()
        {
            return $"Nombre: {Nombre} | Tel: {Telefono} | Correo: {Correo}";
        }
    }

    // 2. CLASE NODO: Unidad básica de la lista simplemente enlazada
    class Nodo
    {
        public Contacto Dato { get; set; }
        public Nodo Siguiente { get; set; }

        public Nodo(Contacto dato)
        {
            Dato = dato;
            Siguiente = null; // Al crearse, no apunta a ningún otro nodo
        }
    }

    // 3. CLASE LISTA ENLAZADA AGENDA: Gestión de nodos y punteros
    class ListaEnlazadaAgenda
    {
        // Referencia al primer nodo de la lista
        private Nodo cabeza;

        public ListaEnlazadaAgenda()
        {
            cabeza = null;
        }

  
        /// Agrega un nuevo contacto al final de la lista enlazada.
        /// Complejidad: O(1) si está vacía, O(n) en caso general.

        public void AgregarAlFinal(Contacto nuevoContacto)
        {
            Nodo nuevoNodo = new Nodo(nuevoContacto);

            // Caso 1: La lista está vacía; el nuevo nodo es la cabeza
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                return;
            }

            // Caso 2: Recorrer hasta el último nodo (cuyo Siguiente es null)
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }

            // Enlazar el último nodo con el nuevo nodo
            actual.Siguiente = nuevoNodo;
        }


        /// Elimina el primer contacto cuyo nombre coincida (sin distinguir mayúsculas).
        /// Complejidad: O(1) si está en la cabeza, O(n) en el peor caso.

        public bool EliminarPorNombre(string nombre)
        {
            // Caso 1: Lista vacía
            if (cabeza == null)
            {
                return false;
            }

            // Caso 2: El contacto a eliminar está en la cabeza
            if (string.Equals(cabeza.Dato.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
            {
                cabeza = cabeza.Siguiente; // La cabeza pasa a ser el segundo nodo
                return true;
            }

            // Caso 3: Buscar en el resto de la lista manteniendo referencia al nodo anterior
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                if (string.Equals(actual.Siguiente.Dato.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    // Se "salta" el nodo eliminado enlazando con el siguiente del siguiente
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    return true;
                }
                actual = actual.Siguiente;
            }

            // No se encontró ningún contacto con ese nombre
            return false;
        }

        /// Recorre la lista desde la cabeza hasta null e imprime todos los contactos.
        /// Complejidad: O(n).
        public void MostrarContactos()
        {
            if (cabeza == null)
            {
                Console.WriteLine("\nLa agenda está vacía.");
                return;
            }

            Console.WriteLine("\n--- Lista de Contactos ---");
            Nodo actual = cabeza;
            int indice = 1;

            while (actual != null)
            {
                Console.WriteLine($" {indice}. {actual.Dato}");
                actual = actual.Siguiente;
                indice++;
            }
        }
    }

    // PROGRAMA PRINCIPAL (MENÚ DE CONSOLA)
    class Program
    {
        static void Main(string[] args)
        {
            ListaEnlazadaAgenda agenda = new ListaEnlazadaAgenda();
            int opcion;

            do
            {
                Console.WriteLine("\n====== AGENDA DE CONTACTOS (LISTA ENLAZADA) ======");
                Console.WriteLine("1. Agregar nuevo contacto al final");
                Console.WriteLine("2. Eliminar contacto por nombre");
                Console.WriteLine("3. Mostrar todos los contactos");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) opcion = -1;

                switch (opcion)
                {
                    case 1:
                        Console.Write("\nNombre: ");
                        string nombre = Console.ReadLine()?.Trim();
                        Console.Write("Teléfono: ");
                        string telefono = Console.ReadLine()?.Trim();
                        Console.Write("Correo electrónico: ");
                        string correo = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(nombre))
                        {
                            Console.WriteLine("Error: El nombre es obligatorio.");
                            break;
                        }

                        agenda.AgregarAlFinal(new Contacto(nombre, telefono, correo));
                        Console.WriteLine($"Contacto \"{nombre}\" agregado al final de la agenda.");
                        break;

                    case 2:
                        Console.Write("\nIngrese el nombre del contacto a eliminar: ");
                        string nombreEliminar = Console.ReadLine()?.Trim();

                        if (agenda.EliminarPorNombre(nombreEliminar))
                        {
                            Console.WriteLine($"Contacto \"{nombreEliminar}\" eliminado correctamente.");
                        }
                        else
                        {
                            Console.WriteLine($"No se encontró ningún contacto con el nombre \"{nombreEliminar}\".");
                        }
                        break;

                    case 3:
                        agenda.MostrarContactos();
                        break;

                    case 0:
                        Console.WriteLine("Cerrando agenda...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            } while (opcion != 0);
        }
    }
}