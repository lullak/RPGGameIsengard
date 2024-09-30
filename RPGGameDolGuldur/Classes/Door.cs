namespace RPGGameIsengard.Classes
{
    public class Door
    {
        public string ExitDirection { get; set; }
        public bool IsDoorLocked { get; set; }

        public Door(string exitDirection, bool isDoorLocked)
        {
            ExitDirection = exitDirection;
            IsDoorLocked = isDoorLocked;
        }
    }
}
