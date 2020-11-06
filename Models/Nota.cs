using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asap.mvc  
{
  public class Nota
  {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public long ID {get; set;}
      
      [Required]
      public string Titulo {get; set;}

      public string Cuerpo {get; set;}

      public Usuario Creador {get; set;}
  }
}