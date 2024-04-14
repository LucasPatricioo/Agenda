// Gerar uma sequência de bytes aleatórios (32 bytes para uma chave SHA-256)
using System.Security.Cryptography;

byte[] randomBytes = new byte[32];
using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
{
    rng.GetBytes(randomBytes);
}

// Aplicar SHA-256 aos bytes aleatórios para obter a chave privada
using (SHA256 sha256 = SHA256.Create())
{
    byte[] hashBytes = sha256.ComputeHash(randomBytes);
    string privateKey = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

    string caminhoArquivo = "privatekey.txt";

    string chave = privateKey;

    using (StreamWriter writer = new StreamWriter(caminhoArquivo))
    {
        writer.Write(chave);
    }

    Console.WriteLine("Chave privada SHA-256: " + privateKey);
}