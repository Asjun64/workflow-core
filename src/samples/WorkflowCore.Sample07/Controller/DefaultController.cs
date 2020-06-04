using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkflowCore.Interface;

namespace WorkflowCore.Sample07.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IWorkflowHost _host;
        public DefaultController(IWorkflowHost host)
        {
            this._host = host;
        }
        public async Task<ActionResult> Get(int i)
        {
            object data;
            if (i == 0)
            {
                data = new Sample03.MyDataClass
                {
                    Value1 = 1,
                    Value2 = 2
                };
                await _host.StartWorkflow("PassingDataWorkflow", data);
            }
            else
            {
                data = new Sample04.MyDataClass
                {
                    Value1 = "9"
                };
                var workflowId = _host.StartWorkflow("EventSampleWorkflow").Result;

                Action action = () =>
                {
                    Thread.Sleep(3000);
                    _host.PublishEvent("MyEvent", workflowId, "9");
                };
                action?.Invoke();

            }
            return new JsonResult(new
            {
                Workflow = i == 0 ? "PassingDataWorkflow" : "EventSampleWorkflow",
                Data = data
            });
        }
    }
}