using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4CalculatorEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Antlr4CalculatorEngineTest
{
    [TestClass]
    public class CalculateTest
    {
        [TestMethod]
        public void Test_Visitor_With_SingleExpression_And_MultipleVariables_Returns_Variables()
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
        }
    }
}
