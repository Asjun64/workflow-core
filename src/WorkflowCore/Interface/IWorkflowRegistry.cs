using WorkflowCore.Models;

namespace WorkflowCore.Interface
{
    /// <summary>
    /// 工作流注册表
    /// 用于注册、获取和注销工作流，以及查询工作流是否注册
    /// </summary>
    public interface IWorkflowRegistry
    {
        void RegisterWorkflow(IWorkflow workflow);
        void RegisterWorkflow(WorkflowDefinition definition);
        void RegisterWorkflow<TData>(IWorkflow<TData> workflow) where TData : new();
        /// <summary>
        /// 根据 Id 和版本号获取工作流定义
        /// 
        /// 找不到则抛出 WorkflowNotRegisteredException 异常
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        WorkflowDefinition GetDefinition(string workflowId, int? version = null);
        bool IsRegistered(string workflowId, int version);
        /// <summary>
        /// 注销工作流定义
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="version"></param>
        void DeregisterWorkflow(string workflowId, int version);
    }
}
