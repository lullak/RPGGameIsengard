namespace RPGGameIsengard.Classes
{
    public class Character
    {
        public List<Item> Items { get; set; }
        public int CharacterCurrentRoomNumber { get; set; }


        public Character(List<Item> items, int characterCurrentRoomNumber)
        {
            Items = items;
            CharacterCurrentRoomNumber = characterCurrentRoomNumber;
        }

    }
}
