using System.Reflection.Metadata;

namespace UscastoresProject.Models;
public class Uscastores
{
    public int IdPersonal { get; set; }
    public string? Nombre { get; set; }
    public string Password { get; set; }
    public string Pregunta { get; set; }
    public string Respuesta { get; set; }
    public string UltimoPassword { get; set; }

    public string? archivo {get; set;}
}
