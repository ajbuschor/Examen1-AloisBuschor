using System;
using System.Collections.Generic;  
using System.Linq;

class Paciente //creando la clase paciente , en donde las variables podran ser llamadas o ser asignadas. metodo get/set
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string TipoSangre { get; set; }
    public string Direccion { get; set; }
    public DateTime FechaNacimiento { get; set; } // ya que la fecha que deberia de ser ingresado es una fecha de nacimiento existe un metodo llamado datetime para poder poner las fechas con /0/
    public string NumeroCedula { get; set; }
}

class Medicamento //creando la clase medicamento, en donde las variables podran ser llamadas o ser asignadas. metodo get/set permitiendo encapsular 
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
}

class Tratamiento //creando la clase tratamiento incializando , y asignado los get y set para permitir encapsular podiendo agregar logica adicional
{
    public Paciente Paciente { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
}

class Program //clase del progrmama principal en donde utilizo listas e las inicializo 
{
    static List<Paciente> pacientes = new List<Paciente>();
    static List<Medicamento> catalogoMedicamentos = new List<Medicamento>();
    static List<Tratamiento> tratamientos = new List<Tratamiento>();

    static void Main(string[] args) //static void main ejecutaria las opciones principales cual el usuario elige
    {
        int opcion;      //el ciclo permite repetirse en caso de agregar mas opciones o datos del paciente y en caso de querer salirlse del menu el usuario tendria que presionar el 0
        do
        {
            MostrarMenu();
            opcion = int.Parse(Console.ReadLine()); // el int.Parse visto en clases , utilizado como una cadena en donde se convierte en un valor numerico.
            switch (opcion)
            {
                case 1:                            //opciones en donde seria elegido por el usuario y donde se ejecutarian ciertas clases creadas
                    AgregarPaciente();
                    break;
                case 2:
                    AgregarMedicamentoCatalogo();
                    break;
                case 3:
                    AsignarTratamientoMedico();
                    break;
                case 4:
                    Consultas();
                    break;
                case 0:
                    Console.WriteLine("Gracias, saliendo del programa , hasta luego "); // en caso de que el usuario quiera salirse del programa
                    break;
                default:
                    Console.WriteLine("Opcion invalida"); // en caso de que se haya puesto una letra o un numero equivocado salga la opcion que fue invalida
                    break;
            }
        } while (opcion != 0);
    }

    static void MostrarMenu() // Menu principal del sistema dandole las opciones al usuario para elegir
    {
        Console.WriteLine("----------Menu principal----------- ");
        Console.WriteLine("1--- Agregar paciente ");
        Console.WriteLine("2--- Agregar medicamento al catalogo ");
        Console.WriteLine("3--- Asignar tratamiento medico a un paciente ");
        Console.WriteLine("4--- Consultas ");
        Console.WriteLine("0--- Salirse del sistema ");
        Console.Write("Seleccione una opcion, en caso de querer salirse seleccione (0): ");
    }

    static void AgregarPaciente() // al agregar un paciente , se le pedira al usuario que ingrese los datos del paciente, pidiendo el nombre, cedula, telefono, tipo de sangre, dirrecion, fehca de nacimiento
    {
        Console.WriteLine("Por Favor, Ingrese los datos del paciente :");
        Console.Write("Nombre completo: ");
        string nombre = Console.ReadLine();
        Console.Write("Numero de cedula: ");
        string numeroCedula = Console.ReadLine();
        Console.Write("Telefono celular: ");
        string telefono = Console.ReadLine();
        Console.Write("Tipo de sangre : ");
        string tipoSangre = Console.ReadLine();
        Console.Write("Direccion : ");
        string direccion = Console.ReadLine();
        Console.Write("Fecha de Nacimiento (dia/mes/anio): ");
        DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine()); // fecha de nacimiento sera registrado como fecha dia , mes, y el anio siendo obligado a poner valores numericos. 

        Paciente paciente = new Paciente //creando nuevo objeto,clase igual llamado paciente, para guardar los datos correctamente 
        {
            Nombre = nombre,
            NumeroCedula = numeroCedula,
            Telefono = telefono,
            TipoSangre = tipoSangre,
            Direccion = direccion,
            FechaNacimiento = fechaNacimiento
        };

        pacientes.Add(paciente); // aniadiendo datos de pacientes dentro de la lista para que sea guardada la informacion ingresada del usuario
        Console.WriteLine("Paciente agregado de forma correcta...");
    }

    static void AgregarMedicamentoCatalogo() // creando la clase para agregar el catalogo del medicamento
    {
        Console.WriteLine("Por Favor, Ingrese los datos del medicamento :");
        Console.Write("Ingrese el codigo del medicamento : ");
        string codigo = Console.ReadLine();
        Console.Write("El Nombre del medicamento : ");
        string nombre = Console.ReadLine();
        Console.Write("La Cantidad deseada : ");
        int cantidad = int.Parse(Console.ReadLine());           //pidiendole al usuario que ingrese los datos del medicamento

        Medicamento medicamento = new Medicamento // creando nuevo objeto llamado igual medicamento para guardar los datos correctamente en la lista array
        {
            Codigo = codigo,
            Nombre = nombre,
            Cantidad = cantidad
        };

        catalogoMedicamentos.Add(medicamento);
        Console.WriteLine("Medicamento agregado al catalogo de forma exitosa...");// salta por pantalla el mensaje confirmando que los datos se hayan guardado 
    }

    static void AsignarTratamientoMedico() //clase, para asignar tratamiento medico pidiendole el numero de cedula para encontrar la data
    {
        Console.WriteLine("Ingrese el numero de cedula del paciente al que desea asignar el tratamiento: "); // buscando al paciente con base a la cedula ingresada
        string numeroCedula = Console.ReadLine();
        Paciente paciente = pacientes.FirstOrDefault(p => p.NumeroCedula == numeroCedula);

        if (paciente == null) // si el paciente es igual a nulo se ejecutaria por pantalla que la cedula no fue encontrada
        {
            Console.WriteLine("Ningun paciente fue encontrado con ese numero de cedula ingresada...");
            return;
        }

        Console.WriteLine("Seleccione los medicamentos para el tratamiento (max 3):"); // el usuario solo podra elegir maximo 3 opciones de medicamentos
        List<Medicamento> medicamentosTratamiento = new List<Medicamento>(); // guardada en la lista de medicamentos
        for (int i = 0; i < catalogoMedicamentos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {catalogoMedicamentos[i].Nombre} (Cantidad: {catalogoMedicamentos[i].Cantidad})"); // ciclo donde salta por pantalla la cantidad del catalogo de medicamentos
        }

        while (medicamentosTratamiento.Count < 3) // mientras la opcion sea dentro de maximo 3 se ejecutaria pidiendo la seleccion del medicamento en donde el usuario tendra que poner una opcion numerica
        {
            Console.Write($"Seleccione el medicamento {medicamentosTratamiento.Count + 1} (0 para terminar y salir ): ");
            int indexMedicamento = int.Parse(Console.ReadLine()) - 1;
            if (indexMedicamento == -1 || medicamentosTratamiento.Count >= 3)
            {
                break;
            }
            if (indexMedicamento < 0 || indexMedicamento >= catalogoMedicamentos.Count) //confirmacion por si la opcion ingresada haya sido incorrecta 
            {
                Console.WriteLine("Medicamento no valido...");
                continue;
            }
            Medicamento medicamentoSeleccionado = catalogoMedicamentos[indexMedicamento];
            if (medicamentoSeleccionado.Cantidad > 0)
            {
                medicamentosTratamiento.Add(medicamentoSeleccionado); // si fue correctamente ejecutado, sera guardado correctamente la cantidad
                medicamentoSeleccionado.Cantidad--;
            }
            else
            {
                Console.WriteLine("No hay suficiente cantidades para este medicamento"); // si se pasa la cantidad del medicamente confirma por pantalla que no habria suficientes cantidades
            }
        }

        Tratamiento tratamiento = new Tratamiento // guardando la informacion dada en una misma clase, objeto tratamiento ejecutaria por pantalla que el tratamiento fue guardado correctamente
        {
            Paciente = paciente,
            Medicamentos = medicamentosTratamiento
        };

        tratamientos.Add(tratamiento);
        Console.WriteLine("Tratamiento asignado de forma exitosa...");
    }

    static void Consultas() // creando clase de consultas monstrando las estadisticas y reportes finales por pantalla
    {
        Console.WriteLine($"Cantidad total de pacientes registrados: {pacientes.Count}"); // pacientes contados que fueron ingresado correctamente 

        Console.WriteLine("Reporte de todos los medicamentos recetados sin ser repetidos:");
        List<string> medicamentosRecetados = tratamientos.SelectMany(t => t.Medicamentos.Select(m => m.Nombre)).Distinct().ToList(); 
        foreach (var medicamento in medicamentosRecetados) //el foreach recorre cada medicamento en la lista de medicamentos recetados , sacando la cantidad 
        {
            Console.WriteLine(medicamento);
        }

        Console.WriteLine("Reporte de cantidad de pacientes agrupados por edades:");
        var gruposEdades = pacientes.GroupBy(p => CalcularGrupoEdad(p.FechaNacimiento));
        foreach (var grupo in gruposEdades) // el foreach recorre cada grupo en la lista de grupos de edades buscando la cantidad de pacientes 
        {
            Console.WriteLine($"{grupo.Key}: {grupo.Count()}");
        }

        Console.WriteLine("Reporte de Pacientes y consultas ordenadas por el nombre:");
        var pacientesOrdenados = pacientes.OrderBy(p => p.Nombre);
        foreach (var paciente in pacientesOrdenados) // este foreach recorre cada paciente dentro de la lista de pacientes ordenados sacando los del paciente y consultas por nombre
        {
            Console.WriteLine($"Nombre: {paciente.Nombre}, Numero de cedula: {paciente.NumeroCedula}, Telefono: {paciente.Telefono}, Tipo de sangre: {paciente.TipoSangre}, Direccion: {paciente.Direccion}, Fecha de nacimiento: {paciente.FechaNacimiento.ToString("dd/MM/yyyy")}");
        } //imprimiendo por pantalla todos los datos del paciente ingresado
    }

    static string CalcularGrupoEdad(DateTime fechaNacimiento) //clase en donde se calcula la edad con base a la fecha de naciemiento ingresada por el paciente
    {
        try // el 'try'catch' donde se ejcutaria lo de adentro del try , donde podrian ocurrir expeciones como controlar errores 
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year; //comparando el año de nacimiento que sea en este momento no en el futuro logrando asi sacar la edad 
            if (DateTime.Now < fechaNacimiento.AddYears(edad))
            {
                edad--;
            }

            if (edad <= 10)
            {
                return "0-10 años"; //monstrando la cantidad de pacientes con la edad dentro de 0 a 10 años
            }
            else if (edad <= 30)
            {
                return "11-30 años"; //muestra los dentro de 11 a 30 años
            }
            else if (edad <= 50)
            {
                return "31-50 años"; // dentro de 31 a 50 , en donde el else retorna los de mayores de 51 años
            }
            else
            {
                return "mayores de 51 años";
            }
        }
        catch (Exception ex) // el catch por si ocurre cierto error se ejecutaria la exepcion monstrando por pantalla "que un hubo un error al calcular la edad"
        {
            Console.WriteLine("Error al calcular la edad: " + ex.Message);
            throw; 
        }
    }





}