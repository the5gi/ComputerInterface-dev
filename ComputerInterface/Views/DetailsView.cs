﻿using System;
using System.IO;
using System.Text;
using BepInEx;
using ComputerInterface.Interfaces;
using ComputerInterface.ViewLib;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;

namespace ComputerInterface.Views
{
    internal class DetailsEntry : IComputerModEntry
    {
        public string EntryName => "Details";
        public Type EntryViewType => typeof(DetailsView);
    }

    internal class DetailsView : ComputerView
    {
        private string _name;
        private string _roomCode;
        private int _playerCount;
        private int _playerBans;

        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            UpdateStats();
            Redraw();
        }

        private void UpdateStats()
        {
            _name = BaseGameInterface.GetName();
            _roomCode = BaseGameInterface.GetRoomCode();
            _playerCount = PhotonNetwork.CountOfPlayersInRooms;
            _playerBans = GorillaComputer.instance.GetField<int>("usersBanned");
        }

        private void Redraw()
        {
            var str = new StringBuilder();

            str.AppendLine();

            str.AppendClr("Name: ", "ffffff50")
                .AppendLine()
                .Repeat(" ", 4)
                .Append(_name)
                .AppendLine();

            str.AppendClr("Current room   : ", "ffffff50")
                .AppendLine()
                .Repeat(" ", 4)
                .Append(_roomCode.IsNullOrWhiteSpace() ? "-None-" : _roomCode)
                .AppendLine();

            str.AppendClr("Players online : ", "ffffff50")
                .AppendLine()
                .Repeat(" ", 4)
                .Append(_playerCount)
                .AppendLine();

            str.AppendClr("Bans yesterday : ", "ffffff50")
                .AppendLine()
                .Repeat(" ", 4)
                .Append(_playerBans)
                .AppendLine();

            Text = str.ToString();
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}