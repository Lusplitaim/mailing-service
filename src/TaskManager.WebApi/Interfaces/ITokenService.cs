﻿using TaskManager.Core.Models;

namespace TaskManager.WebApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
