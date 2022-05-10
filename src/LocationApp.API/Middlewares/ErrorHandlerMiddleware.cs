﻿using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                InvalidDataException e =>
                    (int) HttpStatusCode.BadRequest,
                _ => (int) HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new {errorMessage = error?.Message});
            await response.WriteAsync(result);
        }
    }
}