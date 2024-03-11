using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TPW.Model
{
    public class Mathematics
    {
        public double add(double a, double b) { return a + b; }

        public double subtract(double a, double b) {  return a - b; }

        public double multiply(double a, double b) { return b * a; }

        public double divide(double a, double b) { 
            if (a==0 || b == 0)
            {
                throw new DivideByZeroException();
            }
            return a / b; 
        }
    
    }
}
