using System;

class Program
{
    // Variables globales
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
                    MostrarEstadisticas();
                    break;
                case 5:
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
        Console.WriteLine("4 - Ver Estadísticas");
        Console.WriteLine("5 - Salir");
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
                if (opcion >= 1 && opcion <= 5)
                    return opcion;
                else
                    Console.Write("Opción inválida (1-5). Intente de nuevo: ");
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
        return Console.ReadLine();
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
        // Leer datos del empleado
        string nombre = LeerNombre();
        uint id = LeerID();
        sbyte horasTrabajadas = LeerHorasTrabajadas();
        uint precioHora = LeerPrecioHora();

        // Calcular salarios
        uint salarioOrdinario = (uint)(horasTrabajadas * precioHora);
        uint aumento = CalcularAumento(salarioOrdinario, tipoEmpleado);
        uint salarioBruto = salarioOrdinario + aumento;
        uint deduccion = (uint)(salarioBruto * 0.0917m);
        uint salarioNeto = salarioBruto - deduccion;

        // Actualizar estadísticas
        ActualizarEstadisticas(tipoEmpleado, salarioNeto);

        // Mostrar resultados
        MostrarResumenEmpleado(nombre, id, tipoEmpleado, precioHora, horasTrabajadas,
                              salarioOrdinario, aumento, salarioBruto, deduccion, salarioNeto);
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
    static void MostrarResumenEmpleado(string nombre, uint id, sbyte tipo, uint precioHora,
                                      sbyte horas, uint salarioOrdinario, uint aumento,
                                      uint salarioBruto, uint deduccion, uint salarioNeto)
    {
        string tipoTexto = tipo switch
        {
            1 => "Operario",
            2 => "Técnico",
            3 => "Profesional",
            _ => "Desconocido"
        };

        Console.WriteLine($"\n--- Resumen del Empleado ---");
        Console.WriteLine($"Cédula: {id}");
        Console.WriteLine($"Nombre Empleado: {nombre}");
        Console.WriteLine($"Tipo Empleado: {tipo} ({tipoTexto})");
        Console.WriteLine($"Salario por Hora: {precioHora}");
        Console.WriteLine($"Cantidad de Horas: {horas}");
        Console.WriteLine($"Salario Ordinario: {salarioOrdinario}");
        Console.WriteLine($"Aumento: {aumento}");
        Console.WriteLine($"Salario Bruto: {salarioBruto}");
        Console.WriteLine($"Deducción CCSS: {deduccion}");
        Console.WriteLine($"Salario Neto: {salarioNeto}");
        Console.WriteLine("-----------------------------");
    }

    // MÉTODOS ORIGINALES para leer datos
    static uint LeerID()
    {
        while (true)
        {
            try
            {
                Console.Write("Ingrese ID: ");
                return Convert.ToUInt32(Console.ReadLine());
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
        Console.WriteLine($"\nTotal general de empleados: {Ope + Tec + Pro}");
    }
} 