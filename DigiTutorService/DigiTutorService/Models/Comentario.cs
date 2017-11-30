using System;


namespace DigiTutorService.Models
{
    public class Comentario
    {
        public int Id_Comentario { get; set; }
        public string Id_Autor { get; set; }
        public string Nombre_Autor { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha_comentario { get; set; }

        public bool IsFull()
        {
            if (Id_Autor != null && Nombre_Autor != null && Contenido != null)
                if (Contenido != "")
                    return true;
                else return false;
            else return false;
           
        }
    }
}