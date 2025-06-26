
namespace DevMindSpeed.Entity.Aggregates.QuestionAggregate
{
    public static class NewQuestion
    {
        public static string GetEquation(int difficulty)
        { 
            Random rnd = new Random();

            double rand_1 = rnd.NextDouble();
            double rand_2 = rnd.NextDouble();
            int rand_3 = rnd.Next(1, 5);  // 1:+, 2:-, 3:*, 4:/

            double scale = difficulty == 1 ? 10 :
                           difficulty == 2 ? 100 :
                           difficulty == 3 ? 1000 : 10000;

            rand_1 *= scale;
            rand_2 *= scale;

            int operand_1 = (int)Math.Floor(rand_1);
            int operand_2 = (int)Math.Floor(rand_2);

            string operation;

            if (rand_3 == 4)
            {
                // Ensure operand_2 is not zero for division
                while (operand_2 == 0)
                {
                    rand_2 = rnd.NextDouble() * scale;
                    operand_2 = (int)Math.Floor(rand_2);
                }
                operation = "/";
            }
            else
            {
                operation = rand_3 == 1 ? "+" : rand_3 == 2 ? "-" : "*";
            }

            string Equation = operand_1 + " " + operation + " " + operand_2;
            return Equation;
        }

        public static double SolveEquation(string equation)
        {
            
            string[] parts = equation.Split(' ');
           
            double operand1 = double.Parse(parts[0]);
            string operation = parts[1];
            double operand2 = double.Parse(parts[2]);

            switch (operation)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    return operand1 / operand2;
                default:
                    return 0;
            }
        }

    }
}
