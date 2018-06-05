﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileHelper
{

    static Tile[,] tiles;

    public static Vector3 TilePosition(Tile t)
    {
        return new Vector3(t.x, t.y, 0);
    }

    public static Tile TileUnderPos(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);

        if (x >= tiles.GetLength(0) || x < 0 || y >= tiles.GetLength(1) || y < 0)
        {
            return null;
        }
        else
        {
            return tiles[x, y];
        }
    }

    public static List<Tile> Neighbours4(Tile t)
    {
        List<Tile> neighbours = new List<Tile>();

        if (t.x != 0)
        {
            neighbours.Add(tiles[t.x - 1, t.y]);
        }
        if (t.x != tiles.GetLength(0))
        {
            neighbours.Add(tiles[t.x + 1, t.y]);
        }
        if (t.y != 0)
        {
            neighbours.Add(tiles[t.x, t.y - 1]);
        }
        if (t.y != tiles.GetLength(1))
        {
            neighbours.Add(tiles[t.x, t.y + 1]);
        }

        return neighbours;
    }

    public static void SetTileArray(Tile[,] t)
    {
        tiles = t;
    }


    public static Vector2 RoundVector(Vector2 a)
    {
        return new Vector2(Mathf.Round(a.x), Mathf.Round(a.y));
    }

    public static Vector2 CeilVector(Vector2 b)
    {
        if (b.x >= 0 && b.y >= 0)
            return new Vector2(Mathf.Ceil(b.x), Mathf.Ceil(b.y));
        else
            return new Vector2(Mathf.Floor(b.x), Mathf.Floor(b.y));
    }

    public static bool checkStartDiff(Vector2 startDir, Vector2 nextTileDir)
    {
        if (
            (startDir.x == nextTileDir.x && (startDir.x != 0 && nextTileDir.x != 0))
            ||
            (startDir.y == nextTileDir.y && (startDir.y != 0 && nextTileDir.y != 0))
            ||
            (startDir.x == nextTileDir.y && (startDir.x != 0 && nextTileDir.y != 0))
            ||
            (startDir.y == nextTileDir.x && (startDir.y != 0 && nextTileDir.x != 0))
            )
            return false;
        else
            return true;
    }

    public static Vector2 PredictEnemyPos(Enemy enemy, float t)
    {
        Vector2 pos = enemy.transform.position;
        Vector2 pos2 = RoundVector(pos);
        int currIndex = enemy.path.IndexOf(TileUnderPos(pos2)); //Current tile index 
        Vector2 startDir = CeilVector(pos - pos2);
        Vector2 nextTileDirStart = enemy.path[currIndex + 1].Position() - enemy.path[currIndex].Position();
        int index = Mathf.RoundToInt(enemy.speed * t);
        int endIndex = index + currIndex; //index of the end tile
        float startDifference =
           checkStartDiff(startDir, nextTileDirStart) ?
                -(Vector2.Distance(pos, pos2))
            :
                Vector2.Distance(pos, pos2); //start point to start tile
        float pathDistance = enemy.speed * t; //distance from start point to end point
        float endDifference = pathDistance - index - startDifference; //end tile to end point

        Vector2 endDir = Vector2.zero;
        if (endIndex + 2 <= enemy.path.Count)
        {
            if (Mathf.Sign(endDifference) == -1)
            {
                endDir = enemy.path[endIndex].Position() - enemy.path[endIndex - 1].Position();
            }
            else
            {
                endDir = enemy.path[endIndex + 1].Position() - enemy.path[endIndex].Position();
            }

        }
        else
        {
            Debug.Log("We went too far.");
            return Vector2.zero;
        }
        Tile endTile = enemy.path[endIndex];
        Vector2 result = endTile.Position() + endDir * endDifference;

        /*
        Debug.Log(
            "Time: " + t +
            "\n result:" + result +
            "\n Enemy pos: " + pos + 
            "\n Rounded pos: " + pos2 + 
            "\n currIndex: " + currIndex + 
            "\n index: " + index +
            "\n endIndex: " + endIndex + 
            "\n pathDistance: " + pathDistance +
            "\n startDifference: " + startDifference +
            "\n endDiffenrence: " + endDifference +
            //"\n difference: " + difference +
            "\n endDirection: " + endDir +
            //"\n pathDistanceWithoutEndDifference: " + pathDistanceWithoutEndDifference +
            "\n startDirection: " + startDir +
            "\n endTile: " + endTile +
            "\n startDir + nextTileDir" + (startDir + nextTileDirStart)
            );
          */

        return result;
    }
}