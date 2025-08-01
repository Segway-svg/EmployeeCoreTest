﻿using WebApplication3.Requests;
using WebApplication3.Responses;

namespace WebApplication3.Commands.Interfaces
{
    public interface IEditEmployeeCommand
    {
        Task<OperationResultResponse<bool>> ExecuteAsync(int id, EditEmployeeRequest request);
    }
}
