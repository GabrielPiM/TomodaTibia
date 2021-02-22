using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TomodaTibiaAPI.Utils
{
    public class Response<T>
    {
        public Response()
        {
            Errors = new List<string>();
        }
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
            StatusCode = 200;
            Errors = new List<string>();
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public List<String> Errors { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public void SetErrors(Exception ex)
        {
            this.Errors.Add("Error message: " + ex.Message != null ? ex.Message : null);
            this.Errors.Add("Inner exception: " + ex.InnerException.Message != null ? ex.InnerException.Message : null);
            this.Errors.Add("Stack trace: " + ex.StackTrace != null ? ex.StackTrace : null);
        }

        public void Failed(int status, string mens)
        {
            this.Message = mens;
            this.StatusCode = status;
            this.Succeeded = false;
        }

        public void Sucess(string mens, T data)
        {
            this.Message = mens;
            this.Data = data;
            this.Succeeded = true;
        }
    }
}
