﻿namespace NadinSoft.Application.Contract.QueryResults.User;

public class GetAllUserQueryResult
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}