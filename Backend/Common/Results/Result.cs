using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Results
{
    public class Result<T>
    {
        public bool Success { get; init; }
        public T? Value { get; init; }

        public string Error { get; init; } = "Une erreur est survenue, veuillez contacter votre administrateur";
        public List<string> ErrorsDetails { get; init; } = new();

        public static Result<T> Ok(T value) => new() { Success = true, Value = value };

        public static Result<T> Fail(string error) => new() { Success = false, Error = error };
        public static Result<T> Fail(string error, List<string> errors) => new() { Success = false, Error = error, ErrorsDetails = errors.ToList() };
    }
}
