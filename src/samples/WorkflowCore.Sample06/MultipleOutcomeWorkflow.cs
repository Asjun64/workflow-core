using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Sample06.Steps;

namespace WorkflowCore.Sample06
{    
    public class MultipleOutcomeWorkflow : IWorkflow
    {
        public string Id => "MultipleOutcomeWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<RandomOutput>(x => x.Name("Random Step"))
                    .When(data => 0).Do(then => then
                        .StartWith(context => { Console.WriteLine(context.Step.Name); })
                        .Name("branch A")
                        .Then<TaskA>()
                        .Then<TaskB>()
                        .Then<RandomOutput>(x => x.Name("Branch A: Random Step"))
                        .When(data => 0).Do(then2 => then2
                            .StartWith<RandomOutput>(x => x.Name("Branch A->A"))
                            .Id("Branch A")
                            .Then<SleepStep>(x =>  x.Name("Sleep A->A"))
                            .End<RandomOutput>("Branch A->A"))
                        .When(data => 1).Do(then2 => then2
                            .StartWith<RandomOutput>(x => x.Name("Branch A->B"))
                            .Then<SleepStep>(x => x.Name("Sleep A->B"))
                            .End<RandomOutput>("Branch A->B")))
                    .When(data => 1).Do(then => then
                        .StartWith(context => { Console.WriteLine(context.Step.Name); })
                        .Name("branch C")
                        .Then<TaskC>()
                        .Then<TaskD>()
                        .Attach("Branch A"))
                    .End<RandomOutput>("Random Step");
        }
    }
}
