using WorkflowCore.Models;

namespace WorkflowCore.Interface
{
    public interface IExecutionPointerFactory
    {
        /// <summary>
        /// 构建初始节点
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        ExecutionPointer BuildGenesisPointer(WorkflowDefinition def);
        ExecutionPointer BuildCompensationPointer(WorkflowDefinition def, ExecutionPointer pointer, ExecutionPointer exceptionPointer, int compensationStepId);
        ExecutionPointer BuildNextPointer(WorkflowDefinition def, ExecutionPointer pointer, IStepOutcome outcomeTarget);
        ExecutionPointer BuildChildPointer(WorkflowDefinition def, ExecutionPointer pointer, int childDefinitionId, object branch);
    }
}