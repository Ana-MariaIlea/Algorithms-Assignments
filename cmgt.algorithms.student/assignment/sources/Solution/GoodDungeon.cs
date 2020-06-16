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
    public GoodDungeon(Size pSize) : base(pSize)
    {
        roomArea = new Dictionary<Room, int>();
    }

    protected override void generate(int pMinimumRoomSize)
    {
        maxArea = pMinimumRoomSize * pMinimumRoomSize;
        minArea = size.Width * size.Height;
        //
        // base.generate(pMinimumRoomSize);
        //
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
        paintRooms();
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
                    // Graphics.FillRectngle(new SolidBrush(Color.Blue), room.area);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
    }
}

