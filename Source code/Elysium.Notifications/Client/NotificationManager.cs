﻿using System;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Notifications.Client
{
    [UsedImplicitly]
    internal sealed class NotificationManager : ClientBase<INotificationManager>, INotificationManager
    {
        [UsedImplicitly]
        public NotificationManager() : base(new NetNamedPipeBinding(NetNamedPipeSecurityMode.None)
                                                {
                                                    OpenTimeout = TimeSpan.FromMinutes(2d),
                                                    SendTimeout = TimeSpan.FromMinutes(8d),
                                                    ReceiveTimeout = TimeSpan.FromMinutes(8d),
                                                    CloseTimeout = TimeSpan.FromMinutes(2d),
                                                    TransactionFlow = false,
                                                    TransferMode = TransferMode.Buffered,
                                                    TransactionProtocol = TransactionProtocol.OleTransactions,
                                                    HostNameComparisonMode = HostNameComparisonMode.Exact,
                                                    MaxBufferPoolSize = 2147483520,
                                                    MaxBufferSize = 16777215,
                                                    MaxConnections = 128,
                                                    MaxReceivedMessageSize = 16777215
                                                },
                                            new EndpointAddress("net.pipe://localhost/elysium/v1.5.732.0/notifications"))
        {
        }

        [UsedImplicitly]
        public Notification Reserve(Rect workArea)
        {
            Contract.Ensures(Contract.Result<Notification>() != null);
            var slot = Channel.Reserve(workArea);

            // Can't be proven.
            Contract.Assume(slot != null);
            return slot;
        }

        [UsedImplicitly]
        public void Update(TimeSpan lifetime, Animation animation, Thickness margin, Size size,
                           HorizontalPlacement horizontalPlacement, VerticalPlacement verticalPlacement)
        {
            Channel.Update(lifetime, animation, margin, size, horizontalPlacement, verticalPlacement);
        }

        [UsedImplicitly]
        public void Free(byte id)
        {
            Channel.Free(id);
        }
    }
}
