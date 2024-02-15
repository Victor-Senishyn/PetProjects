﻿using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IAccessCardService
    {
        Task<AccessCardDto> CreateAccessCardAsync(EmployeeDto employeeDto, CancellationToken cancellationToken);
        Task<AccessCardDto> GetAccessCardByIdAsync(long id, CancellationToken cancellationToken);
    }
}
