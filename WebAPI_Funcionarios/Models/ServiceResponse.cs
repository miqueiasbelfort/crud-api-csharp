namespace WebAPI_Funcionarios.Models
{
    // O <T> diz que recebe dados genericos
    public class ServiceResponse<T>
    {
        public T? Dados { get; set; }
        public string Messagem { get; set; } = string.Empty;
        public bool Sucesso { get; set; } = true;
    }
}
