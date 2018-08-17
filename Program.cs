using System;
/*
Copyright (c) <2018> <Ramon Rodriguez>

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the 
Software without restriction, including without limitation the rights to use, copy, 
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the 
following conditions:

The above copyright notice and this permission notice shall be included in all copies
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR 
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Author: Ramon Rodriguez, Mat.:00-EISN-1-407, Seccion: 0908,
Descripcion: 
Esta aplicacion permite a los usuarios(Profesores y alta gerencia) tener la posibilidad de tener un 
listado de los estudiantes de manera semi-manual, permitiendole agregar, visualizar, borrar estudiantes 
en una Lista, ademas permite  ver estudiantes de manera individual ademas ver promedios general del listado. 
la aplicacion contiene algunas  validaciones para evitar que se intenten ciertas violaciones en las entradas
*/


using System.Collections;
using System.Collections.Generic;
using System.Threading;
namespace proyecto_final
{
    class Program
    {

        public class Estudiante
        {
            public string nombre { set; get; }
            public string matricula { set; get; }
            public int materia_inscritas { set; get; }
            public double promedio { set; get; }
            public string toString()
            {
                return "---------------------------------\n" +
                        "Nombre : " + nombre + "\n" +
                        "Matricula " + matricula + "\n" +
                        "Materias Inscritas " + materia_inscritas + "\n" +
                        "Promedio actual de notas: " + promedio + "\n";
            }

            public void imprimir_este_estudiante() {

                Console.WriteLine("---------------------------------\n" +
                        "Nombre : " + nombre + "\n" +
                        "Matricula " + matricula + "\n" +
                        "Materias Inscritas " + materia_inscritas + "\n" +
                        "Promedio actual de notas: " + promedio + "\n");

            }

        }

        public static List<Estudiante> listado_estudiantes= new List<Estudiante>();


        public Program()
        {
     
        }

        void ImprimirEstudiante()
        {
            string mtr = "Nombre          |   Matricula ";
            mtr += "Ramon Rodrigues |   00-EISN-1-407";
            Console.WriteLine(mtr);
        }

        void CapturarEstudiante()
        {
            try
            {
                Estudiante est = new Estudiante();
                Console.WriteLine("Ingrese el Nombre del Estudiante: ");
                est.nombre = Console.ReadLine();
                Console.WriteLine("Ingrese la Matricula: ");
                est.matricula = Console.ReadLine();
                if(BuscarEstudiantePorMatricula(est.matricula)==true)
                {
                    Console.WriteLine("Matricula ya registrada el sistema.");
                    Thread.Sleep(2000);
                    return;
                }

                Console.WriteLine("Ingrese la cantidad  de materias inscritas: ");
                est.materia_inscritas = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese el promedio del estudiante: ");
                est.promedio = Double.Parse(Console.ReadLine());
                int resultado_insercion;
                Console.WriteLine(est.toString());
                Console.WriteLine("\n\nEsta Correcta la insercion de Datos 1=si otro valor es no ?");
                resultado_insercion = Int32.Parse(Console.ReadLine());
                if (resultado_insercion == 1)
                {
                    listado_estudiantes.Add(est);
                    Console.WriteLine("Conteo de usuarios Actualizados Cantidad de estudiante actuales " + listado_estudiantes.Count);
                    Console.WriteLine("Estudiante Insertado Correctamente");
                    return;
                }
                else
                {
                    Console.WriteLine("------------------------------------------------------\n");
                    CapturarEstudiante();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hubo un Error en la insercion de los Datos, por favor vuelva intentarlo");
                CapturarEstudiante();
            }

        }


       public void visualizarEstudiantes()
        {
            Console.WriteLine("Visualizacion de estudiante.\nCantidad de estudiantes en Lista  #"+listado_estudiantes.Count+"\n");
            int iterador = 1;
            foreach (Estudiante est in listado_estudiantes)
            {
                Console.WriteLine("Estudiante # " + iterador);
                est.imprimir_este_estudiante();
                iterador++;
            }
        }


       public void verPromedio() {
            {
                double sum = 0.0f;
                int iter = 0;
                foreach(Estudiante est in listado_estudiantes)
                {
                    sum += est.promedio;
                    ++iter;
                }
                if(iter==0)
                {
                    Console.WriteLine("-- Listado Vacio -- ");
                    return;
                }

                double promedio_general = sum / iter;

                Console.Write("El promedio general de estudiantes en el listado es el siguiente\n\n>>> #[ " + promedio_general + "]");
            }

        }


   

        public bool BuscarEstudiantePorMatricula(string mat)
        {
            foreach(Estudiante est in listado_estudiantes)
            {
                if (est.matricula == mat)
                    return true;
            }
            return false;
        }

        public void BuscarYverEstudiante() {
            int opcion = -1;
            bool seguir = true;
            do
            {
                    Console.Write("Ingrese el Matricula del Estudiante:\n>>>");
                    string nombre = Console.ReadLine();
                    if (BuscarEstudiantePorMatricula(nombre))
                    {
                        Estudiante est = null;
                       foreach(Estudiante e in listado_estudiantes )
                        {
                        if (e.matricula == nombre)
                        { est = e; break; }
                        }

                    if (est != null)
                        est.imprimir_este_estudiante();
                        seguir = false;
                    }
                    else
                    {
                        Console.WriteLine("Estudiante no Encontrado");
                        seguir = false;
                    }
            } while (seguir);

        }

        int buscarPorMatricula(string m)
        {
            int iterador = 0;
            int retorno = -1;
            foreach(Estudiante e in listado_estudiantes)
            {
            
                if(e.matricula == m)
                {
                    retorno = iterador;
                    break;
                }
                iterador++; 
            }
            return retorno;
        }



        void removerEstudiante()
        {
            Console.WriteLine("Ingrese la matricula del estudiante a remover de listado");
             string matr = Console.ReadLine();
            int resultado_busqueda = buscarPorMatricula(matr);
             if(resultado_busqueda==-1)
            {
                Console.WriteLine("Matricula no existe.");
            }
             listado_estudiantes.RemoveAt(resultado_busqueda);
              
        }



        void MostrarOpciones()
        {
            Console.Clear();
            int opcion = -1;
            Console.WriteLine("                              ---Listado de Estudiantes-----");
            Console.WriteLine("                                 " + DateTime.Now.ToString());

            Console.WriteLine("1- Agregar Estudiante.");
            Console.WriteLine("2- Visualizar Estudiantes.");
            Console.WriteLine("3- Ver Promedio.");
            Console.WriteLine("4- Buscar Estudiante.");
            Console.WriteLine("5- Remover Estudiante.");
            Console.WriteLine("6- Acerca de esta Aplicacion.");
            Console.Write("\nPor Favor ingrese La opcion elegida.\n>>>");
            try
            {
                opcion = Int32.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1: CapturarEstudiante(); break;
                    case 2: visualizarEstudiantes(); break;
                    case 3: verPromedio(); break;
                    case 4: BuscarYverEstudiante(); break;
                    case 5: removerEstudiante(); break;
                    case 6: ImprimirEstudiante(); break;
                    default: Console.WriteLine("Opcion invalida\a\n"); break;
                }
            }catch(Exception e)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Opcion invalidad");
                Thread.Sleep(2 * 1000);
                Console.BackgroundColor = ConsoleColor.Black;
                opcion = -1;
                MostrarOpciones();
            }
            Thread.Sleep(2000);

            }


        static void Main(string[] args)
        {
            bool para_siempre = true;
            while (para_siempre)
            {
                new Program().MostrarOpciones();
            }
        }
    }
}
