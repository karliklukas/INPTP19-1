using System;

namespace INPTPZ1
{
    class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumber(double real = 0, double imaginary = 0)
        {
            this.Real = real;
            this.Imaginary = imaginary;
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public ComplexNumber Multiply(ComplexNumber secondNumber)
        {
            return new ComplexNumber()
            {
                Real = this.Real * secondNumber.Real - this.Imaginary * secondNumber.Imaginary,
                Imaginary = this.Real * secondNumber.Imaginary + this.Imaginary * secondNumber.Real
            };
        }

        public ComplexNumber Add(ComplexNumber secondNumber)
        {
            return new ComplexNumber()
            {
                Real = this.Real + secondNumber.Real,
                Imaginary = this.Imaginary + secondNumber.Imaginary
            };
        }
        public ComplexNumber Subtract(ComplexNumber secondNumber)
        {
            return new ComplexNumber()
            {
                Real = this.Real - secondNumber.Real,
                Imaginary = this.Imaginary - secondNumber.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        public ComplexNumber Divide(ComplexNumber secondNumber)
        {
            ComplexNumber dividedNumber = Multiply(new ComplexNumber() { Real = secondNumber.Real, Imaginary = -secondNumber.Imaginary });
            double divisor = Math.Pow(secondNumber.Real, 2) + Math.Pow(secondNumber.Imaginary, 2);

            return new ComplexNumber()
            {
                Real = dividedNumber.Real / divisor,
                Imaginary = dividedNumber.Imaginary / divisor
            };
        }
    }
}
