﻿namespace RassoApi.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string? Email { get; set; }
        
        public Role role { get; set; } = ;
    }

}
