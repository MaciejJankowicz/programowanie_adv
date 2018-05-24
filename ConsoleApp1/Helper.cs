using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Reflection;

namespace ConsoleApp1
{
    public static class Helper
    {



        public static string Ctrt(Object c)
        {

            Type t = c.GetType();



            FieldInfo[] fields = t.GetFields();

            PropertyInfo[] props = t.GetProperties();

            MethodInfo[] mets = t.GetMethods();

            string str = "";



            str = t.ToString() + "\r\n \r\n Fields: \r\n";

            foreach (FieldInfo f in fields)

                str += f.FieldType + " " + f.Name + " = " + f.GetValue(c) + "\r\n";



            str += "\r\n Properties: \r\n";

            foreach (PropertyInfo f in props)

                str += f.PropertyType + " " + f.Name + " = " + f.GetValue(c) + "\r\n";



            str += "\r\n Methods: \r\n";

            foreach (MethodInfo f in mets)

            {

                str += f.Name + "\r\n";

                foreach (ParameterInfo p in f.GetParameters())

                    str += p.Name + " " + p.ParameterType.Name + " " + p.Position + "_____";



                str += "\r\n Return value: " + f.ReturnParameter + "\r\n\r\n";

            }



            object instance = Activator.CreateInstance(t);

            //props[1].SetValue(instance, "Jan");

            //props[2].SetValue(instance, "Kowalski");

            //props[4].SetValue(instance, 22);



            return str;

        }


    }
}
