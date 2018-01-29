using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Antlr4CalculatorEngine
{
    public class CalculatorBaseVisitorImpl : CalculatorBaseVisitor<decimal>
    {
        private readonly Dictionary<string, decimal> variables;

        public CalculatorBaseVisitorImpl()
        {
            variables = new Dictionary<string, decimal>();
        }

        public IDictionary<string, decimal> Variables
        {
            get { return variables; }
        }

        public override decimal VisitPlus([NotNull] CalculatorParser.PlusContext context)
        {
            return Visit(context.plusOrMinus()) + Visit(context.multOrDiv());
        }

        public override decimal VisitMinus([NotNull] CalculatorParser.MinusContext context)
        {
            return Visit(context.plusOrMinus()) - Visit(context.multOrDiv());
        }

        public override decimal VisitMultiplication([NotNull] CalculatorParser.MultiplicationContext context)
        {
            return Visit(context.multOrDiv()) * Visit(context.pow());
        }

        public override decimal VisitDivision([NotNull] CalculatorParser.DivisionContext context)
        {
            return Visit(context.multOrDiv()) / Visit(context.pow());
        }

        public override decimal VisitVariable([NotNull] CalculatorParser.VariableContext context)
        {
            return variables[context.ID().GetText()];
        }

        public override decimal VisitSetVariable([NotNull] CalculatorParser.SetVariableContext context)
        {
            decimal value = Visit(context.plusOrMinus());
            variables.Add(context.ID().GetText(), value);
            return base.VisitSetVariable(context);
        }

        public override decimal VisitPower([NotNull] CalculatorParser.PowerContext context)
        {
            if (context.pow() != null)
                return (decimal)Math.Pow((double)Visit(context.unaryMinus()), (double)Visit(context.pow()));
            return Visit(context.unaryMinus());
        }

        public override decimal VisitChangeSign([NotNull] CalculatorParser.ChangeSignContext context)
        {
            return -1m * Visit(context.unaryMinus());
        }

        public override decimal VisitBraces([NotNull] CalculatorParser.BracesContext context)
        {
            return Visit(context.plusOrMinus());
        }

        public override decimal VisitConstantPI([NotNull] CalculatorParser.ConstantPIContext context)
        {
            return (decimal)Math.PI;
        }

        public override decimal VisitConstantE([NotNull] CalculatorParser.ConstantEContext context)
        {
            return (decimal)Math.E;
        }

        public override decimal VisitInt([NotNull] CalculatorParser.IntContext context)
        {
            return Decimal.Parse(context.INT().GetText());
        }

        public override decimal VisitDouble([NotNull] CalculatorParser.DoubleContext context)
        {
            return Decimal.Parse(context.DOUBLE().GetText());
        }

        public override decimal VisitCalculate([NotNull] CalculatorParser.CalculateContext context)
        {
            return Visit(context.plusOrMinus());
        }
    }
}
