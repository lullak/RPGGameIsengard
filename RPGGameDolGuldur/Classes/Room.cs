namespace RPGGameIsengard.Classes
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public List<Door> Exit { get; set; }
        public bool EndPoint { get; set; }

        public Room(string name, string description, List<Item> items, List<Door> exit, bool endPoint)
        {
            Name = name;
            Description = description;
            Items = items;
            Exit = exit;
            EndPoint = endPoint;
        }
    }
}
