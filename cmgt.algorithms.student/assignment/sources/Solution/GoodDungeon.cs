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
    List<Room> roomsWithOneDoor;
    List<Room> roomsWithNoDoor;
    List<Room> roomsWithTwoDoors;
    List<Room> roomsWithMoreDoors;
    public GoodDungeon(Size pSize) : base(pSize)
    {
        roomArea = new Dictionary<Room, int>();
        roomsWithNoDoor = new List<Room>();
        roomsWithOneDoor = new List<Room>();
        roomsWithTwoDoors = new List<Room>();
        roomsWithMoreDoors = new List<Room>();
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
        paintRooms();
        draw();
        
    }

    protected override void draw()
    {
        graphics.Clear(Color.Transparent);
        drawRooms(roomsWithNoDoor, wallPen,Brushes.Red);
        drawRooms(roomsWithOneDoor, wallPen,Brushes.Orange);
        drawRooms(roomsWithTwoDoors, wallPen, Brushes.Yellow);
        drawRooms(roomsWithMoreDoors, wallPen, Brushes.Green);
        drawDoors(doors, doorPen);
    }
    void removeRooms()
    {
        for(int i=0;i<rooms.Count();i++)
        {
            int area = rooms[i].area.Width * rooms[i].area.Height;
            if (area == minArea || area == maxArea) rooms.Remove(rooms[i]);
        }
    }

    void paintRooms()
    {
        foreach(Room room in rooms)
        {
            int doorNumber = 0;
            foreach(Door door in doors)
            {
                if (door.roomA == room || door.roomB == room)
                {
                    doorNumber++;
                }
            }
            switch (doorNumber)
            {
                case 0:
                    roomsWithNoDoor.Add(room);
                    break;
                case 1:
                    roomsWithOneDoor.Add(room);
                    break;
                case 2:
                    roomsWithTwoDoors.Add(room);
                    break;
                default:
                    roomsWithMoreDoors.Add(room);
                    break;
            }
        }
    }
}

