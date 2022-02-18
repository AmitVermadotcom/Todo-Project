using System;
using System.ComponentModel.DataAnnotations;
public class TodoItem{
    [Key]
    public int Id {get; set;}
    public string? title {get; set;}
    public bool isDone{get; set;}
    public DateTime created {get; set;}
    public DateTime updated {get; set;}
    public Board board {get; set;} 
}