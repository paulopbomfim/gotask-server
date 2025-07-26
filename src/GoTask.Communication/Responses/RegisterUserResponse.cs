﻿namespace GoTask.Communication.Responses;

public record RegisterUserResponse()
{
    public string Name { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}