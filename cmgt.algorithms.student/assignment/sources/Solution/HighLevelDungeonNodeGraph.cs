using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using GXPEngine;


class HighLevelDungeonNodeGraph : NodeGraph
{
    protected Dungeon _dungeon;

    public HighLevelDungeonNodeGraph(Dungeon pDungeon) : base((int)(pDungeon.size.Width * pDungeon.scale), (int)(pDungeon.size.Height * pDungeon.scale), (int)pDungeon.scale / 3)
    {
        Debug.Assert(pDungeon != null, "Please pass in a dungeon.");

        _dungeon = pDungeon;
    }

    protected override void generate()
    {
        Dictionary<Room, Node> roomToNodeMap = new Dictionary<Room, Node>();
        for (int i = 0; i < _dungeon.rooms.Count(); i++)
        {
            Node roomNode = new Node(getRoomCenter(_dungeon.rooms[i]));
            nodes.Add(roomNode);
            for (int j = 0; j < _dungeon.doors.Count(); j++)
            {
                if (_dungeon.doors[j].roomA ==_dungeon.rooms[i] || _dungeon.doors[j].roomB == _dungeon.rooms[i])
                {
                    Node doorNode;
                    doorNode = new Node(getDoorCenter(_dungeon.doors[j]));
                    int k;
                    for (k = 0; k < nodes.Count(); k++)
                    {
                        if (nodes[k].location == doorNode.location)
                        {
                            doorNode = nodes[k];
                            break;
                        }
                    }
                    if (k >= nodes.Count())
                    {
                        nodes.Add(doorNode);
                    }
                    AddConnection(roomNode, doorNode);

                }
            }
        }

    }

    /**
     * A helper method for your convenience so you don't have to meddle with coordinate transformations.
     * @return the location of the center of the given room you can use for your nodes in this class
     */
    protected Point getRoomCenter(Room pRoom)
    {
        float centerX = ((pRoom.area.Left + pRoom.area.Right) / 2.0f) * _dungeon.scale;
        float centerY = ((pRoom.area.Top + pRoom.area.Bottom) / 2.0f) * _dungeon.scale;
        return new Point((int)centerX, (int)centerY);
    }

    /**
     * A helper method for your convenience so you don't have to meddle with coordinate transformations.
     * @return the location of the center of the given door you can use for your nodes in this class
     */
    protected Point getDoorCenter(Door pDoor)
    {
        return getPointCenter(pDoor.location);
    }

    /**
     * A helper method for your convenience so you don't have to meddle with coordinate transformations.
     * @return the location of the center of the given point you can use for your nodes in this class
     */
    protected Point getPointCenter(Point pLocation)
    {
        float centerX = (pLocation.X + 0.5f) * _dungeon.scale;
        float centerY = (pLocation.Y + 0.5f) * _dungeon.scale;
        return new Point((int)centerX, (int)centerY);
    }
}

