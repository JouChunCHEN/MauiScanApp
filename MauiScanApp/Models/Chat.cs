﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MauiScanApp.Models;

public partial class Chat
{
    public int ChatRoomId { get; set; }

    public int ChatMessengerId { get; set; }

    public int ReceiverId { get; set; }

    public int SenderId { get; set; }

    public string MessageContent { get; set; }

    public DateTime? MessageCreateTime { get; set; }

    public virtual CustomerInfomation ChatMessenger { get; set; }

    public virtual Supplier ChatMessengerNavigation { get; set; }

    public virtual CustomerInfomation Receiver { get; set; }

    public virtual Supplier ReceiverNavigation { get; set; }
}