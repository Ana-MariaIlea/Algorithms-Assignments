using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class GoodDungeon : SufficientDungen
{
    private int minArea;
    private int maxArea;
    private Dictionary<Room, int> roomArea;
    private Pen wallPen = new Pen(Color.FromArgb(128, Color.Black));
    private Pen doorPen = Pens.White;

    public GoodDungeon(Size pSize) : base(pSize)
    {
        roomArea = new Dictionary<Room, int>();
    }

    protected override void generate(int pMinimumRoomSize)
    {
        maxArea = pMinimumRoomSize * pMinimumRoomSize;
        minArea = size.Width * size.Height;

        makeRoom(new Room(new Rectangle(0, 0, size.Width, size.Height)), pMinimumRoomSize);
        foreach (Room room in rooms)
        {
            int area = room.area.Width * room.area.Height;
            if (area > maxArea) maxArea = area;
            if (area < minArea) minArea = area;
            roomArea.Add(room, area);
        }
        removeRooms();
        MakeDoors();
        draw();
    }

    protected override void draw()
    {
        graphics.Clear(Color.Transparent);
        paintRooms();
        drawDoors(doors, doorPen);
    }
    void removeRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (roomArea[rooms[i]] == minArea || roomArea[rooms[i]] == maxArea) rooms.Remove(rooms[i]);
        }
    }

    void paintRooms()
    {
        foreach (Room room in rooms)
        {
            int doorNumber = 0;
            foreach (Door door in doors)
            {
                if (door.roomA == room || door.roomB == room)
                {
                    doorNumber++;
                }
                else continue;
            }
            switch (doorNumber)
            {
                case 0:
                    drawRoom(room, wallPen, Brushes.Red);
                    break;
                case 1:
                    drawRoom(room, wallPen, Brushes.Orange);
                    break;
                case 2:
                    drawRoom(room, wallPen, Brushes.Yellow);
                    break;
                default:
                    drawRoom(room, wallPen, Brushes.Green);
                    break;
            }
        }
    }
}

