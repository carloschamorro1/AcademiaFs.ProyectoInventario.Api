using System.Security.Cryptography;
using System.Text;

namespace AcademiaFs.ProyectoInventario.Api.Domain.Usuarios
{
    public class UsuariosDomain
    {
        public bool EstaVacio(string? usuario, string? password)
        {
            if (usuario == string.Empty || password == string.Empty)
            {
                return true;
            }
            return false;
        }

        public byte[] ObtenerBytes(string? clave)
        {
            // Truncate hashBytes to 36 bytes (288 bits)
            byte[] truncatedHash = Encoding.UTF8.GetBytes(clave);

            return truncatedHash;

        }

        public string HashedString(byte[] hashBytes)
        {
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        public bool ValidarPassword(byte[]? dbClave, string? inputPassword)
        {
            var inputPasswordCifrada = ObtenerBytes(inputPassword);
            var hashedLocalString = HashedString(inputPasswordCifrada);
            var hashedDbString = HashedString(dbClave);

            return hashedLocalString == hashedDbString;
        }
    }
}
