﻿using System;
using System.Collections.Generic;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace WorkflowCore.Interface
{
    public interface IWorkflowBuilder
    {
        List<WorkflowStep> Steps { get; }

        int LastStep { get; }                

        IWorkflowBuilder<T> UseData<T>();

        WorkflowDefinition Build(string id, int version);

        void AddStep(WorkflowStep step);

        void AttachBranch(IWorkflowBuilder branch);
    }

    public interface IWorkflowBuilder<TData> : IWorkflowBuilder
    {        
        IStepBuilder<TData, TStep> StartWith<TStep>(Action<IStepBuilder<TData, TStep>> stepSetup = null) where TStep : IStepBody;

        IStepBuilder<TData, InlineStepBody> StartWith(Func<IStepExecutionContext, ExecutionResult> body);

        IStepBuilder<TData, ActionStepBody> StartWith(Action<IStepExecutionContext> body);

        /// <summary>
        /// 获取上一级步骤
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<WorkflowStep> GetUpstreamSteps(int id);

        IWorkflowBuilder<TData> UseDefaultErrorBehavior(WorkflowErrorHandling behavior, TimeSpan? retryInterval = null);

        IWorkflowBuilder<TData> CreateBranch();
                
    }
}