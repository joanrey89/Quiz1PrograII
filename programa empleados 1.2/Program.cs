using System;

class Program
{
    // Tamaño máximo de arreglos
    const int TAM = 20;

    // Arreglos para almacenar datos de empleados
    static string[] nombres = new string[TAM];
    static uint[] ids = new uint[TAM];
    static sbyte[] tipos = new sbyte[TAM];
    static sbyte[] horasTrabajadas = new sbyte[TAM];
    static uint[] preciosHora = new uint[TAM];
    static uint[] salariosOrdinarios = new uint[TAM];
    static uint[] aumentos = new uint[TAM];
    static uint[] salariosBrutos = new uint[TAM];
    static uint[] deducciones = new uint[TAM];
    static uint[] salariosNetos = new uint[TAM];

    // Contador de empleados registrados
    static int contadorEmpleados = 0;

    // Variables globales para estadísticas
    static byte Ope = 0, Tec = 0, Pro = 0;
    static uint TotalOpe = 0, TotalTec = 0, TotalPro = 0;

    static void Main()
    {
        bool salir = false;

        Console.WriteLine("=== SISTEMA DE NÓMINA ===");

        while (!salir)
        {
            MostrarMenuPrincipal();
            sbyte opcion = LeerOpcionMenu();

            switch (opcion)
            {
                case 1:
                    IngresarOperario();
                    break;
                case 2:
                    IngresarTecnico();
                    break;
                case 3:
                    IngresarProfesional();
                    break;
                case 4:
                    BuscarEmpleado();
                    break;
                case 5:
                    MostrarEstadisticas();
                    break;
                case 6:
                    MostrarTodosEmpleados();
                    break;
                case 7:
                    salir = true;
                    Console.WriteLine("¡Hasta pronto!");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    break;
            }
        }
    }

    // MENÚ PRINCIPAL
    static void MostrarMenuPrincipal()
    {
        Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
        Console.WriteLine("1 - Ingresar Operario");
        Console.WriteLine("2 - Ingresar Técnico");
        Console.WriteLine("3 - Ingresar Profesional");
        Console.WriteLine("4 - Buscar Empleado");
        Console.WriteLine("5 - Ver Estadísticas");
        Console.WriteLine("6 - Mostrar Todos los Empleados");
        Console.WriteLine("7 - Salir");
        Console.Write("Seleccione una opción: ");
    }

    // FUNCIÓN para leer opción del menú
    static sbyte LeerOpcionMenu()
    {
        while (true)
        {
            try
            {
                sbyte opcion = Convert.ToSByte(Console.ReadLine());
                if (opcion >= 1 && opcion <= 7)
                    return opcion;
                else
                    Console.Write("Opción inválida (1-7). Intente de nuevo: ");
            }
            catch
            {
                Console.Write("Entrada inválida. Intente de nuevo: ");
            }
        }
    }

    // FUNCIÓN para ingresar nombre
    static string LeerNombre()
    {
        Console.Write("Ingrese nombre: ");
        string nombre = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(nombre))
        {
            Console.Write("El nombre no puede estar vacío. Ingrese nombre: ");
            nombre = Console.ReadLine();
        }
        return nombre.Trim();
    }

    // MÉTODO para ingresar Operario
    static void IngresarOperario()
    {
        Console.WriteLine("\n--- INGRESAR OPERARIO ---");
        ProcesarEmpleado(1);
    }

    // MÉTODO para ingresar Técnico
    static void IngresarTecnico()
    {
        Console.WriteLine("\n--- INGRESAR TÉCNICO ---");
        ProcesarEmpleado(2);
    }

    // MÉTODO para ingresar Profesional
    static void IngresarProfesional()
    {
        Console.WriteLine("\n--- INGRESAR PROFESIONAL ---");
        ProcesarEmpleado(3);
    }

    // MÉTODO principal para procesar empleado
    static void ProcesarEmpleado(sbyte tipoEmpleado)
    {
        // Verificar si hay espacio disponible
        if (contadorEmpleados >= TAM)
        {
            Console.WriteLine(" No se pueden ingresar más empleados. Límite alcanzado.");
            return;
        }

        // Leer datos del empleado
        string nombre = LeerNombre();
        uint id = LeerID();
        sbyte horas = LeerHorasTrabajadas();
        uint precioHora = LeerPrecioHora();

        // Calcular salarios
        uint salarioOrdinario = (uint)(horas * precioHora);
        uint aumento = CalcularAumento(salarioOrdinario, tipoEmpleado);
        uint salarioBruto = salarioOrdinario + aumento;
        uint deduccion = (uint)(salarioBruto * 0.0917m);
        uint salarioNeto = salarioBruto - deduccion;

        // Guardar en arreglos
        nombres[contadorEmpleados] = nombre;
        ids[contadorEmpleados] = id;
        tipos[contadorEmpleados] = tipoEmpleado;
        horasTrabajadas[contadorEmpleados] = horas;
        preciosHora[contadorEmpleados] = precioHora;
        salariosOrdinarios[contadorEmpleados] = salarioOrdinario;
        aumentos[contadorEmpleados] = aumento;
        salariosBrutos[contadorEmpleados] = salarioBruto;
        deducciones[contadorEmpleados] = deduccion;
        salariosNetos[contadorEmpleados] = salarioNeto;

        // Incrementar contador
        contadorEmpleados++;

        // Actualizar estadísticas
        ActualizarEstadisticas(tipoEmpleado, salarioNeto);

        // Mostrar resultados
        MostrarResumenEmpleado(contadorEmpleados - 1);

        Console.WriteLine($"Empleado registrado exitosamente. Espacio disponible: {TAM - contadorEmpleados}");
    }

    // FUNCIÓN para calcular aumento
    static uint CalcularAumento(uint salarioOrdinario, sbyte tipoEmpleado)
    {
        decimal porcentaje = tipoEmpleado switch
        {
            1 => 0.15m,  // Operario 15%
            2 => 0.10m,  // Técnico 10%
            3 => 0.05m,  // Profesional 5%
            _ => 0m
        };

        return (uint)(salarioOrdinario * porcentaje);
    }

    // MÉTODO para actualizar estadísticas
    static void ActualizarEstadisticas(sbyte tipoEmpleado, uint salarioNeto)
    {
        switch (tipoEmpleado)
        {
            case 1:
                Ope++;
                TotalOpe += salarioNeto;
                break;
            case 2:
                Tec++;
                TotalTec += salarioNeto;
                break;
            case 3:
                Pro++;
                TotalPro += salarioNeto;
                break;
        }
    }

    // MÉTODO para mostrar resumen del empleado
    static void MostrarResumenEmpleado(int indice)
    {
        string tipoTexto = tipos[indice] switch
        {
            1 => "Operario",
            2 => "Técnico",
            3 => "Profesional",
            _ => "Desconocido"
        };

        Console.WriteLine($"\n--- Resumen del Empleado ---");
        Console.WriteLine($"Cédula: {ids[indice]}");
        Console.WriteLine($"Nombre Empleado: {nombres[indice]}");
        Console.WriteLine($"Tipo Empleado: {tipos[indice]} ({tipoTexto})");
        Console.WriteLine($"Salario por Hora: {preciosHora[indice]}");
        Console.WriteLine($"Cantidad de Horas: {horasTrabajadas[indice]}");
        Console.WriteLine($"Salario Ordinario: {salariosOrdinarios[indice]}");
        Console.WriteLine($"Aumento: {aumentos[indice]}");
        Console.WriteLine($"Salario Bruto: {salariosBrutos[indice]}");
        Console.WriteLine($"Deducción CCSS: {deducciones[indice]}");
        Console.WriteLine($"Salario Neto: {salariosNetos[indice]}");
        Console.WriteLine("-----------------------------");
    }

    // MÉTODO de búsqueda básica
    static void BuscarEmpleado()
    {
        if (contadorEmpleados == 0)
        {
            Console.WriteLine("\nNo hay empleados registrados.");
            return;
        }

        Console.WriteLine("\n=== BUSCAR EMPLEADO ===");
        Console.WriteLine("1 - Buscar por ID");
        Console.WriteLine("2 - Buscar por nombre");
        Console.Write("Seleccione opción de búsqueda: ");

        try
        {
            sbyte opcionBusqueda = Convert.ToSByte(Console.ReadLine());

            switch (opcionBusqueda)
            {
                case 1:
                    BuscarPorID();
                    break;
                case 2:
                    BuscarPorNombre();
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Entrada inválida.");
        }
    }

    // MÉTODO para buscar por ID
    static void BuscarPorID()
    {
        Console.Write("Ingrese ID a buscar: ");
        try
        {
            uint idBuscado = Convert.ToUInt32(Console.ReadLine());
            bool encontrado = false;

            for (int i = 0; i < contadorEmpleados; i++)
            {
                if (ids[i] == idBuscado)
                {
                    Console.WriteLine("\n--- EMPLEADO ENCONTRADO ---");
                    MostrarResumenEmpleado(i);
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine($"No se encontró ningún empleado con ID: {idBuscado}");
            }
        }
        catch
        {
            Console.WriteLine("ID inválido.");
        }
    }

    // MÉTODO para buscar por nombre
    static void BuscarPorNombre()
    {
        Console.Write("Ingrese nombre a buscar: ");
        string nombreBuscado = Console.ReadLine().ToLower();
        bool encontrado = false;

        for (int i = 0; i < contadorEmpleados; i++)
        {
            if (nombres[i].ToLower().Contains(nombreBuscado))
            {
                if (!encontrado)
                {
                    Console.WriteLine("\n--- EMPLEADOS ENCONTRADOS ---");
                    encontrado = true;
                }
                MostrarResumenEmpleado(i);
            }
        }

        if (!encontrado)
        {
            Console.WriteLine($"No se encontraron empleados con nombre: {nombreBuscado}");
        }
    }

    // MÉTODO para mostrar todos los empleados
    static void MostrarTodosEmpleados()
    {
        if (contadorEmpleados == 0)
        {
            Console.WriteLine("\nNo hay empleados registrados.");
            return;
        }

        Console.WriteLine($"\n=== TODOS LOS EMPLEADOS ({contadorEmpleados} de {TAM} registros) ===");

        for (int i = 0; i < contadorEmpleados; i++)
        {
            string tipoTexto = tipos[i] switch
            {
                1 => "Operario",
                2 => "Técnico",
                3 => "Profesional",
                _ => "Desconocido"
            };

            Console.WriteLine($"[{i + 1}] ID: {ids[i]} | Nombre: {nombres[i]} | Tipo: {tipoTexto} | Salario Neto: {salariosNetos[i]}");
        }

        Console.WriteLine($"\nEspacio disponible: {TAM - contadorEmpleados}");
    }

    // MÉTODOS ORIGINALES para leer datos
    static uint LeerID()
    {
        while (true)
        {
            try
            {
                Console.Write("Ingrese ID: ");
                uint id = Convert.ToUInt32(Console.ReadLine());

                // Verificar si el ID ya existe
                bool idExiste = false;
                for (int i = 0; i < contadorEmpleados; i++)
                {
                    if (ids[i] == id)
                    {
                        idExiste = true;
                        break;
                    }
                }

                if (idExiste)
                {
                    Console.WriteLine("Este ID ya está registrado. Use otro ID.");
                }
                else
                {
                    return id;
                }
            }
            catch
            {
                Console.WriteLine("ID inválido. Intente de nuevo.");
            }
        }
    }

    static sbyte LeerHorasTrabajadas()
    {
        while (true)
        {
            try
            {
                Console.Write("Ingrese horas trabajadas: ");
                sbyte horas = Convert.ToSByte(Console.ReadLine());
                if (horas > 0) return horas;
                Console.WriteLine("Las horas deben ser mayores a 0.");
            }
            catch
            {
                Console.WriteLine("Horas inválidas. Intente de nuevo.");
            }
        }
    }

    static uint LeerPrecioHora()
    {
        while (true)
        {
            try
            {
                Console.Write("Ingrese precio por hora: ");
                uint precio = Convert.ToUInt32(Console.ReadLine());
                if (precio > 0) return precio;
                Console.WriteLine("El precio debe ser mayor a 0.");
            }
            catch
            {
                Console.WriteLine("Precio inválido. Intente de nuevo.");
            }
        }
    }

    // MÉTODO para mostrar estadísticas
    static void MostrarEstadisticas()
    {
        Console.WriteLine("\n=== ESTADÍSTICAS DE NÓMINA ===");

        // Operarios
        uint avgOpe = Ope > 0 ? TotalOpe / Ope : 0;
        Console.WriteLine($"1) Cantidad Empleados Tipo Operarios: {Ope}");
        Console.WriteLine($"2) Acumulado Salario Neto para Operarios: {TotalOpe}");
        Console.WriteLine($"3) Promedio Salario Neto para Operarios: {avgOpe}");

        // Técnicos
        uint avgTec = Tec > 0 ? TotalTec / Tec : 0;
        Console.WriteLine($"4) Cantidad Empleados Tipo Técnico: {Tec}");
        Console.WriteLine($"5) Acumulado Salario Neto para Técnicos: {TotalTec}");
        Console.WriteLine($"6) Promedio Salario Neto para Técnicos: {avgTec}");

        // Profesionales
        uint avgPro = Pro > 0 ? TotalPro / Pro : 0;
        Console.WriteLine($"7) Cantidad Empleados Tipo Profesional: {Pro}");
        Console.WriteLine($"8) Acumulado Salario Neto para Profesional: {TotalPro}");
        Console.WriteLine($"9) Promedio Salario Neto para Profesionales: {avgPro}");

        // Totales generales
        Console.WriteLine($"\nTotal general de empleados: {contadorEmpleados}");
        Console.WriteLine($"Espacio disponible: {TAM - contadorEmpleados}");
        Console.WriteLine($"Total general pagado: {TotalOpe + TotalTec + TotalPro}");
    }
}