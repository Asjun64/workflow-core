using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Sample06.Steps
{
    public class SleepStep : StepBody
    {
        
        public TimeSpan Period { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            if (context.PersistenceData == null)
                return ExecutionResult.Sleep(Period, 0);

            int.TryParse(context.PersistenceData.ToString(), out int data);
            Console.WriteLine($"{context.Step.Name}: {data}");
            if (data < 5)
                return ExecutionResult.Sleep(Period, data+1);
            else
                return ExecutionResult.Next();
        }
    }
}
