﻿namespace FormWise.WebApi.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
