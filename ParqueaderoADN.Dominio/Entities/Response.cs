using System;
namespace ParqueaderoADN.Dominio.Entities
{
    public class Response<T>
    {
        public bool status;
        public T data;
        public string error;

        public Response(bool status, T data, string error)
        {
            this.status = status;
            this.data = data;
            this.error = error;
        }
    }
}
