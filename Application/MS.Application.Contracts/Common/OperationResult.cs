using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Application.Contracts.Common;

public class OperationResult
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public OperationResult()
    {
        Success = false;
    }

    public OperationResult Succeeded(string message = "Succeeded!")
    {
        Success = true;
        Message = message;
        return this;
    }

    public OperationResult Failed(string message = "Failed! Try Again.")
    {
        Success = false;
        Message = message;
        return this;
    }
}

