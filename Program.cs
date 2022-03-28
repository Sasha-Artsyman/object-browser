using System;
using System.Reflection;

namespace ObjectBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Object browser ---");
            string typeName = "";
            do
            {
                Console.WriteLine("\nEnter a type name to evaluate");
                // предлогаем ввести имя типа
                Console.Write("or enter Q to quit: ");
                // или 'Q' для завершения
                typeName = Console.ReadLine();
                if (typeName.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                // попытка вывода информации
                try
                {
                    Type t = Type.GetType(typeName);
                    Console.WriteLine("");
                    ListVariousStats(t);
                    ListFields(t);
                    ListProps(t);
                    ListMethods(t);
                    ListInterfaces(t);
                }
                catch
                {
                    // если не найден тип
                    Console.WriteLine("Sorry, can’t find type");
                }
            }
            while (true);
        }
        static void ListVariousStats(Type t)
        {
            Console.WriteLine("--- Various Statistics: ");
            Console.WriteLine("Base class is: {0}", t.BaseType);
            Console.WriteLine("Is type abstract? {0}", t.IsAbstract);
            Console.WriteLine("Is type sealed? {0}", t.IsSealed);
            Console.WriteLine("Is type generic? {0}", t.IsGenericTypeDefinition);
            Console.WriteLine("Is type a class type? {0}", t.IsClass);

            Console.WriteLine();
        }
        static void ListFields(Type t)
        {
            Console.WriteLine("--- Fields: ");
            var fieldNames = from f in t.GetFields() select f.Name;
            foreach (var name in fieldNames)
                Console.WriteLine("->{0}", name);

            Console.WriteLine();
        }
            static void ListProps(Type t)
        {
            Console.WriteLine("--- Properties: ");
            var propNames = from p in t.GetProperties() select p.Name;
            foreach (var name in propNames)
                Console.WriteLine("->{0}", name);

            Console.WriteLine();
        }

        static void ListInterfaces(Type t)
        {
            Console.WriteLine("--- Interfaces: ");
            var ifaces = from i in t.GetInterfaces() select i;
            foreach (Type i in ifaces)
                Console.WriteLine("->{0}", i.Name);
        }
        static void ListMethods(Type t)
        {
            Console.WriteLine("--- Methods: ");
            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)
            {
                // информация о типе
                string retVal = m.ReturnType.FullName;
                string paramlnfo = " ( ";
                // Получить информацию о параметрах.
                foreach (ParameterInfo pi in m.GetParameters())
                {
                    paramlnfo += string.Format("{0} {1} ", pi.ParameterType, pi.Name);
                }
                paramlnfo += " ) ";
                // Отобразить базовую сигнатуру метода.
                Console.WriteLine("->{0} {1} {2} ", retVal, m.Name, paramlnfo);
            }
            Console.WriteLine();
        }
    }
}