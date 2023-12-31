﻿using Business_Logic.Strategy;
using Business_Model.Interfaces;
using Business_Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using IComponent = Business_Model.Interfaces.IComponent;

namespace Business_Model.Abstractions
{

    public abstract class Sudoku : ISudoku
    {
        public Stopwatch timer;
        public Dictionary<Position, Leaf> sharedLeaves = new Dictionary<Position, Leaf>();
        public List<IComponent> components;
        public SudokuMode mode;
        public SudokuInputModeState CurrentState;

        public Sudoku()
        {
            components = new List<IComponent>();
        }

        public void AcceptLoad(ISudokuLoadVisitor visitor)
        {
            visitor.VisitLoadComposite(new Composite());
        }

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        public abstract void AcceptPrint(ISudokuPrintVisitor visitor);

        public bool IsSafeToPlaceValue(Leaf cell, int value, Sudoku JigsawSudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                if (GetCellValue(cell.position.X, i, JigsawSudoku) == value || GetCellValue(i, cell.position.Y, JigsawSudoku) == value)
                    return false;
            }

            Composite group = GetGroup(cell, JigsawSudoku);
            foreach (Leaf groupCell in group.cells)
            {
                if (groupCell.currentValue == value)
                    return false;
            }

            return true;
        }


        public int GetCellValue(int x, int y, Sudoku JigsawSudoku)
        {
            foreach (IComponent component in JigsawSudoku.components)
            {
                if (component is Composite group)
                {
                    foreach (Leaf cell in group.cells)
                    {
                        if (cell.position.X == x && cell.position.Y == y)
                            return cell.currentValue;
                    }
                }
            }

            return 0;
        }

        public Composite GetGroup(Leaf cell, Sudoku JigsawSudoku)
        {
            foreach (IComponent component in JigsawSudoku.components)
            {
                if (component is Composite group)
                {
                    if (group.cells.Contains(cell))
                        return group;
                }
            }
            return null;
        }



    }


}

