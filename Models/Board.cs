using System.ComponentModel.DataAnnotations;
public class Board
{
    [Key]
    public int Id {get; set;}
    public string name { get; set; }
}
