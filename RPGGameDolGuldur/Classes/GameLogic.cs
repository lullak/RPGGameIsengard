using System.ComponentModel.DataAnnotations;

namespace RPGGameIsengard.Classes
{
    public class GameLogic
    {
        public Character Character { get; set; }
        public List<Room> Rooms { get; set; }
        public bool IsGameOver { get; set; }


        public GameLogic()
        {
            Character = CreateCharacter();
            IsGameOver = false;
            Rooms = AddRooms();

        }

        public List<Room> AddRooms()
        {
            Rooms = new List<Room>();
            
            List<Item> itemsPrison = new List<Item> { Item.Torch() };
            List<Door> exitPrison = new List<Door>{new ("North", false)};
            Rooms.Add(new Room("Prison","Water is pouring in.", itemsPrison, exitPrison, false));

            List<Item> itemsHallway = new List<Item>();
            List<Door> exitHallway = new List<Door> { new("West", false),new ("East", false), new("South", false) }; 
            Rooms.Add(new Room("Hallway", "Even the hallway is flooded.", itemsHallway, exitHallway, false));

            List<Item> itemsKitchen = new List<Item> { Item.RawMeat(), Item.Key() };
            List<Door> exitKitchen = new List<Door> { new("East", false) };
            Rooms.Add(new Room("Kitchen", "Ýou seemed to have entered somekind of a kitchen.", itemsKitchen, exitKitchen, false));

            List<Item> itemsBarracks = new List<Item> { Item.Sword() };
            List<Door> exitBarracks = new List<Door> { new("West", false), new("North", true) };
            Rooms.Add(new Room("Barracks", "You find yourself in the orc barracks.", itemsBarracks, exitBarracks, false));

            List<Item> itemsCourtYard = new List<Item>();
            List<Door> exitCourtYard = new List<Door>();
            Rooms.Add(new Room("Court Yard", "", itemsCourtYard, exitCourtYard, true));

            return Rooms;
        }


        public void StartGameLoop()
        {
            Console.WriteLine("You wake up starved and with a headache, you notice that you are wet!");

            while (!IsGameOver)
            {
                Room currentRoom = Rooms[Character.CharacterCurrentRoomNumber];
                

                Console.Write("\nWhat do you want to do? (move, pickup, drop, look around, inventory, save, load or quit) ");
                string action = Console.ReadLine().ToLower();

                switch (action)
                {
                    case "move":
                        string moveResult = Move();
                        Console.WriteLine(moveResult);

                        if (Rooms[Character.CharacterCurrentRoomNumber].EndPoint)
                        {
                            Console.WriteLine("Finally you are outside and free. There are alot of Ents and you can see a damaged dam.");
                            IsGameOver = true;
                        }
                        break;

                    case "pickup":
                        Console.Write("Which item do you want to pick up? ");
                        string itemName = Console.ReadLine();
                        Item itemToPickUp = currentRoom.Items.FirstOrDefault(i => i.Name.ToLower() == itemName.ToLower());
                        if (itemToPickUp != null)
                        {
                            PickUpItem(currentRoom, itemToPickUp);
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                        break;

                    case "drop":
                        Console.Write("Which item do you want to drop? ");
                        string dropItemName = Console.ReadLine();
                        Item itemToDrop = Character.Items.FirstOrDefault(i => i.Name.ToLower() == dropItemName.ToLower());
                        if (itemToDrop != null)
                        {
                            DropItem(currentRoom, itemToDrop);
                        }
                        else
                        {
                            Console.WriteLine("You don't have that item.");
                        }
                        break;

                    case "look around":
                        Console.Write($"\nYou are in {currentRoom.Name}.");
                        Console.WriteLine($" {currentRoom.Description}");
                        
                        var exits = string.Join(", ", currentRoom.Exit.Select(e => e.ExitDirection));
                        Console.WriteLine($"Exits: {exits}");
                        CheckForItems(currentRoom);
                        break;

                    case "inventory":
                        if (Character.Items.Any())
                        {
                            Console.WriteLine("\nYou have:");
                            foreach (Item item in Character.Items)
                            {
                                Console.WriteLine($"{item.Name}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nYour inventory is empty.");
                        }
                        break;

                    case "combine":
                        Item rawMeat = Character.Items.FirstOrDefault(item => item.Name == "Raw rat meat");
                        Item torch = Character.Items.FirstOrDefault(item => item.Name == "Torch");

                        if (rawMeat != null && torch != null)
                        {
                            Character.Items.Remove(rawMeat);
                            Character.Items.Remove(torch);

                            Item cookedRatMeat = Item.CookedMeat();
                            Character.Items.Add(cookedRatMeat);

                            Console.WriteLine("You combined the Torch and Raw rat meat to cook the meat.");
                        }
                        else
                        {
                            Console.WriteLine("You don't have any items to combine.");
                        }
                        break;

                    case "load":
                        GameLogic load = Repositery.LoadGame();
                        if(load != null)
                        {
                            this.Character = load.Character;
                            this.Rooms = load.Rooms;
                            this.IsGameOver = load.IsGameOver;
                            Console.WriteLine("You loaded an eariler save.");
                        }
                        break;
                    case "save":
                        Repositery.SaveGame(this);
                        Console.WriteLine("You saved the game.");
                        break;
                    case "quit":
                        IsGameOver = true;
                        Console.WriteLine("You quit the game.");
                        break;

                    default:
                        Console.WriteLine("Wrong command. Try again.");
                        break;
                }
            }
        }

        public Character CreateCharacter()
        {
            List<Item> itemsCharacter = new ();
            return new Character(itemsCharacter, 0);
        }


        public void PickUpItem(Room room, Item item)
        {
            if (room.Items.Contains(item))
            {
                room.Items.Remove(item);
                Character.Items.Add(item);
                Console.WriteLine($"You picked up {item.Name}.");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }
        public void DropItem(Room room, Item item)
        {
            if (Character.Items.Contains(item))
            {
                Character.Items.Remove(item);
                room.Items.Add(item);
                Console.WriteLine($"You dropped {item.Name}.");
            }
            else
            {
                Console.WriteLine("You don't have this item.");
            }
        }

        public void CheckForItems(Room room)
        {
            foreach (Item item in room.Items) 
            {
                Console.WriteLine($"You see a {item.Name}.");
            }
        }

        public string Move()
        {
 
            Room currentRoom = Rooms[Character.CharacterCurrentRoomNumber];
            Console.Write("Where do you want to go? (East, West, North or South) ");
            string movementDirection = Console.ReadLine().ToLower();

            Door movementExit = currentRoom.Exit.FirstOrDefault(door => door.ExitDirection.ToLower() == movementDirection);

            if (movementExit != null)
            {
                if (movementExit.IsDoorLocked)
                {
                    Item keyItem = Character.Items.FirstOrDefault(item => item.Name.ToLower() == "key");
                    if (keyItem != null)
                    {
                        movementExit.IsDoorLocked = false;
                        Console.WriteLine("You used the key to unlock the door.");
                    }
                    else
                    {
                        return "The door is locked, and you don't have the key.";
                    }
                }

                int newRoomIndex = GetRoom(Character.CharacterCurrentRoomNumber, movementDirection);
                if (newRoomIndex != -1)
                {
                    Character.CharacterCurrentRoomNumber = newRoomIndex;

                    return $"\nYou moved {movementDirection} to {Rooms[newRoomIndex].Name}. {Rooms[newRoomIndex].Description}";
                }
            }

            return "There is no exit in that direction.";
        }


        public int GetRoom(int currentRoomIndex, string movementDirection)
        {
            Dictionary<string, int> moveableRooms = new Dictionary<string, int>
            {
                {"Prison_north", 1},
                {"Hallway_south", 0},  
                {"Hallway_west", 2},  
                {"Hallway_east", 3},  
                {"Kitchen_east", 1},  
                {"Barracks_west", 1}, 
                {"Barracks_north", 4}
            };

            string currentRoomDirection = $"{Rooms[currentRoomIndex].Name}_{movementDirection}";

            if (moveableRooms.ContainsKey(currentRoomDirection))
            {
                return moveableRooms[currentRoomDirection];
            }

            return -1;
        }
    }
}
