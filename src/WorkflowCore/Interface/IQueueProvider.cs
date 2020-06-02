using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowCore.Interface
{
    /// <remarks>
    /// 该接口的实现将负责提供（分布式）队列机制来处理待办工作流
    /// </remarks>
    /// The implemention of this interface will be responsible for
    /// providing a (distributed) queueing mechanism to manage in flight workflows    
    public interface IQueueProvider : IDisposable
    {

        /// <summary>
        /// 将工作入队列，让其由集群中的一台主机处理
        /// </summary>
        /// Enqueues work to be processed by a host in the cluster
        /// <param name="Id"></param>
        /// <returns></returns>
        Task QueueWork(string id, QueueType queue);

        /// <summary>
        /// 从队列中取出第一个待办项。
        /// 若为空，则返回 NULL
        /// </summary>
        /// Fetches the next work item from the front of the process queue.
        /// If the queue is empty, NULL is returned
        /// <returns></returns>
        Task<string> DequeueWork(QueueType queue, CancellationToken cancellationToken);

        bool IsDequeueBlocking { get; }

        Task Start();

        Task Stop();
    }

    public enum QueueType { Workflow = 0, Event = 1, Index = 2 }
}
