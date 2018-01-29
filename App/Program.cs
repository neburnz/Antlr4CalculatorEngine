using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4CalculatorEngine;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var expressions = @"x = (((nrm / (1 + pct)) * (1 + (iss/100) + (pis/100))) / (1 + (iss/100) + (pis/100))) * (1 + (iss/100) + (pis/100) + frd)";
            AntlrInputStream input = new AntlrInputStream(expressions);
            CalculatorLexer lexer = new CalculatorLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CalculatorParser parser = new CalculatorParser(tokens);
            IParseTree tree = parser.input();

            CalculatorBaseVisitorImpl visitor = new CalculatorBaseVisitorImpl();

            visitor.Variables.Add("nrm", 1223.5251m);
            visitor.Variables.Add("pct", 0.05m);
            visitor.Variables.Add("iss", 5m);
            visitor.Variables.Add("pis", 3m);
            visitor.Variables.Add("frd", 0.012m);

            visitor.Visit(tree);

            var result = visitor.Variables["x"];
            Console.ReadLine();
        }
    }
}
