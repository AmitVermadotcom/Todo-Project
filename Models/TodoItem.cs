using System;
public class TodoItem{
    public int Id {get; set;}
    public string? title {get; set;}
    public bool isDone{get; set;}
    public DateTime created {get; set;}
    public DateTime updated {get; set;}
    public int board_id {get; set;} 
}