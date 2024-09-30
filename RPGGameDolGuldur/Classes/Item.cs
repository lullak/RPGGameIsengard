namespace RPGGameIsengard.Classes
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanBeCombined { get; set; }

        public Item(string name, string description, bool canBeCombined)
        {
            Name = name;
            Description = description;
            CanBeCombined = canBeCombined;
        }

        public static Item Key()
        {
            return new Item("Key", "A old rusty key.", false);
        }
        public static Item Torch()
        {
            return new Item("Torch", "A flaming Torch.", true);
        }
        public static Item Sword()
        {
            return new Item("Orc Sword", "A disgusting looking Orc blade.", false);
        }
        public static Item RawMeat()
        {
            return new Item("Raw rat meat", "Disgustin uncooked Rat meat.", true);
        }
        public static Item CookedMeat()
        {
            return new Item("Barely cooked rat meat", "Disgustin cooked Rat meat.", false);
        }
    }
}
