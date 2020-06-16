using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GXPEngine;

class SufficientDungen : Dungeon
{
    public SufficientDungen(Size pSize) : base(pSize) { }
    protected override void generate(int pMinimumRoomSize)
    {
        makeRoom(new Room(new Rectangle(0, 0, size.Width, size.Height)), pMinimumRoomSize);
        MakeDoors();
    }
    protected void makeRoom(Room currentRoom, int minSize)
    {
        if (currentRoom.area.Width > currentRoom.area.Height)
        {
            if (currentRoom.area.Width >= minSize * 2 && currentRoom.area.Height >= minSize)
            {
                int split = Utils.Random(minSize, currentRoom.area.Width - minSize);
                Room newRoom1 = new Room(new Rectangle(currentRoom.area.X, currentRoom.area.Y,
                   split + 1, currentRoom.area.Height));
                Room newRoom2 = new Room(new Rectangle(split + currentRoom.area.X, currentRoom.area.Y,
                   currentRoom.area.Width - split, currentRoom.area.Height));
                makeRoom(newRoom1, minSize);
                makeRoom(newRoom2, minSize);
            }
            else
            {
                rooms.Add(currentRoom);
            }
        }
        else
        {
            if (currentRoom.area.Width >= minSize && currentRoom.area.Height >= minSize * 2)
            {
                int split = Utils.Random(minSize, currentRoom.area.Height - minSize);
                Room newRoom1 = new Room(new Rectangle(currentRoom.area.X, currentRoom.area.Y, currentRoom.area.Width, split + 1));
                Room newRoom2 = new Room(new Rectangle(currentRoom.area.X, currentRoom.area.Y + split,
                    currentRoom.area.Width, currentRoom.area.Height - split));
                makeRoom(newRoom1, minSize);
                makeRoom(newRoom2, minSize);
            }
            else
            {
                rooms.Add(currentRoom);
            }
        }
    }

    protected void MakeDoors()
    {
        for (int i = 0; i < rooms.Count(); i++)
        {
            for (int j = i + 1; j < rooms.Count(); j++)
            {
                if (rooms[i].area.IntersectsWith(rooms[j].area))
                {
                    Rectangle overlap;
                    overlap = Rectangle.Intersect(rooms[i].area, rooms[j].area);
                    if (overlap.Width > overlap.Height)
                    {
                        if (overlap.Width > 3 && overlap.Height >= 1)
                        {
                            Door door = new Door(new Point(overlap.X + overlap.Width / 2 + Utils.Random(-overlap.Width / 2 + 1, overlap.Width / 2 - 1), overlap.Y));
                            doors.Add(door);
                            door.roomA = rooms[i];
                            door.roomB = rooms[j];
                        }
                        else continue;
                    }
                    else
                    {
                        if (overlap.Height > 3 && overlap.Width >= 1)
                        {
                            Door door = new Door(new Point(overlap.X, overlap.Y + overlap.Height / 2 + Utils.Random(-overlap.Height / 2 + 1, overlap.Height / 2 - 1)));
                            doors.Add(door);
                            door.roomA = rooms[i];
                            door.roomB = rooms[j];
                        }
                        else continue;
                    }
                }
                else continue;
            }
        }
    }
}

