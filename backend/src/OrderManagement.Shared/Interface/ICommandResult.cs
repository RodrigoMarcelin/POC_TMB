using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Shared.Interface
{
    public interface ICommandResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        dynamic Data { get; set; }
    }
}
