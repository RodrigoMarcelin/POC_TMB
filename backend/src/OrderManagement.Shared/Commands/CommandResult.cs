using OrderManagement.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Shared.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {

        }
        public CommandResult(bool success, string message)
        {
            Success = success;
            Type = 0;
            Message = message;
            Data = null;
        }
        public CommandResult(bool success, string message, dynamic data)
        {
            Success = success;
            Type = 0;
            Message = message;
            Data = data;
        }
        public CommandResult(bool success, int type, string message, dynamic data)
        {
            Success = success;
            Type = type;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public int Type { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
