using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Models;

namespace WorkflowCore.Interface
{
    /// <summary>
    /// 该接口负责提供持久化方案
    /// </summary>
    public interface IPersistenceProvider : IWorkflowRepository, ISubscriptionRepository, IEventRepository
    {        

        Task PersistErrors(IEnumerable<ExecutionError> errors);

        void EnsureStoreExists();

    }
}
