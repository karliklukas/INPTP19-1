using System.Collections.Generic;


namespace INPTPZ1
{
    class Polynome
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynome()
        {
            Coefficients = new List<ComplexNumber>();
        }

        public Polynome Derive()
        {
            Polynome polynome = new Polynome();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynome.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return polynome;
        }

        public ComplexNumber Eval(ComplexNumber secondNumber)
        {
            ComplexNumber evaluatedNumber = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber copiedSecondNumber = secondNumber;                

                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                    {
                        copiedSecondNumber = copiedSecondNumber.Multiply(secondNumber);
                    }

                        coefficient = coefficient.Multiply(copiedSecondNumber);
                }

                evaluatedNumber = evaluatedNumber.Add(coefficient);
            }

            return evaluatedNumber;
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i < Coefficients.Count - 1)
                {
                    result += " + ";
                }
                
            }
            return result;
        }
    }
}
