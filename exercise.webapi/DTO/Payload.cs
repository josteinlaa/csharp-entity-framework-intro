using System;

namespace exercise.webapi.DTO;

public class Payload<T> where T : class
{
    public T Data { get; set; }
    public string Message { get; set; } = "Success";
}
