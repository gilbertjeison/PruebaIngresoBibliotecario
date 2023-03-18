namespace PruebaIngresoBibliotecario.Domain.Models.Interfaces
{
    public interface IBaseDtoResponse
    {   
        public class KeyPair
        {
            public string key { get; set; }
            public string value { get; set; }

            public KeyPair() { }
            public KeyPair(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
        }
    }
}
